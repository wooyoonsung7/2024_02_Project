using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߻� Ŭ���� : Ż�� 
public abstract class Vehicle : MonoBehaviour
{
    public float speed = 10f;                   //�̵� �ӵ� ���� ����

    // ���� �Լ� : �̵�
    public virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);      //������ �ش� �ӵ���ŭ �����δ�. 
    }

    // �߻� �Լ� : ���� 
    public abstract void Horn();                                            //���� �Լ��� ���� �Ѵ�. 
 
}
