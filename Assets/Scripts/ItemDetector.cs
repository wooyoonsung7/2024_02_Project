using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������ ���� ����
public enum ItemType
{
    Crystal,            //ũ����Ż
    Plant,              //�Ĺ�
    Bush,               //��Ǯ
    Tree,               //����
}

public class ItemDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;                //������ ���� ����
    private Vector3 lastPosition;                   //�÷��̾��� ������ ��ġ ���� (�ø���� �̵��� ���� ��� �ֺ��� �����ؼ� ������ ȹ��)
    private float moveThreshold = 0.1f;             //�̵� ���� �Ӱ谩 (�÷��̾ �̵��ؾ� �� �ּҰŸ�)
    private CollectibleItem currentNearbyItem;      //���� ���� ������ �ִ� ���� ������ ������  
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;          //���� �� ���� ��ġ�� ������ ��ġ�� ����
        CheckForItems();                            //�ʱ� ������ üũ ����
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        if (Vector3.Distance(lastPosition, transform.position) < checkRadius)
        {
            CheckForItems();                        //�̵��� ������ üũ
            lastPosition = transform.position;      //���� ��ġ�� ������ ��ġ�� ������Ʈ      
        }
        //����� �������� �ְ� EŰ�� ������ �� ������ ����
        if(currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());         //PlayerInventory�� �����Ͽ� ������ ����
        }
    }

    //�ֺ��� ���� ������ �������� �����ϴ� �Լ�
    private void CheckForItems()
    {
        //���� ���� ���� ��� �ݶ��̴��� ã��
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;     //���� ����� �Ÿ��� �ʱⰪ
        CollectibleItem closestItemem = null;       //���� ����� ������ �ʱⰪ

        //�� �ݶ��̴��� �˻��Ͽ� ���� ������ �������� ã��
        foreach( Collider collider in hitCollider )
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();
            if(item != null && item.canCollect) //�������� �ְ� ���� �������� Ȯ��
            {
                float distance = Vector3.Distance(transform.position, item.transform.position); //�Ÿ����
                if(distance > closestDistance)
                {
                    closestDistance = distance;
                    closestItemem = item;
                }
            }
        }

        if( closestItemem != currentNearbyItem)  //���� ����� �������� ����Ǿ��� ���� �޼��� ǥ��
        {
            currentNearbyItem = closestItemem;
            if( currentNearbyItem != null )
            {
                Debug.Log($"[E] Ű�� ���� {currentNearbyItem.itemName}����");             //���ο� ������ ���� �޼��� ǥ��
            }
        }
    }

    //���� ������ �ð������� ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;          //���� ���� ���� ����
        Gizmos.DrawWireSphere(transform.position, checkRadius);     //���� ������ ���� ǥ��
    }
}
