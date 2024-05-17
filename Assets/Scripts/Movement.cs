using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainThrustAudio;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb;
    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioData = GetComponent<AudioSource>();
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
            StartThrust();

        }
        else
        {
            // issue, it will stop all audio including crash audio
            StopThrust();

        }


    }
    void RotationProcess()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {
            StopRotateParticles();
        }


    }


    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // alternative way to write rb.AddRelativeForce(0, 1, 0)

        if (!audioData.isPlaying)
        {
            audioData.PlayOneShot(mainThrustAudio);
        }

        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }
    void StopThrust()
    {
        audioData.Stop();
        mainThrustParticles.Stop();
    }

    void RotateRocket(float rotationValue)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over


    }

    void RotateLeft()
    {
        RotateRocket(rotateThrust);

        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }


    void RotateRight()
    {
        RotateRocket(-rotateThrust);

        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    

    void StopRotateParticles()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    
}
