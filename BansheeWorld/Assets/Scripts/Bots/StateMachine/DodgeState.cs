using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : IState
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
        bot.transform.LookAt(bot.Target);
        bot.transform.rotation = Quaternion.Slerp
            (bot.transform.rotation, Quaternion.LookRotation(bot.Target.position - bot.transform.position),
                    bot.RotationSpeed * Time.deltaTime);

        if(bot.isDodging)
        {
            Dodge();
        }

        if(!bot.isDodging)
        {
            bot.isAttacking = true;
            bot.ChangeState(new AttackState());
        }
    }

    private void Dodge()
    {
        bot.anim.SetTrigger("Blocking");
        float animDuration = bot.anim.GetCurrentAnimatorStateInfo(0).length;

        if (bot.timer <= animDuration)
        {
            bot.transform.position -= bot.transform.forward * Time.deltaTime / 5;
            bot.anim.ResetTrigger("Blocking");
        }
        else
        {
            bot.timer = 0;
            bot.isDodging = false;
        }
    }
}
