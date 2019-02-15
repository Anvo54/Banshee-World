using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttack : MonoBehaviour
{
    [SerializeField] float coolDown;
    [SerializeField] float waitingTimeForAttackIfNeeded;
    //[SerializeField] float punchingAnimationTime;
    //[SerializeField] float kickingAnimationTime;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        StartCoroutine(AttackPlayer());
    }

    IEnumerator AttackPlayer()
    {
        while (GetComponent<BotMovement>().distance <= GetComponent<BotMovement>().maxAttackDistance)
        {
            yield return new WaitForSeconds(waitingTimeForAttackIfNeeded);
            anim.SetTrigger("punch");
            yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);
            anim.SetTrigger("kick");
            yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

            yield return new WaitForSeconds(coolDown);
            
        }
    }
}
