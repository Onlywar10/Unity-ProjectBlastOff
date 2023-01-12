using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    AudioSource aS;
    Rigidbody rb;
    [SerializeField]float boostSpeed;
    [SerializeField] float rotatePush;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrust;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;
    // Start is called before the first frame update
    void Start()
    {

        aS = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        boostMovement(boostSpeed);
        rotateMovement(rotatePush);
    }
    void boostMovement(float Boost)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            addThrust(Boost);
        }
        else
        {
            aS.Stop();
            mainThrust.Stop();
        } 
    }

    void addThrust(float Boost)
    {
        rb.AddRelativeForce(Vector3.up * Boost * Time.deltaTime);
        if (!aS.isPlaying)
        {
            aS.PlayOneShot(mainEngine);
        }
        if (!mainThrust.isPlaying)
        {
            mainThrust.Play();
        }
    }

    void rotateMovement(float Boost) 
    {
        if (Input.GetKey(KeyCode.A))
        {
            leftMovement(Boost);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rightMovement(Boost);
        }
        else
        {
            rightThrust.Stop();
            leftThrust.Stop();
        }
    }

    private void rightMovement(float Boost)
    {
        rb.freezeRotation = true;
        rb.transform.Rotate(Vector3.back * Boost * Time.deltaTime);
        rb.freezeRotation = false;
        if (!rightThrust.isPlaying)
        {
            rightThrust.Play();
        }
    }

    private void leftMovement(float Boost)
    {
        rb.freezeRotation = true;
        rb.transform.Rotate(Vector3.forward * Boost * Time.deltaTime);
        rb.freezeRotation = false;
        if (!leftThrust.isPlaying)
        {
            leftThrust.Play();
        }
    }
}
