using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI recipeName;                  //������ �̸�
    public TextMeshProUGUI materialsText;               //�ʿ� ��� �ؽ�Ʈ
    public Button craftButton;                          //���� ��ư

    private CraftingRecipe recipe;                      //������ ������
    private BuildingCrafter crafter;                    //�ǹ��� ���� �ý���
    private PlayerInventory playerInventory;            //�÷��̾� �κ��丮 

    public void Setup(CraftingRecipe recipe, BuildingCrafter crafter)
    {
        this.recipe = recipe;
        this.crafter = crafter; 
        playerInventory = FindObjectOfType<PlayerInventory>();

        recipeName.text = recipe.itemName;                      //������ ���� ǥ��
        UpdateMaterialsText();

        craftButton.onClick.AddListener(OnCraftButtonClicked);  //���� ��ư�� �̺�Ʈ ���� 
    }
    private void UpdateMaterialsText()                      //��� ���� ������Ʈ 
    {
        string materials = "�ʿ� ��� :\n";
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            ItemType item = recipe.requiredItems[i];
            int required = recipe.requiredAmounts[i];
            int has = playerInventory.GetItemCount(item);
            materials += $"{item} : {has}/{required}\n";
        }
        materialsText.text = materials;
    }
    private void OnCraftButtonClicked()                             //���� ��ư Ŭ�� ó�� 
    {
        crafter.TryCraft(recipe, playerInventory);
        UpdateMaterialsText();
    }
}
