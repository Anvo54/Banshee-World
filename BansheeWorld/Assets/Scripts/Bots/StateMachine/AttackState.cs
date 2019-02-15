using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
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

        if (bot.isAttacking)
        {
            bot.StartCoroutine(Attack());
        }
        
        if (bot.Distance < bot.MinDistanceFromPlayer)
        {
            bot.isAttacking = false;
            bot.ChangeState(new IdleState());
        }

        if (bot.Distance > bot.MaxAttackDistance)
        {
            bot.isAttacking = false;
            bot.StopAllCoroutines();
            bot.ChangeState(new FollowState());
        }

        if (Input.GetButtonDown("Fire2_P1"))
        {
            bot.isAttacking = false;
            bot.StopAllCoroutines();
            bot.isJumping = true;
            bot.ChangeState(new JumpState());
        }

        if (Input.GetButtonDown("Fire1_P1"))
        {
            bot.isAttacking = false;
            bot.StopAllCoroutines();
            bot.isDodging = true;
            bot.ChangeState(new DodgeState());
        }
    }

    public IEnumerator Attack()
    {
        bot.isAttacking = false;

        while (bot.Distance <= bot.MaxAttackDistance)
        {   
            bot.anim.SetTrigger("punch");
            yield return new WaitForSecondsRealtime(bot.anim.GetCurrentAnimatorStateInfo(0).length);
            bot.anim.SetTrigger("kick");
            yield return new WaitForSecondsRealtime(bot.anim.GetCurrentAnimatorStateInfo(0).length);

            yield return new WaitForSeconds(bot.AttackCooldown);
        }
    }
}
