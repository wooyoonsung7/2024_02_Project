using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum BuildingType
    {
        CraftingTalbe, 
        Furnace,
        Kitchen,
        Storage
    }
    [System.Serializable]
    public class CraftingRecipe
    {
        public string itemName;             //제작할 아이템 이름
        public ItemType resultltem;         //결과물
        public int resultAmount = 1;        //결과물 개수

        public float hungerRestoreAmount;       //허기 획복량 (음식일 경우)
        public float repairAmount;              //수리량 (수리 키트일 경우)

        public ItemType[] requiredItems;        //필요한 재료들 
        public int[] requiredAmounts;           //필요한 재료 개수
    }
}
