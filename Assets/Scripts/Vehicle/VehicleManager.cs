using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public Vehicle[] vehicles;

    public Car car;                         //������Ʈ ����
    public Bicycle bicycle;                 //������Ʈ ����

    float Timer;                            //Ÿ�̸� ����

    void Update()
    {
        //car.Move();
        //bicycle.Move();

        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].Move();
        }
                

        Timer -= Time.deltaTime;            //Ÿ�̸� ī��Ʈ�� �Ѵ�. 
        if(Timer <= 0 )
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                vehicles[i].Horn();
            }
            //car.Horn();            
            //bicycle.Horn();                       

            Timer = 1.0f;                   //1�ʷ� ������ش�.
        }
    }
}
