using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GridCell 클래스는 각 그리드 셀의 상태아 데이터를 관리
public class GridCell
{
    public Vector3 Position;                            //셀의 그리드 내 위치
    public bool IsOccupied;                             //셀이 건물로 차있는지 여부
    public GameObject Building;                         //셀에 배치된 건물 객체

    public GridCell(Vector3 position)                   //생성자 객체가 호출 된때
    {
        Position = position;
        IsOccupied = false;
        Building = null;
    }
}

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private int width = 10;            //그리드 가로 크기
    [SerializeField] private int height = 10;           //그리드 세로 크기
    [SerializeField] private float cellSize = 1;        //각 셀의 크기

    [SerializeField] private GameObject cellPrefab;       //셀 프리팹
    [SerializeField] private GameObject buildingPrefab;   //건물 프리팹
    [SerializeField] private PlayerController playerController;         //플레이어 컨트롤러 참조
    [SerializeField] private float maxBuildDistance = 5f;               //건설 가능한 최대 거리

    [SerializeField] private Grid grid;                          //그리드 선언 후 받아온다.
    private GridCell[,] cells;                                   //2차 배열로 선언 GridCell
    private Camera firstPersonCamera;                            //플레이어의 1인칭 카레라를 가져온다.

    void Start()
    {
        firstPersonCamera = playerController.firstPersonCamera;
        CreateGrid();
    }

    private void Update()
    {
        Vector3 lookPosition = GetLookPosition();
        if(lookPosition != Vector3.zero)            //포지션이 0이 아닐때
        {
            Vector3Int gridPosition = grid.WorldToCell(lookPosition);      //보고 있는 그리드 셀의 월드 좌표를 받아온다.
            if (isValidGridPosition(gridPosition))
            {
                HighlightCell(gridPosition);
                if(Input.GetMouseButtonDown(0))
                {
                    PlaceBuilding(gridPosition);
                }
                if(Input.GetMouseButtonDown(1))
                {
                    RemoveBuilding(gridPosition);
                }
            }
        }
    }
    //그리드를 생성하고 셀을 초기화하는 함수
    private void PlaceBuilding(Vector3Int gridPosition)
    {
        GridCell cell = cells[gridPosition.x, gridPosition.z];
        if(!cell.IsOccupied)
        {
            Vector3 worldPosition = grid.GetCellCenterWorld(gridPosition);
            GameObject building = Instantiate(buildingPrefab, worldPosition, Quaternion.identity);
            cell.IsOccupied = true;
            cell.Building = building;
        }
    }

    private void RemoveBuilding(Vector3Int gridPosition)
    {
        GridCell cell = cells[gridPosition.x, gridPosition.z];

        if (!cell.IsOccupied)
        {
            Destroy(cell.Building);
            cell.IsOccupied = true;
            cell.Building = null;
        }
    }

    private void CreateGrid()
    {
        grid.cellSize = new Vector3(cellSize, cellSize, cellSize);      //설정한 셀 사이즈를 그리드 컴포넌트에 넣는다.
        cells = new GridCell[width, height];                            //그리드 가로 세로 칸수 설정
        Vector3 gridCenter = playerController.transform.position;       //플레이어 중심으로 그리드 생성 (원하는 위치로 변경가능)
        gridCenter.y = 0;
        transform.position = gridCenter - new Vector3(width * cellSize / 2.0f, 0, height * cellSize / 2);       //그리드 중심으로 이동 시킨다.

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, z);                   //(0,0,0) (1,0,0), (2,0,0) ~ (10,0,10) for 문 돌리면서 배치
                Vector3 worldPosition = grid.GetCellCenterWorld(cellPosition);  //grid를 통해 월드 포지션을 가져온다.
                GameObject cellObject = Instantiate(cellPrefab, worldPosition, cellPrefab.transform.rotation);      //설정한 그리드를 만든다.
                cellObject.transform.SetParent(transform);                      //지금 오브젝트 하위로 설정 한다.

                cells[x, z] = new GridCell(cellPosition);                       //셀 데이터 값을 생성 한다.

            }
        }
    }
    
    //선택된 셀을 하이라이트하는 함수

    private void HighlightCell(Vector3Int gridPosition)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject cellObject = cells[x, z].Building != null ? cells[x, z].Building : transform.GetChild(x * height + z).gameObject;
                cellObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        GridCell cell = cells[gridPosition.x, gridPosition.z];
        GameObject highlightObject =
            cell.Building!=null?cell.Building:transform.GetChild(gridPosition.x * height + gridPosition.z).gameObject;
        highlightObject.GetComponent<Renderer>().material.color = cell.IsOccupied? Color.red : Color.green;
    }

    //플레이어가 보고 있는 위치를 계산하는 매서드
    private Vector3 GetLookPosition()
    {
        if (playerController.isActiveAndEnabled) //플레이어가 1인칭 모드
        {
            Ray ray = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);   //받아온 1인칭 카메라 앞으로 ray
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxBuildDistance))  //설정한 ray에 물체가 있을 경우
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red); //Secne 창에서 빨간색 선으로 ray를 보여준다.
                return hitInfo.point;                                                   //raycast 된 물체 정보를 리턴하다.
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * maxBuildDistance, Color.white);   //Secen 창에서 하얀색 선으로 ray를 보여준다
            }
        }
        else
        {
            Vector3 characterPosition = playerController.transform.position;                        //캐릭터 위치 선언
            Vector3 characterForward = playerController.transform.forward;                          //캐릭터 앞방향 선언
            Vector3 rayOrigin = characterPosition + Vector3.up * 1.5f + characterForward * 0.5f;    //레이를 시작 위치 설정
            Vector3 rayDirection = (characterForward - Vector3.up).normalized;                      //레이를 쏠 방향 설정 후 정규직 (0~1)

            Ray ray = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);   //받아온 1인칭 카메라 앞으로 ray
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxBuildDistance))  //설정한 ray에 물체가 있을 경우
            {
                    Debug.DrawRay(ray.origin, ray.direction * maxBuildDistance, Color.red); //Secne 창에서 빨간색 선으로 ray를 보여준다.
                    return hitInfo.point;                                                   //raycast 된 물체 정보를 리턴하다.
            }
            else
            {
                    Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.white);   //Secen 창에서 하얀색 선으로 ray를 보여준다
            }
        }

        return Vector3.zero;
    }

    //그리드 포지션이 유효한지 확인하는 함수

    private bool isValidGridPosition(Vector3 gridPosition)
    {
        return gridPosition.magnitude > 0 && gridPosition.x < width &&
                gridPosition.z >= 0 && gridPosition.z < height;
    }

    private void OnDrawGizmos()     //Gizom를 표시해주는 함수
    {
        Gizmos.color = Color.blue;
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3 cellCenter = grid.GetCellCenterWorld(new Vector3Int(x, 0, z));      //그리드 중심 점을 가져온다.
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));     //각각의 칸을 그려준다.
            }
        }
    }
}
