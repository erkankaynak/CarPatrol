using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TurnPoint : MonoBehaviour
{
    public float roadWidth = 3.4f;

    public TurnPointDirection N;
    public TurnPointDirection S;
    public TurnPointDirection W;
    public TurnPointDirection E;


    private TurnPointDirection carComingFrom;

    public bool isCarTurning = false;
    private List<Car.Direction> possibleDirections = new List<Car.Direction>();


    // Start is called before the first frame update
    void Start()
    {
        if (N.isActiveAndEnabled) possibleDirections.Add(Car.Direction.N);
        if (S.isActiveAndEnabled) possibleDirections.Add(Car.Direction.S);
        if (W.isActiveAndEnabled) possibleDirections.Add(Car.Direction.W);
        if (E.isActiveAndEnabled) possibleDirections.Add(Car.Direction.E);

    }

    private void OnTriggerEnter(Collider other)
    {
        var car = other.gameObject.GetComponent<Car>();
        if (car == null) return;

        if (car.isTurning == false)
        {
            //isCarTurning = true;

            var tempPossibleDirections = new List<Car.Direction>();
            foreach(var i in possibleDirections) tempPossibleDirections.Add(i);   
            tempPossibleDirections.Remove((Car.Direction)((int)car.direction*-1));


            var direction = tempPossibleDirections[Random.Range(0, possibleDirections.Count - 1)];

            Debug.Log("Car's Direction is " + car.direction);
            Debug.Log("GOTO:" + direction);

            

            if (direction == Car.Direction.S)
            {
                if (car.direction != Car.Direction.S) TurnCar(car, 180);
                car.direction = Car.Direction.S;
            }

            if (direction == Car.Direction.W)
            {
                if (car.direction != Car.Direction.W) TurnCar(car, -90);
                car.direction = Car.Direction.W;
            }

            if (direction == Car.Direction.E)
            {
                if (car.direction != Car.Direction.E) TurnCar(car, 90);
                car.direction = Car.Direction.E;
            }

            if (direction == Car.Direction.N)
            {
                if (car.direction != Car.Direction.N) TurnCar(car, 0);
                car.direction = Car.Direction.N;
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void TurnCar(Car car, float targetAngle)
    {
        if (car.isTurning) return;
        car.isTurning = true;

        Sequence sequence = DOTween.Sequence();
        sequence.Join(car.transform.DOMove(transform.position, 1f).SetEase(Ease.Linear))
                .Join(car.transform.DORotate(new Vector3(0f, targetAngle, 0f), 1f, RotateMode.Fast));

        sequence.Play().OnComplete(() => car.isTurning = false);
        
    }

}
