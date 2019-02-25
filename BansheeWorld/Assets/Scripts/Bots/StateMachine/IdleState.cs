using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
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
        if(bot.Target != null)
        {
            bot.anim.SetTrigger("idle");

            bot.transform.LookAt(bot.Target);
            bot.transform.rotation = Quaternion.Slerp
                (bot.transform.rotation, Quaternion.LookRotation(bot.Target.position - bot.transform.position), bot.RotationSpeed * Time.deltaTime);

            bot.transform.position -= bot.transform.forward * bot.Speed *2 * Time.deltaTime;

            if (bot.Distance >= bot.MinDistanceFromPlayer)
            {
                bot.ChangeState(new FollowState());
            }

            if (Input.GetButtonDown("Fire2"))
            {
                bot.StopAllCoroutines();
                bot.ChangeState(new JumpState());
            }

            if (Input.GetButtonDown("Fire1"))
            {
                bot.StopAllCoroutines();
                bot.ChangeState(new DodgeState());
            }
        }

        else 
        {
            bot.anim.SetTrigger("idle");
            // bot.gameObject.SetActive(false);
        }
    }
}
