using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;                //�ǹ� ���� ����
    private Vector3 lastPosition;                   //�÷��̾��� ������ ��ġ ����
    private float moveThreshold = 0.1f;             //�̵� ���� �Ӱ谪
    private ConstructibleBuilding currentNearbyBuilding;    //���� ������ �ִ� �Ǽ� ������ �ǹ�
    private BuildingCrafter currentBuildingCrafter;          //�߰� : ���� �ǹ��� ���� �ý��� 

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        CheckForBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ 
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForBuilding();                                //�̵��� ������ üũ 
            lastPosition = transform.position;              //���� ��ġ�� ������ ��ġ�� ������Ʈ 
        }

        //����� �������� �ְ� F Ű�� ������ �� �Ǽ� 
        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if(!currentNearbyBuilding.isConstructed)                                            //�ǹ��� �ϼ��� ���� �ʾ��� �� 
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());         //PlayerInventroy�� �����Ͽ� �Ǽ� ���� �Լ� ȣ�� 
            }
            else if(currentBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding.buildingName}�� ���� �޴� ����");
                CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);                     //�̱������� �����༭ UI ǥ�ø� �Ѵ�. 
            }
           
        }
    }

    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);  //���� ���� ���� ��� �ݶ��̴��� ã��

        float closestDistance = float.MaxValue;     //���� ����� �Ÿ��� �ʱⰪ
        ConstructibleBuilding closestBuilding = null;         //���� ����� ������ �ʱⰪ
        BuildingCrafter closestCrafter = null;              //�߰� 


        foreach (Collider collider in hitColliders)   
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();       //�ǹ� ����
            if (building != null) //��� �ǹ� ������ ���� 
            {
                float distance = Vector3.Distance(transform.position, building.transform.position); //�Ÿ� ���
                if (distance < closestDistance)     //�� ����� �������� �߰� �� ������Ʈ 
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closestCrafter = building.GetComponent<BuildingCrafter>();              //���⼭ ũ������ �������� 
                }
            }
        }

        if (closestBuilding != currentNearbyBuilding) //���� �����  �ǹ��� ����Ǿ��� ���� �޼��� ǥ��
        {
            currentNearbyBuilding = closestBuilding;            //���� �����ǹ� ������Ʈ 
            currentBuildingCrafter = closestCrafter;            //�߰�

            if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstructed)
            {
                if (FloatingTextManager.Instance != null)
                {
                    FloatingTextManager.Instance.Show(
                        $"[F]Ű�� {currentNearbyBuilding.buildingName} �Ǽ� (���� {currentNearbyBuilding.requiredTree} �� �ʿ�)",
                        currentNearbyBuilding.transform.position + Vector3.up
                    );
                }
            }
        }
    }

}
