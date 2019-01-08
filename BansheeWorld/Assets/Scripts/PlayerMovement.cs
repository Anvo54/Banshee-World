using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool grounded;
    Rigidbody playerRB;

    // Use this for initialization
    void Start () {
        playerRB = GetComponent<Rigidbody>();   
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveHorizontally();
        Jump();
        Punch();
        Kick();
    }

    private void Kick()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Kick");
        }
    }

    private void Punch()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Punch");
        }
        
    }

    private void MoveHorizontally()
    {
        float moveSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(moveSpeed, 0, 0);
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
