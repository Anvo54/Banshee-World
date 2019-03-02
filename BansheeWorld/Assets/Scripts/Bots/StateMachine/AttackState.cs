using System;
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
        if (bot.Target == null)
        {
            bot.ChangeState(new IdleState());
        }

        else
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
    }

    public IEnumerator Attack()
    {
        bot.isAttacking = false;

        while (bot.Distance <= bot.MaxAttackDistance)
        {
            Attack(bot.attackHitBoxes[0]);
            bot.anim.SetTrigger("punch");
            yield return new WaitForSecondsRealtime(bot.anim.GetCurrentAnimatorStateInfo(0).length);

            Attack(bot.attackHitBoxes[1]);
            bot.anim.SetTrigger("kick");
            yield return new WaitForSecondsRealtime(bot.anim.GetCurrentAnimatorStateInfo(0).length);

            yield return new WaitForSeconds(bot.AttackCooldown);
        }
    }

    private void Attack(Collider collider)
    {
        Collider[] cols = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents,
                                                collider.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == bot.transform)
            {
                continue;
            }

            switch (c.name)
            {
                case "Head":
                    bot.attackDamage = 30;
                    break;
                case "Torso":
                    bot.attackDamage = 20;
                    break;
                default:
                    Debug.Log("Unable to indetify witch bodypart was hit. Check your spelling!");
                    break;
            }

            c.transform.root.GetComponent<PlayerHealth>().TakeDamage(bot.attackDamage);

        }
    }
}
