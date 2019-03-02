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

    public float MinDistanceFromPlayer = 0.75f;

    public float MaxAttackDistance = 2.1f;

    public float AttackCooldown = 2f;
    public Collider[] attackHitBoxes;
    public float attackDamage;

    private IState currentState;

    public Vector3 GroundPosition;

    public float timer;
    public bool isAttacking;
    public bool isJumping;
    public bool isDodging;
    public Animator anim;

    void Awake()
    {       
        ChangeState(new FollowState());
    }

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        GroundPosition = transform.position;
        isAttacking = false;
        isJumping = false;
        isDodging = false;
    }
        
    public void Update()
    {
        Distance = Vector3.Distance(Target.transform.position, transform.position);

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
