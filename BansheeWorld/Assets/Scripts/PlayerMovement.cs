using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool grounded;
    [SerializeField] Collider[] attackHitBoxes;
    [SerializeField] GameObject Hit;
    [SerializeField] float hitForce=300;

    [SerializeField] float damage = 0;

    internal float headDamage = 30;
    internal float bodyDamage = 20;

    [SerializeField] string horizonal_Axis;
    [SerializeField] string Vertical_Axis;
    [SerializeField] string jump;
    [SerializeField] string fire1;
    [SerializeField] string fire2;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2;

    PlayerStats stats;
    public int index;
    private int pnum;
    Vector3 moveInput;
    Vector3 moveVelocity;

    

    Camera mainCamera;

    Rigidbody playerRB;
    Animator playerAnimator;


    void Start()
    {
        //index = gameObject.GetComponent<PlayerScriptKim>().playerIndex;
        pnum = PlayerNumber();
        playerRB = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        playerAnimator = GetComponent<Animator>();
        fire1 = "J" + pnum + "Fire1";
        fire2 = "J" + pnum + "Fire2";
        horizonal_Axis = "J" + pnum + "Horizontal";
        Vertical_Axis = "J" + pnum + "Vertical";
        jump = "J" + pnum + "Jump";

    }

    private void Update()
    {

        
        float lh = Input.GetAxis(horizonal_Axis);
        float lv = Input.GetAxis(Vertical_Axis);
        Animate();

        moveInput = new Vector3(lh, 0f, lv);
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;

        Quaternion cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
        Vector3 lookToward = cameraRelativeRotation * moveInput;

        if(moveInput.sqrMagnitude > 0)
        {
            Ray lookRay = new Ray(transform.position, lookToward);
            transform.LookAt(lookRay.GetPoint(1));
        }

        moveVelocity = transform.forward * speed * moveInput.sqrMagnitude;
        moveVelocity.y = playerRB.velocity.y;
    }

    private void Animate()
    {
        playerAnimator.SetFloat("Running", playerRB.velocity.sqrMagnitude);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerRB.velocity = moveVelocity;
        Jump();
        Attack1();
        Attack2();
        Kick();     
    }
    private void Attack1()
    {
        if (Input.GetButtonDown(fire1))
        {
            Debug.Log("Fire1");
            playerAnimator.SetTrigger("Attack1");
            Attack(attackHitBoxes[0]);
        }
        if (Input.GetButtonUp(fire1))
        {
            playerAnimator.ResetTrigger("Attack1");
        }
    }
    private void Attack2()
    {
        if (Input.GetButtonDown(fire2))
        {
            playerAnimator.SetTrigger("Attack2");
            Attack(attackHitBoxes[0]);
        }
        if (Input.GetButtonUp(fire2))
        {
            playerAnimator.ResetTrigger("Attack2");
        }
    }



    private void Kick()
    {
        if (Input.GetButtonDown(fire2))
        {
            Debug.Log("Kick");
            playerAnimator.SetTrigger("Attack1");
            Attack(attackHitBoxes[1]);
        }
        if (Input.GetButtonUp(fire2))
        {
            playerAnimator.ResetTrigger("Attack1");
        }
    }

    private void Attack(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }
            Debug.Log(c.name);



            switch (c.name)
            {
                case "Head":
                    damage = headDamage;
                    c.transform.root.GetComponent<Rigidbody>().AddExplosionForce(hitForce,transform.position, 10f);
                    //Instantiate(Hit,transform.position,Quaternion.identity);
                    break;
                case "Torso":
                    damage = bodyDamage;
                    //Instantiate(Hit, transform.position, Quaternion.identity);
                    break;
                default:
                    Debug.Log("Unable to indetify witch bodypart was hit. Check your spelling!");
                    break;
            }
            if (GameStaticValues.multiplayer)
            {
                c.transform.root.GetComponent<PlayerHealth>().TakeDamage(damage);
            }

            else
            {
                c.transform.root.GetComponent<BotHealth>().AddjustCurrentHealth(damage);
            }

        }
    }

    private void Jump()
    {
        if (Input.GetButton(jump) && grounded)
        {
            playerAnimator.SetTrigger("Jump");
            playerRB.AddForce(Vector3.up * jumpForce*2);
            grounded = false;
            
            playerRB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1) * Time.deltaTime;
            } if(playerRB.velocity.y >0 && !Input.GetButton(jump))
            {
                playerRB.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
   
            }
        playerAnimator.ResetTrigger("Jump");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("it's the ground!!");
            grounded = true;
        }
    }




    int PlayerNumber()
    {
        if (GameObject.FindGameObjectWithTag("Spawner1").transform.position.x == transform.position.x)
        {
            return 1;
        }
        else return 2;
    }

}