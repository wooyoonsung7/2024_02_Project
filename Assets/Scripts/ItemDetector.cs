using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//아이템 종류 정의
public enum ItemType
{
    Crystal,            //크리스탈
    Plant,              //식물
    Bush,               //수풀
    Tree,               //나무
}

public class ItemDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;                //아이템 감지 범위
    private Vector3 lastPosition;                   //플레이어의 마지막 위치 저장 (플리에어가 이동이 있을 경우 주변을 감지해서 아이템 획득)
    private float moveThreshold = 0.1f;             //이동 감지 임계갑 (플레이어가 이동해야 할 최소거리)
    private CollectibleItem currentNearbyItem;      //현재 가장 가까이 있는 수집 가능한 아이템  
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;          //시작 시 현재 위치를 마지막 위치로 설정
        CheckForItems();                            //초기 아이템 체크 수행
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어가 일정 거리 이상 이동했는지 체크
        if (Vector3.Distance(lastPosition, transform.position) < checkRadius)
        {
            CheckForItems();                        //이동시 아이템 체크
            lastPosition = transform.position;      //현재 위치를 마지막 위치로 업데이트      
        }
        //가까운 아이템이 있고 E키를 눌렀을 때 아이템 수집
        if(currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());         //PlayerInventory를 참조하여 아이템 수집
        }
    }

    //주변의 수집 가능한 아이템을 감지하는 함수
    private void CheckForItems()
    {
        //감지 범위 내의 모든 콜라이더를 찾음
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;     //가장 가까운 거리의 초기값
        CollectibleItem closestItemem = null;       //가장 가까운 아이템 초기값

        //각 콜라이더를 검사하여 수집 가능한 아이템을 찾음
        foreach( Collider collider in hitCollider )
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();
            if(item != null && item.canCollect) //아이템이 있고 수집 가능한지 확인
            {
                float distance = Vector3.Distance(transform.position, item.transform.position); //거리계산
                if(distance > closestDistance)
                {
                    closestDistance = distance;
                    closestItemem = item;
                }
            }
        }

        if( closestItemem != currentNearbyItem)  //가장 가까운 아이템이 변경되었을 때만 메세지 표시
        {
            currentNearbyItem = closestItemem;
            if( currentNearbyItem != null )
            {
                Debug.Log($"[E] 키를 눌러 {currentNearbyItem.itemName}수집");             //새로운 아이템 수집 메세지 표시
            }
        }
    }

    //감지 범위를 시각적으로 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;          //감지 범위 색성 설정
        Gizmos.DrawWireSphere(transform.position, checkRadius);     //감지 범위를 궃로 표시
    }
}
