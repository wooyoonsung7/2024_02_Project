using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerState  ��� �÷��̾� ������ �⺻�� �Ǵ� �߻� Ŭ����
public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;        //���� �ӽſ� ���� ���� (���� ����)
    protected PlayerController playerController;        //�÷��̾� ��Ʈ�ѷ��� ���� ����

    //������ ���� �ӽŰ� �÷��̾� ��Ʈ�ѷ� ���� �ʱ�ȭ
    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.PlayerController;
    }

    //���� �޼���� : ���� Ŭ�������� �ʿ信 ���� �������̵�
    public virtual void Enter() { }     //���� ���� �� ȣ��
    public virtual void Exit() { }      //���� ���� �� ȣ��
    public virtual void Update() { }    //�� ������ ȣ��
    public virtual void FixedUpdate() { } //���� �ð� �������� ȣ�� (���� �����)

    //���� ��ȯ�� ������ üũ�ϴ� �޼���
    protected void CheckTransitions()
    {
        if (playerController.isGrounded())
        {
            if(Input.GetKeyDown(KeyCode.Space)) //�����̽��� ��������
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                stateMachine.TransitionToState(new MovingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new IdleState(stateMachine));
            }
        }
        else
        {
            //���߿� ������ ���� ��ȯ ����
            if(playerController.GetVertocalVelocity() > 0)
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new FallingState(stateMachine));
            }
        }
    }
}

//IdleState : �÷��̾ ������ �ִ� ����
public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();         //�� �����Ӹ��� ���� ��ȯ ���� üũ
    }
}

//MovingState : �÷��̾ �����̴� ����
public class MovingState : PlayerState
{
    public MovingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();         //�� �����Ӹ��� ���� ��ȯ ���� üũ
    }
    
    public override void FixedUpdate()
    {
        playerController.HandleMovement();          //���� ��� �̵� ó��
    }
}

//JumpingState : �÷��̾ ���� �����϶�
public class JumpingState : PlayerState
{
    public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();         //�� �����Ӹ��� ���� ��ȯ ���� üũ
    }

    public override void FixedUpdate()
    {
        playerController.HandleMovement();          //���� ��� �̵� ó��
    }
}

//FallingState : �÷��̾ ���� ���϶�
public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();         //�� �����Ӹ��� ���� ��ȯ ���� üũ
    }

    public override void FixedUpdate()
    {
        playerController.HandleMovement();          //���� ��� �̵� ó��
    }
}

