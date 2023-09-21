using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotChange : MonoBehaviour
{
    public Transform pivotPoint; // Assign the pivot point GameObject in the Inspector
    public float rotationSpeed = 45.0f; // Adjust the rotation speed as needed

    private void Update()
    {
        // Rotate the car around the custom pivot point
        if (Input.GetKey(KeyCode.A)) // Example: Rotate left
        {
            RotateAroundPivot(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D)) // Example: Rotate right
        {
            RotateAroundPivot(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // Function to rotate an object around a custom pivot point
    private void RotateAroundPivot(Vector3 axis, float angle)
    {
        Vector3 pivotPosition = pivotPoint.position;

        // Move the object to the pivot point
        transform.position = Quaternion.AngleAxis(angle, axis) * (transform.position - pivotPosition) + pivotPosition;

        // Rotate the object
        transform.rotation *= Quaternion.AngleAxis(angle, axis);
    }
}
