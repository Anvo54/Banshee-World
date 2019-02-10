using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //
    [SerializeField] List<InputAxesSO> inputAxes = new List<InputAxesSO>();

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool grounded;
    [SerializeField] Collider[] attackHitBoxes;
    public float damage = 0;

    Rigidbody playerRB;
    Animator playerAnimator;

    //
    GameObject GameSceneManagerRef;
    GameSceneManager gameSceneManager;

    int playerIndex;
    
    void Start ()
    {
        GameSceneManagerRef = GameObject.FindGameObjectWithTag("GameManager");
        gameSceneManager = GameSceneManagerRef.GetComponent<GameSceneManager>();

        for (int i = 0; i < gameSceneManager.players.Length; i++)
        {
            if (gameObject == gameSceneManager.players[i].instance)
            {
//                playerIndex = i;
                playerIndex = gameSceneManager.players[i].playerIndex;
                
            }
        }


        playerRB = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
	
    internal void ResetMaxHealth()
    {
        Debug.Log("SET current health to max health");
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        MoveHorizontally();
        MoveVertically();
       // Jump();
        Punch();
        Kick();
    }

    private void Punch()
    {
        if (Input.GetButtonDown(inputAxes[playerIndex].fire1))
        {
            playerAnimator.SetTrigger("Punch");
            Attack(attackHitBoxes[0]);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            playerAnimator.ResetTrigger("Punch");
        }
    }

    private void Kick()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Kick");
          //  playerAnimator.SetLayerWeight(1, 1);
            playerAnimator.SetTrigger("Punch");
            Attack(attackHitBoxes[0]);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            playerAnimator.ResetTrigger("Punch");
        }
    }

    private void Attack(Collider col)
    {  
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach(Collider c in cols)
        {
            if(c.transform.root == transform)
            {
                continue;
            }
            Debug.Log(c.name);

            switch (c.name)
            { case "Head":
                    damage = 30;
                    break;
                case "Torso":
                    damage = 20;
                    break;
                default:
                    Debug.Log("Unable to indetify witch bodypart was hit. Check your spelling!");
                    break;
            }
        }
    }

    private void MoveHorizontally()
    {
        float moveSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(moveSpeed, 0, 0);
    //    playerAnimator.SetFloat("Running", Mathf.Abs(moveSpeed*2));
    }

    private void MoveVertically()
    {
        float moveSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, moveSpeed);
     //   playerAnimator.SetFloat("Running", Mathf.Abs(moveSpeed*2));
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && grounded == true)
        {
            playerAnimator.SetTrigger("Jumping");
            grounded = false;
            playerRB.AddForce(Vector3.up * jumpForce);
        }
        else
        {
            playerAnimator.ResetTrigger("Jumping");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
    }

}
