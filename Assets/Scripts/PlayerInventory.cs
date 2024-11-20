using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats;                //Ŭ���� ����

    //������ ������ ������ �����ϴ� ���� 
    public int crystalCount = 0;            //ũ����Ż ����
    public int plantCount = 0;              //�Ĺ� ����
    public int bushCount = 0;               //��Ǯ ����
    public int treeCount = 0;               //���� ���� 

    public void Start()
    {
        survivalStats = GetComponent<SurvivalStats>();
    }

    public void UseItem(ItemType itemType)
    {
        if (GetItemCount(itemType) <= 0)
        {
            return;
        }

        switch (itemType)
        {
            case ItemType.VegetableStew:
                Removeitem(ItemType.VegetableStew, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[0].hungerRestoreAmount);
                break;
            case ItemType.FruitSalad:
                Removeitem(ItemType.FruitSalad, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[1].hungerRestoreAmount);
                break;
            case ItemType.RepairKit:
                Removeitem(ItemType.RepairKit, 1);
                survivalStats.EatFood(RecipeList.WorkbenchRecipes[0].repairAmount);
                break;
        }
    }

    //���� �������� �Ѳ����� ȹ��
    public void AddItem(ItemType itemType, int amount)
    {
        //amount ��ƴ ������ Additme ȣ��
        for(int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }

    //�������� �߰��ϴ� �Լ�, ������ ������ ���� �ش� �������� ������ ������Ŵ
    public void AddItem(ItemType itemType)
    {
        //������ ������ ���� �ٸ� ���� ���� 
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;  //ũ����Ż ���� ����
                Debug.Log($"ũ����Ż ȹ��! ���� ���� :{crystalCount}");           //���� ũ����Ż ���� ��� 
                break;
            case ItemType.Plant:
                plantCount++;  //�Ĺ� ���� ����
                Debug.Log($"�Ĺ� ȹ��! ���� ���� :{plantCount}");           //���� �Ĺ� ���� ��� 
                break;
            case ItemType.Bush:
                bushCount++;  //��Ǯ ���� ����
                Debug.Log($"��Ǯ ȹ��! ���� ���� :{bushCount}");           //���� ��Ǯ ���� ��� 
                break;
            case ItemType.Tree:
                treeCount++;  //���� ���� ����
                Debug.Log($"���� ȹ��! ���� ���� :{treeCount}");           //���� ���� ���� ��� 
                break;
        }
    }

    //�������� �����ϴ� �Լ� 
    public bool Removeitem(ItemType itemType, int amount = 1)
    {
        //������ ������ ���� �ٸ� ���� ���� 
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"ũ����Ż {amount} ���! ���� ���� :{crystalCount}");
                    return true;
                }
                break;
            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    plantCount -= amount;
                    Debug.Log($"�Ĺ� {amount} ���! ���� ���� :{crystalCount}");
                    return true;
                }
                break;
            case ItemType.Bush:
                if (plantCount >= amount)
                {
                    plantCount -= amount;
                    Debug.Log($"��Ǯ {amount} ���! ���� ���� :{crystalCount}");
                    return true;
                }
                break;
            case ItemType.Tree:
                if (plantCount >= amount)
                {
                    plantCount -= amount;
                    Debug.Log($"���� {amount} ���! ���� ���� :{crystalCount}");
                    return true;
                }
                break;
        }
        Debug.Log($"{itemType} �������� �����մϴ�");
        return false;
    }
    public int GetItemCount(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
            case ItemType.Plant:
                return plantCount;
            case ItemType.Bush:
                return bushCount;
            case ItemType.Tree:
                return treeCount;
            default:
                return 0;
        }
    }

    void Update()
    {
        // I Ű�� ������ �� �κ��丮 �α� ������ ������ 
        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();                    //�κ��丮 ��� �Լ� ȣ�� 
        }
    }

    private void ShowInventory()
    {
        Debug.Log("====�κ��丮====");
        Debug.Log($"ũ����Ż:{crystalCount}��");        //ũ����Ż ���� ���
        Debug.Log($"�Ĺ�:{plantCount}��");             //�Ĺ� ���� ���
        Debug.Log($"��Ǯ:{bushCount}��");              //��Ǯ ���� ���
        Debug.Log($"����:{treeCount}��");              //���� ���� ���
        Debug.Log("================");
    }
}
