using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5f;
    public float turningSpeed = 5f;

    public bool isTurning = false;

    public  float currentSpeed;


    void Update()
    {
        currentSpeed = isTurning ? turningSpeed : speed ;
        if (isTurning==false) MoveCar();
    }

    void MoveCar() {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }



}
