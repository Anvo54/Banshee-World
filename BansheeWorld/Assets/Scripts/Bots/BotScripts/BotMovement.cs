using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public Transform target;
    Vector3 tempTarget;

    [SerializeField] float movementSpeed =2;
    [SerializeField] float rotationSpeed = 5;

    [SerializeField] float minDistanceFromPlayer = 0.75f;
    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] float fallSpeed = 2f;

    public float maxAttackDistance = 2.1f;
    [HideInInspector] public float distance;
        
    Animator anim;

    bool isDodging;
    bool isJumping;
    float timer = 0;
    Vector3 groundPosition;

   
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        groundPosition = transform.position;

    }

    void Update()
    {
        if (!isJumping)
        { 
            transform.LookAt(target);
            transform.rotation = Quaternion.Slerp
                (transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
        }

        distance = Vector3.Distance(transform.position, target.position);
        
        if (distance < minDistanceFromPlayer)
        {
            transform.position -= transform.forward * movementSpeed * Time.deltaTime;
        }

        else
        {
            if (distance > maxAttackDistance && !isDodging && !isJumping)
            {
                anim.SetTrigger("walk");
                transform.position += transform.forward * movementSpeed * Time.deltaTime;
            }

            if (distance <= maxAttackDistance && !isJumping)
            {
                Debug.Log("Attack");
                GetComponent<BotAttack>().Attack();
            }
            else
            {
                StopAllCoroutines();
            }
        }
    
        if (Input.GetButtonDown("Fire1")) // when player punching attack - block
        {
            isDodging = true;
        }
        if (isDodging)
        {
            timer += Time.deltaTime;
            Dodge();        
        }
        
        if(Input.GetButtonDown("Fire2") && !isJumping) // when player kicking attack - jump
        {
            isJumping = true;
        }
        if(isJumping)
        {
            timer += Time.deltaTime;
            Jump();
        }

        if ((transform.position.y > groundPosition.y) && !isJumping)
        {   
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
            if (transform.position.y < groundPosition.y)
            {
                transform.position = new Vector3(transform.position.x, groundPosition.y, transform.position.z);
            }
        }
    }

    private void Jump()
    {
        anim.SetTrigger("jump");
        
        float animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
        
        if(timer <= animDuration)
        {
            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime/2);
        }
        else
        {    
            timer = 0;
            
            isJumping = false;
          
        }
    }

    private void Dodge()
    {
        anim.SetTrigger("block");
        float animDuration = anim.GetCurrentAnimatorStateInfo(0).length;

        if (timer <= animDuration)
        {   
            transform.position -= transform.forward * Time.deltaTime /5;
        }
        else
        {
            timer = 0;
            isDodging = false;
        }
    }
}