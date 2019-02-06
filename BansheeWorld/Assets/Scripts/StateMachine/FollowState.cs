using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : IState
{
    private Bot bot;

    public void Enter(Bot bot)
    {
        this.bot = bot;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (bot.Target != null)
        {
            if(bot.Distance < bot.MinDistanceFromPlayer)
            {
                bot.ChangeState(new IdleState());
            }

            bot.transform.LookAt(bot.Target);
            bot.transform.rotation = Quaternion.Slerp
                (bot.transform.rotation, Quaternion.LookRotation(bot.Target.position - bot.transform.position), 
                        bot.RotationSpeed * Time.deltaTime);

            bot.anim.SetTrigger("walk");
            bot.transform.position += bot.transform.forward * bot.Speed * Time.deltaTime;

            if(bot.Distance <= bot.MaxAttackDistance)
            {
                bot.isAttacking = true;
                bot.ChangeState(new AttackState());
            }

            if(Input.GetButtonDown("Fire1"))
            {
                bot.isDodging = true;
                bot.ChangeState(new DodgeState());
            }

            if(Input.GetButtonDown("Fire2"))
            {
                bot.isJumping = true;
                bot.ChangeState(new JumpState());
            }
        }

        else
        {
            bot.ChangeState(new IdleState());
        }
    }

}
