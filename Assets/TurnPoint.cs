using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPoint : MonoBehaviour
{
    public TurnPointDirection N;
    public TurnPointDirection S;
    public TurnPointDirection W;
    public TurnPointDirection E;

    private TurnPointDirection carComingFrom;

    private bool isCarTurning = false;
    private List<TurnPointDirection> possibleDirections = new List<TurnPointDirection>(); 

    // Start is called before the first frame update
    void Start()
    {
        if (N.isActiveAndEnabled) possibleDirections.Add(N);
        if (S.isActiveAndEnabled) possibleDirections.Add(S);
        if (W.isActiveAndEnabled) possibleDirections.Add(W);
        if (E.isActiveAndEnabled) possibleDirections.Add(E);

    }

    public void CarIsComingFrom(TurnPointDirection direction)
    {
        carComingFrom = direction;
        possibleDirections.Remove(direction);
        Debug.Log("Car is coming from:" + direction);
    }


    private void OnTriggerEnter(Collider other)
    {
        var car = other.gameObject.GetComponent<Car>();
        if (car == null) return;

        if (isCarTurning == false)
        {
            isCarTurning = true;

            var direction = possibleDirections[Random.Range(0, possibleDirections.Count - 1)];
            Debug.Log("GOTO:" + direction);

            if (direction == S)
            {
                if (carComingFrom == W) car.transform.Rotate(Vector3.up, 90);
                if (carComingFrom == E) car.transform.Rotate(Vector3.up, -90);
            }

            if (direction == W)
            {
                if (carComingFrom == S) car.transform.Rotate(Vector3.up, -90);
                if (carComingFrom == N) car.transform.Rotate(Vector3.up, 90);
            }

            if (direction == E)
            {
                if (carComingFrom == S) car.transform.Rotate(Vector3.up, 90);
                if (carComingFrom == N) car.transform.Rotate(Vector3.up, -90);
            }

            if (direction == N)
            {
                if (carComingFrom == W) car.transform.Rotate(Vector3.up, -90);
                if (carComingFrom == E) car.transform.Rotate(Vector3.up, 90);
            }

            car.transform.position = this.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Car>() != null)
        {
            possibleDirections.Add(carComingFrom);
            isCarTurning = false;
        }
    }
}
