using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickText : MonoBehaviour
{

    public CharacterController characterController;

    Vector3 moveVector;
    float verticalVelocity;

    public Collider[] attackHitBoxes;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.G))
        {
            LaunchAttack(attackHitBoxes[0]);
        }
        if (Input.GetKey(KeyCode.H))
        {
            LaunchAttack(attackHitBoxes[1]);
        }
        if (characterController.isGrounded)
        {
            verticalVelocity = -1;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = 10;
            }
            else
            {
                verticalVelocity -= 15 * Time.deltaTime;
            }


            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal") * 5;
            moveVector.y = verticalVelocity             ;

            characterController.Move(moveVector * Time.deltaTime);
        }
        
    }

    public void LaunchAttack(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitBox"));
        foreach (var item in cols)
        {
            if(item.transform.parent.parent == transform)
            {
                continue;
            }
            Debug.Log(item.name);
        }
    }
}
