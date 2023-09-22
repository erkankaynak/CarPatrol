using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotChange : MonoBehaviour
{

    private Car car;
    private bool isTurning;

    private void Start()
    {
        car = gameObject.GetComponent<Car>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) // Example: Rotate right
        {
            if (!isTurning)
                StartCoroutine(Turn(targetDirection: Car.Direction.W, carDirection: Car.Direction.N));

        }
    }


    private IEnumerator Turn(Car.Direction targetDirection, Car.Direction carDirection)
    {
        var targetAngle = 0f;
        var pivotPoint = Vector3.zero;
        var angle = 0f;

        if (carDirection == Car.Direction.N && targetDirection == Car.Direction.W) { targetAngle = 270f; pivotPoint = transform.position + new Vector3(-2f, 0f, 1f); angle = -50f; }
        if (carDirection == Car.Direction.N && targetDirection == Car.Direction.E) { targetAngle = 90f; pivotPoint = transform.position + new Vector3(2f, 0f, 0f); angle = 50f; }
        if (carDirection == Car.Direction.S && targetDirection == Car.Direction.E) { targetAngle = 90f; pivotPoint = transform.position + new Vector3(2f, 0f, -1f); angle = -50f; }
        if (carDirection == Car.Direction.S && targetDirection == Car.Direction.W) { targetAngle = 270f; pivotPoint = transform.position + new Vector3(-2f, 0f, 0f); angle = 50f; }

        if (carDirection == Car.Direction.W && targetDirection == Car.Direction.N) { targetAngle = 0f; pivotPoint = transform.position + new Vector3(0f, 0f, 2f); angle = 50f; }
        if (carDirection == Car.Direction.W && targetDirection == Car.Direction.S) { targetAngle = 180f; pivotPoint = transform.position + new Vector3(-1f, 0f, -2f); angle = -50f; }

        isTurning = true;

        while ((int)transform.rotation.eulerAngles.y != (int)targetAngle)
        {
            transform.RotateAround(pivotPoint, Vector3.up, angle * Time.deltaTime);
            yield return null;
        }

        isTurning = false;
    }
}