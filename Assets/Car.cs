using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public enum Direction { N=1, S=-1, W=2, E=-2 };

    public float speed = 5f;
    public float turningSpeed = 5f;
    public float currentSpeed;
    public bool isTurning = false;
    public Direction direction; 


    void Update()
    {
        currentSpeed = isTurning ? turningSpeed : speed ;
        //if (isTurning == false) 
        MoveCar();
    }

    void MoveCar() {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }



}
