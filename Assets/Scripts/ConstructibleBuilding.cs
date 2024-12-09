using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("Building Seetings")]
    public BuildingType buildingType;                       //�ǹ� Ÿ�� ����
    public string buildingName;                             //�ǹ� �̸� 
    public int requiredTree = 5;                            //�ǹ� �Ǽ��� �ʿ��� ���� ���� 
    public float constructionTime = 2.0f;                   //�ǹ� �Ǽ� �ð�

    public bool canBuild = true;                            //�Ǽ� ���� ���� 
    public bool isConstructed = false;                      //�Ǽ� �Ϸ� ����

    private Material buildingMaterial;                      //�ǹ��� ���׸��� ���� 

    // Start is called before the first frame update
    void Start()
    {
        buildingMaterial = GetComponent<MeshRenderer>().material;   
        //�ʱ� ���� ���� (������)
        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;
    }

    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstructed) return;                 //�Ǽ� ����,�Ϸ� ���� üũ�Ͽ� ���� ��Ų��. 

        if (inventory.treeCount >= requiredTree)                        //�Ǽ��� �ʿ��� ���� ���ڸ� Ȯ������ 
        {
            inventory.Removeitem(ItemType.Tree, requiredTree);          //�ش� ���� ���� ��ŭ ���� 
            if (FloatingTextManager.Instance != null)
            {
                FloatingTextManager.Instance.Show($"{buildingName} �Ǽ� ����!", transform.position + Vector3.up);
            }
            StartCoroutine(CostructionRoutine());
        }
        else
        {
            if (FloatingTextManager.Instance != null)
            {
                FloatingTextManager.Instance.Show($"������ �����մϴ�! ({inventory.treeCount} / {requiredTree})", transform.position + Vector3.up);
            }
        }
    }

    private IEnumerator CostructionRoutine()
    {
        canBuild = false;
        float timer = 0;
        Color color = buildingMaterial.color;

        while (timer < constructionTime)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 1f, timer / constructionTime);
            buildingMaterial.color = color;
            yield return null;
        }
        isConstructed = true;

        if(FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.Show($"{buildingName} �Ǽ� �Ϸ�!", transform.position + Vector3.up);
        }
    }
}
