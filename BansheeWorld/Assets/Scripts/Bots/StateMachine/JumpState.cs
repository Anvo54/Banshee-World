using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
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
        if (bot.isJumping)
        {
            Jump();   
        }

        if ((bot.transform.position.y > bot.GroundPosition.y) && !bot.isJumping)
        {
            bot.transform.Translate(Vector3.down * bot.FallSpeed * Time.deltaTime);

            if (bot.transform.position.y < bot.GroundPosition.y)
            {
                bot.transform.position = new Vector3(bot.transform.position.x, bot.GroundPosition.y, bot.transform.position.z);
            }
        }

        if(!bot.isJumping && bot.transform.position.y == bot.GroundPosition.y)
        {
            bot.isAttacking = true;
            bot.ChangeState(new AttackState());
        }
    }

    private void Jump()
    {
        bot.anim.SetTrigger("jump");

        float animDuration = bot.anim.GetCurrentAnimatorStateInfo(0).length;
        
        if (bot.timer <= animDuration /2)
        {
            bot.transform.Translate(Vector3.up * bot.JumpSpeed * Time.deltaTime / 2);
        }
        else
        {
            bot.timer = 0;
            bot.isJumping = false;
        }
    }
}
