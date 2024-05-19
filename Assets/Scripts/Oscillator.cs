using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(period <= Mathf.Epsilon)  // float can be vary by very tiny amount, therefore compare to this rather than 0
        {
            return;
        }
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 2 pi

        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1, sin

        movementFactor = (rawSinWave + 1f) / 2f; // recalculate so it goes from 0 to 1

        Vector3 offset =movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
}
