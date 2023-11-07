using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Car : MonoBehaviour
{
    public enum Direction { N=1, S=-1, W=2, E=-2 };

    public float speed = 5f;
    public float currentSpeed;
    public bool isTurning = false;
    public Direction direction;

    private float slowSpeed = 1f;
    private bool isBehindCar = false;
    


    void Update()
    {
        currentSpeed = speed;
        if (isBehindCar) currentSpeed = slowSpeed;

        if (isTurning == false) MoveCar();


        int layerMask = 1 << 8;



        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            isBehindCar= true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 3f, Color.white);
            Debug.Log("Did not Hit");
            isBehindCar = false;
        }
    }

    void MoveCar() {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }



}
