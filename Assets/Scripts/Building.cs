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
        public string itemName;             //������ ������ �̸�
        public ItemType resultltem;         //�����
        public int resultAmount = 1;        //����� ����

        public float hungerRestoreAmount;       //��� ȹ���� (������ ���)
        public float repairAmount;              //������ (���� ŰƮ�� ���)

        public ItemType[] requiredItems;        //�ʿ��� ���� 
        public int[] requiredAmounts;           //�ʿ��� ��� ����
    }
}
