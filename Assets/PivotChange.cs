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
                StartCoroutine(TurnCar(targetDirection: Car.Direction.W));

        }
    }

    private IEnumerator TurnCar(Car.Direction targetDirection)
    {
        var targetPosition = Vector3.zero;
        var carDirection = Car.Direction.S;
        var destination = "";

        if (carDirection == Car.Direction.N && targetDirection == Car.Direction.W) { targetPosition = transform.position + Vector3.forward * 2f; destination = "L"; }
        if (carDirection == Car.Direction.N && targetDirection == Car.Direction.E) { destination = "R"; }

        if (carDirection == Car.Direction.S && targetDirection == Car.Direction.E) { targetPosition = transform.position + Vector3.forward * 2f; destination = "L"; }
        if (carDirection == Car.Direction.S && targetDirection == Car.Direction.W) { destination = "R"; }

        if (targetPosition != Vector3.zero)
        {
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPosition;
        }

        if (destination == "L")
            yield return StartCoroutine(TurnLeft());
        else
            yield return StartCoroutine(TurnRight());
    }

    private IEnumerator TurnRight()
    {
        var targetAngle = 90f;
        var pivotPoint = transform.position + Vector3.right * 2f + Vector3.forward * 0.2f;
        var angle = 50f;

        while ((int)transform.rotation.eulerAngles.y != (int)targetAngle)
        {
            transform.RotateAround(pivotPoint, Vector3.up, angle * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator TurnLeft()
    {
        var targetAngle = 270f;
        var pivotPoint = transform.position + Vector3.left * 2f + Vector3.forward * 0.2f;
        var angle = -50f;

        while ((int)transform.rotation.eulerAngles.y != (int)targetAngle)
        {
            transform.RotateAround(pivotPoint, Vector3.up, angle * Time.deltaTime);
            yield return null;
        }
    }
}