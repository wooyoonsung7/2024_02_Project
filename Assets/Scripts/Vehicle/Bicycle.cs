using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ü Ŭ���� : ������
public class Bicycle : Vehicle      //Ż�� Ŭ���� ���
{
    public override void Move()
    {
        base.Move();    //�⺻ �Լ� ������ base Ű����� ���� ��Ų��.
        //������ ���� �߰� ���� 
        transform.Rotate(0, Mathf.Sin(Time.time) * 10 * Time.deltaTime, 0);
    }

    public override void Horn()
    {
        Debug.Log("������ ����");
    }
}
