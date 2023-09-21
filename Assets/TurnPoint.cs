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

    private float turnDuration = .6f;
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

            if (car.direction != direction)
                TurnCar(car, direction);
        }
    }

    private void TurnCar(Car car, Car.Direction direction)
    {
        Vector3 targetPosition = Vector3.zero;
        float angle = 0f;

        if (direction == Car.Direction.W) { angle = -90f; targetPosition = new Vector3(car.transform.position.x, car.transform.position.y, transform.position.z + 1f); }
        if (direction == Car.Direction.E) { angle = 90f; targetPosition = new Vector3(car.transform.position.x, car.transform.position.y, transform.position.z - 1f); }
        if (direction == Car.Direction.N) { angle = 0f; targetPosition = new Vector3(transform.position.x + 1f, car.transform.position.y, car.transform.position.z); }
        if (direction == Car.Direction.S) { angle = 180f; targetPosition = new Vector3(transform.position.x - 1f, car.transform.position.y, car.transform.position.z); }

        if (car.isTurning) return;
        car.isTurning = true;

        Sequence sequence = DOTween.Sequence();
        sequence.Join(car.transform.DOMove(targetPosition, 1f).SetEase(Ease.Linear))
                .Join(car.transform.DORotate(new Vector3(0f, angle, 0f), 1f, RotateMode.Fast));

        sequence.Play().OnComplete(() => {
            car.isTurning = false;
            car.direction = direction;
        });
        
    }

}
