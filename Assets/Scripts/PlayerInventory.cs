using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //각각의 아이템 개수를 저장하는 변수
    public int crystalConut = 0;        //크리스탈 개수
    public int plantCount = 0;          //식물 개수
    public int bushCount = 0;           //수풀 개수
    public int treeCount = 0;           //나무 개수

    //아이템을 추가하는 함수, 아이템 종류에 따라 해당 아이템 개수를 증가시킴
    public void AddItem(ItemType itemType)
    {
        //아이템 종류에 따른 다른 동작 수행
        switch (itemType)
        {
            case ItemType.Crystal:
                {
                    crystalConut++;     //크리스탈 개수 증가
                    Debug.Log($"크리스탈 획득! 현재 개수: {crystalConut}");
                    break;
                }
            case ItemType.Plant:
                {
                    plantCount++;     //크리스탈 개수 증가
                    Debug.Log($"크리스탈 획득! 현재 개수: {plantCount}");
                    break;
                }
            case ItemType.Bush:
                {
                    bushCount++;     //크리스탈 개수 증가
                    Debug.Log($"크리스탈 획득! 현재 개수: {bushCount}");
                    break;
                }
            case ItemType.Tree:
                {
                    treeCount++;     //크리스탈 개수 증가
                    Debug.Log($"크리스탈 획득! 현재 개수: {treeCount}");
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
        Debug.Log("====인벤토리====");
        Debug.Log($"크리스탈:{crystalConut}개");
        Debug.Log($"식물:{plantCount}개");
        Debug.Log($"수풀:{bushCount}개");
        Debug.Log($"나무:{treeCount}개");
        Debug.Log("=================");
    }
}
