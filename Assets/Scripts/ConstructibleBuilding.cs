using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Building;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("Building Seetings")]
    public BuildingType buildingType;
    public string buildigName;
    public int requiredTree = 5;
    public float constructionTime = 2.0f;

    public bool canBuild = true;
    public bool isConstructed = false;

    private Material buildingMaterial;

    void Start()
    {
        buildingMaterial = GetComponent<MeshRenderer>().material;
        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;

    }
    private IEnumerator CostructionRoutine()
    {
        canBuild = false;
        float time = 0;
        Color color = buildingMaterial.color;

        while (time < constructionTime) {
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(0.5f, 1f, time / constructionTime);
                buildingMaterial.color = color;
                yield return null;
            }
            isConstructed = true;

            if (FloatingTextManager.Instance != null)
            {
                FloatingTextManager.Instance.Show($"{buildigName} 건설완료!", transform.position + Vector3.up);
            }
        }
    }
    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstructed) return;

        if (inventory.treeCount >= requiredTree)
        {
            inventory.Removeitem(ItemType.Tree, requiredTree);
            if (FloatingTextManager.Instance == null)
            {
                FloatingTextManager.Instance.Show($"{buildigName} 건설 시작!", transform.position + Vector3.up);
            }
            StartCoroutine(CostructionRoutine());
        }
        else
        {
            if (FloatingTextManager.Instance == null)
            {
                FloatingTextManager.Instance.Show($"나무가 부족합니다! ({inventory.treeCount} / {requiredTree})", transform.position + Vector3.up);
            }
        }
    }
}

