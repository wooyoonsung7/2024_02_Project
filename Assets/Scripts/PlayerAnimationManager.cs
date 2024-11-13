using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;                           //�ִϸ��̼��� �����ϴ� ������Ʈ
    public PlayerStateMachine stateMachine;             //����ڰ� ������ ���� ���� 

    //�ִϸ����� �Ķ���� �̸����� ����� ����
    private const string PARAM_IS_MOVING = "IsMoving";
    private const string PARAM_IS_RUNNING = "IsRunning";
    private const string PARAM_IS_JUMPING = "IsJumping";
    private const string RARAM_IS_FALLING = "IsFalling";
    private const string PARAM_ATTACK_TRIGGER = "Attack";

    void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        //���� ���¿� ���� �ִϸ��̼� �Ķ���� ���� 
        if (stateMachine.currentState != null)
        {
            //��� bool �Ķ���͸� �ʱ�ȭ
            ResetAllBoolParameters();

            //���� ���¿� ���� �ش��ϴ� �ִϸ��̼� �Ķ���͸� ���� 
            switch (stateMachine.currentState)
            {
                case IdleState:
                    //Idle ���´� ��� �Ķ���Ͱ� false�� ���� 
                    break;
                case MovingState:
                    animator.SetBool(PARAM_IS_MOVING, true);
                    //�޸��� �Է� Ȯ��
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        animator.SetBool(PARAM_IS_RUNNING, true);
                    }
                    break;
                case JumpingState:
                    animator.SetBool(PARAM_IS_JUMPING, true);
                    break;
                case FallingState:
                    animator.SetBool(RARAM_IS_FALLING, true);
                    break;
            }
        }
    }

    //���� �ִϸ��̼� Ʈ����
    public void TriggerAttack()
    {
        animator.SetTrigger(PARAM_ATTACK_TRIGGER);
    }   

    //��� bool �Ķ���͸� �ʱ�ȭ ���ִ� �Լ� 
    private void ResetAllBoolParameters()
    {
        animator.SetBool(PARAM_IS_MOVING, false);
        animator.SetBool(PARAM_IS_RUNNING, false);
        animator.SetBool(PARAM_IS_JUMPING, false);
        animator.SetBool(RARAM_IS_FALLING, false);
    }
}
