using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool grounded;
    [SerializeField] Collider[] attackHitBoxes;
    Rigidbody playerRB;
    Animator playerAnimator;

    // Use this for initialization
    void Start () {
        playerRB = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        MoveHorizontally();
        MoveVertically();
        Jump();
        Punch();
        Kick();
    }

    private void Punch()
    {
        if (Input.GetButtonDown("Fire1"))
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
            playerAnimator.SetTrigger("Punch");
            Attack(attackHitBoxes[1]);
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
        }


        
    }

    private void MoveHorizontally()
    {
        float moveSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(moveSpeed, 0, 0);
    }

    private void MoveVertically()
    {
        float moveSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, moveSpeed);
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && grounded == true)
        {
            grounded = false;
            playerRB.AddForce(Vector3.up * jumpForce);
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
