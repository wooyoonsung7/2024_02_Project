using static Building;

public class RecipeList
{
    public static CraftingRecipe[] KitchenRecipes = new CraftingRecipe[]
    {
        new CraftingRecipe
        {
            itemName = "��ä ��Ʃ",
            resultltem = ItemType.VegetableStew,
            resultAmount = 1,
            hungerRestoreAmount = 40f,
            requiredItems = new ItemType[] {ItemType.Plant , ItemType.Bush },
            requiredAmounts = new int[] {2,1}
        },
        new CraftingRecipe
        {
            itemName = "���� ������",
            resultltem = ItemType.FruitSalad,
            resultAmount = 1,
            hungerRestoreAmount = 60f,
            requiredItems = new ItemType[] {ItemType.Plant , ItemType.Bush },
            requiredAmounts = new int[] {3,3}
        },
    };
    public static CraftingRecipe[] WorkbenchRecipes = new CraftingRecipe[]
    {
         new CraftingRecipe
        {
            itemName = "���� ŰƮ",
            resultltem = ItemType.RepairKit,
            resultAmount = 1,
            hungerRestoreAmount = 25f,
            requiredItems = new ItemType[] {ItemType.Crystal},
            requiredAmounts = new int[] {3}
        },
    };

}
