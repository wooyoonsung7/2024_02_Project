using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾��� ���¸� ���� 
public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState;                //���� �÷��̾��� ���¸� ��Ÿ���� ���� 
    public PlayerController PlayerController;       //PlayerController�� ���� 

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();    //���� ������Ʈ�� �پ��ִ� PlayerController�� ����       
    }
    void Start()
    {
        //�ʱ� ���¸� IdleState �� ����
        TransitionToState(new IdleState(this));
    }
    // Update is called once per frame
    void Update()
    {
        //���� ���°� �����Ѵٸ� �ش� ������ Update �޼��� ȣ��
        if (currentState != null)
        {
            currentState.Update();
        }
    }
    private void FixedUpdate()
    {
        //���� ���°� �����Ѵٸ� �ش� ������ FixedUpdate �޼��� ȣ��
        if (currentState != null)
        {
            currentState.FixedUpdate();
        }
    }

    //���ο� ���·� ��ȯ �ϴ� �޼��� 
    public void TransitionToState(PlayerState newState)
    {
        //���� ���¿� ���ο� ���°� ���� Ÿ�� �� ���
        if (currentState?.GetType() == newState.GetType())
        {
            return;                     //���� Ÿ���̸� ���¸� ��ȯ ���� �ʰ� ���� 
        }

        //���� ���°� �����Ѵٸ� Exit �޼��带 ȣ�� 
        currentState?.Exit();       //�˻��ؼ� ȣ�� ���� (?)�� IF ���� 

        //���ο� ���·� ��ȯ
        currentState = newState;

        //���ο� ������ Enter �޼��� ȣ�� (���� ����)
        currentState.Enter();

        //�α׿� ���� ��ȯ ������ ���
        //Debug.Log($"���� ��ȯ �Ǵ� ������Ʈ : {newState.GetType().Name}");
    }
}
