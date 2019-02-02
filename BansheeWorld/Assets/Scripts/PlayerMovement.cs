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
    [SerializeField] float damage = 0;
    Vector3 moveInput;
    Vector3 moveVelocity;

    Camera mainCamera;

    Rigidbody playerRB;
    Animator playerAnimator;

    // Use this for initialization
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        float lh = Input.GetAxis("Horizontal");
        float lv = Input.GetAxis("Vertical");

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
            playerAnimator.SetLayerWeight(1, 1);
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

    private void Jump()
    {
        if (Input.GetButton("Jump") && grounded == true)
        {
            StartCoroutine("JumpAnim");
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

    private IEnumerable JumpAnim()
    {
        Debug.Log("Started");
        playerAnimator.SetTrigger("Jumping");
        yield return new WaitForSeconds(4f*Time.deltaTime);
        playerAnimator.ResetTrigger("Jumping");
    }
}