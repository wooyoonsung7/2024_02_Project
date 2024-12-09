using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCrafter : MonoBehaviour
{
    public BuildingType buildingType;                       //�ǹ� Ÿ��
    public CraftingRecipe[] recipes;                        //��� ������ ���ǽ� �迭
    private SurvivalStats survivalStats;                    //���� ���� ����
    private ConstructibleBuilding building;                 //�ǹ� ���� ���� 

    // Start is called before the first frame update
    void Start()
    {
        survivalStats = FindObjectOfType<SurvivalStats>();
        building = GetComponent<ConstructibleBuilding>();   

        switch(buildingType)                                    //�ǹ� Ÿ�Կ� ���� ������ ���� 
        {
            case BuildingType.Kitchen:
                recipes = RecipeList.KitchenRecipes;
                break;
            case BuildingType.CraftingTable:
                recipes = RecipeList.WorkbenchRecipes;
                break;
        }
    }

    public void TryCraft(CraftingRecipe recipe , PlayerInventory inventory)     //������ ���� �õ� 
    {   
        if(!building.isConstructed)                     //�Ǽ��� �Ϸ���� �ʾҴٸ� ���� �Ұ�
        {
            FloatingTextManager.Instance?.Show("�Ǽ��� �Ϸ� ���� �ʾҽ��ϴ�!", transform.position + Vector3.up);
            return;
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)       //��� üũ 
        {
            if (inventory.GetItemCount(recipe.requiredItems[i]) < recipe.requiredAmounts[i])
            {
                FloatingTextManager.Instance?.Show("��ᰡ �����մϴ�. !" , transform.position + Vector3.up);
                return;
            }
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)       //��� �Һ�
        {
            inventory.Removeitem(recipe.requiredItems[i], recipe.requiredAmounts[i]);
        }

        survivalStats.DamageOnCrafting();                   //���ֺ� ������ ����

        inventory.AddItem(recipe.resultItem, recipe.resultAmount);      //������ ����
        FloatingTextManager.Instance?.Show($"{ recipe.itemName } ���� �Ϸ�!" , transform.position + Vector3.up);
    }

}
