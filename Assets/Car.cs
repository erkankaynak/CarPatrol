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

    //public IEnumerator GotoTarget(Vector3 _targetPosition)
    //{
    //    Debug.Log("Goto Target");
    //    targetPosition = new Vector3(_targetPosition.x, transform.position.y, _targetPosition.y);
    //    float distance = Vector3.Distance(transform.position, targetPosition.Value);
    //
    //    while (distance > .5f)
    //    {
    //        //transform.position = Vector3.MoveTowards(transform.position, targetPosition.Value, speed * Time.deltaTime);
    //        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    //        distance = Vector3.Distance(transform.position, targetPosition.Value);
    //        yield return null;
    //    }
    //
    //    Debug.Log("Ok Target");
    //
    //    transform.position = targetPosition.Value;
    //    targetPosition = null;
    //}
}
