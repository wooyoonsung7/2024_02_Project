using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ü���� Ŭ���� : �ڵ���
public class Car : Vehicle              //Vehicle ��� 
{
    public override void Horn()         //�߻��Լ� ���� ����
    {
        Debug.Log("������ ����");
    }
}
