using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 100f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ThrustProcess();
        RotationProcess();
    }

    void ThrustProcess()
    {
        if (Input.GetKey(KeyCode.Space)) //avoid using string because naming could be a headache
        {
     
            rb.AddRelativeForce(Vector3.up  * mainThrust * Time.deltaTime ); // alternative way to write rb.AddRelativeForce(0, 1, 0)

        }

        
    }

    void RotationProcess()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRocket(rotateThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRocket(-rotateThrust);
        }
    }

    void RotateRocket(float rotationValue)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
