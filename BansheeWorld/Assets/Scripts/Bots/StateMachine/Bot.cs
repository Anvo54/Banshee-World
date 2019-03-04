using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpSpeed = 10f;
    public float FallSpeed = 4f;
    public float RotationSpeed =5f;
    public Transform Target;

    public float Distance;

    public float MinDistanceFromPlayer = 0.25f;

    public float MaxAttackDistance = 0.75f;

    public float AttackCooldown = 1f;
    public Collider[] attackHitBoxes;
    public float attackDamage;

    private IState currentState;

    public Vector3 GroundPosition;

    public float timer;
    public bool isAttacking;
    public bool isJumping;
    public bool isDodging;
    public Animator anim;

   
    private void Start()
    {
      
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        GroundPosition = transform.position;
        Speed = 2f;
        MinDistanceFromPlayer = 0.25f;
        MaxAttackDistance = 0.75f;
        AttackCooldown = 0.5f;
        isAttacking = false;
        isJumping = false;
        isDodging = false;

        anim.SetFloat("Running", Speed);

        ChangeState(new IdleState());

    }
        
    public void Update()
    {
        Distance = Vector3.Distance(Target.transform.position, transform.position);

       // Debug.Log(Distance);
        if(isDodging)
        {
            timer += Time.deltaTime;
        }

        if(isJumping)
        {
            timer += Time.deltaTime;
        }

        currentState.Update();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        Debug.Log(currentState);
        currentState.Enter(this);
    }
}
