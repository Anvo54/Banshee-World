using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), orbitSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
