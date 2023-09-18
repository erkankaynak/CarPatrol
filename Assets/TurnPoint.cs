using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
        if (isCarTurning == false)
        {
            carComingFrom = direction;
            possibleDirections.Remove(direction);
            Debug.Log("Car is coming from:" + direction);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var car = other.gameObject.GetComponent<Car>();
        if (car == null) return;

        if (car.isTurning == false)
        {
            //isCarTurning = true;

            var direction = possibleDirections[Random.Range(0, possibleDirections.Count - 1)];
            Debug.Log("GOTO:" + direction);

            if (direction == S)
            {
                TurnCar(car, 180);

                //if (carComingFrom == W) {
                //    car.transform.Rotate(Vector3.up, 90);
                //    car.transform.position = this.transform.position;
                //}
                //if (carComingFrom == E) {
                //    car.transform.Rotate(Vector3.up, -90);
                //    car.transform.position = this.transform.position;
                //}
            }

            if (direction == W)
            {
                TurnCar(car, -90);

                //if (carComingFrom == S) {
                //    car.transform.Rotate(Vector3.up, -90);
                //    car.transform.position = this.transform.position;
                //}
                //if (carComingFrom == N) {
                //    car.transform.Rotate(Vector3.up, 90);
                //    car.transform.position = this.transform.position;
                //}
            }

            if (direction == E)
            {
                TurnCar(car, 90);

                //if (carComingFrom == S) {
                //    car.transform.Rotate(Vector3.up, 90);
                //    car.transform.position = this.transform.position;
                //}
                //if (carComingFrom == N) {
                //    car.transform.Rotate(Vector3.up, -90);
                //    car.transform.position = this.transform.position;
                //}
            }

            if (direction == N)
            {
                TurnCar(car, 0);

                //if (carComingFrom == W) {
                //    car.transform.Rotate(Vector3.up, -90);
                //    car.transform.position = this.transform.position;
                //}
                //if (carComingFrom == E) {
                //    car.transform.Rotate(Vector3.up, 90);
                //    car.transform.position = this.transform.position;
                //}
            }

            
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

    private void TurnCar(Car car, float targetAngle)
    {
        if (car.isTurning) return;


        car.isTurning = true;
        car.transform.DORotate(new Vector3(0f, targetAngle, 0f), .5f, RotateMode.Fast).OnComplete(() =>
        {
            car.isTurning = false;
        });

    }

}
