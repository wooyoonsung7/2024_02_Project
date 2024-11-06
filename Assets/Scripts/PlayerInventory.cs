using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //������ ������ ������ �����ϴ� ����
    public int crystalConut = 0;        //ũ����Ż ����
    public int plantCount = 0;          //�Ĺ� ����
    public int bushCount = 0;           //��Ǯ ����
    public int treeCount = 0;           //���� ����

    //�������� �߰��ϴ� �Լ�, ������ ������ ���� �ش� ������ ������ ������Ŵ
    public void AddItem(ItemType itemType)
    {
        //������ ������ ���� �ٸ� ���� ����
        switch (itemType)
        {
            case ItemType.Crystal:
                {
                    crystalConut++;     //ũ����Ż ���� ����
                    Debug.Log($"ũ����Ż ȹ��! ���� ����: {crystalConut}");
                    break;
                }
            case ItemType.Plant:
                {
                    plantCount++;     //ũ����Ż ���� ����
                    Debug.Log($"ũ����Ż ȹ��! ���� ����: {plantCount}");
                    break;
                }
            case ItemType.Bush:
                {
                    bushCount++;     //ũ����Ż ���� ����
                    Debug.Log($"ũ����Ż ȹ��! ���� ����: {bushCount}");
                    break;
                }
            case ItemType.Tree:
                {
                    treeCount++;     //ũ����Ż ���� ����
                    Debug.Log($"ũ����Ż ȹ��! ���� ����: {treeCount}");
                    break;
                }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("====�κ��丮====");
        Debug.Log($"ũ����Ż:{crystalConut}��");
        Debug.Log($"�Ĺ�:{plantCount}��");
        Debug.Log($"��Ǯ:{bushCount}��");
        Debug.Log($"����:{treeCount}��");
        Debug.Log("=================");
    }
}
