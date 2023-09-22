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

            print("Car's Direction is " + car.direction);
            print("Car is going to " + direction);

            if (car.direction != direction)
                StartCoroutine(TurnCar(car, direction));
        }
    }

    private void _Turn(Car car, Car.Direction direction)
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

    private IEnumerator TurnCar(Car car, Car.Direction targetDirection)
    {
        var targetPosition = Vector3.zero;
        var carDirection = car.direction;
        var destination = "";

        if (carDirection == Car.Direction.N && targetDirection == Car.Direction.W) { targetPosition = car.transform.position + Vector3.forward * 2f; destination = "L"; }
        if (carDirection == Car.Direction.N && targetDirection == Car.Direction.E) { destination = "R"; }

        if (carDirection == Car.Direction.S && targetDirection == Car.Direction.E) { targetPosition = car.transform.position - Vector3.forward * 2f; destination = "L"; }
        if (carDirection == Car.Direction.S && targetDirection == Car.Direction.W) { destination = "R"; }

        if (carDirection == Car.Direction.W && targetDirection == Car.Direction.S) { targetPosition = car.transform.position - Vector3.forward * 2f; destination = "L"; }
        if (carDirection == Car.Direction.W && targetDirection == Car.Direction.N) { destination = "R"; }

        if (carDirection == Car.Direction.E && targetDirection == Car.Direction.N) { targetPosition = car.transform.position + Vector3.forward * 2f; destination = "L"; }
        if (carDirection == Car.Direction.E && targetDirection == Car.Direction.S) { destination = "R"; }

        car.isTurning = true;

        print("Destination : " + destination);

        if (targetPosition != Vector3.zero)
        {
            while (Vector3.Distance(car.transform.position, targetPosition) > 0.1f)
            {
                car.transform.position = Vector3.MoveTowards(car.transform.position, targetPosition, 5f * Time.deltaTime);
                yield return null;
            }
            car.transform.position = targetPosition;
        }

        if (destination == "L")
            yield return StartCoroutine(TurnLeft(car));
        else
            yield return StartCoroutine(TurnRight(car));

        car.direction = targetDirection;
        car.isTurning = false;
    }

    private IEnumerator TurnRight(Car car)
    {
        var targetAngle = 90f;
        var pivotPoint = car.transform.position +  Vector3.right * 2f + Vector3.forward * 0.2f;
        var angle = 50f;

        if (car.direction == Car.Direction.N) { targetAngle = 90f; pivotPoint = car.transform.position - Vector3.right * 2f + Vector3.forward * 0.2f; }
        if (car.direction == Car.Direction.S) { targetAngle = 270f; pivotPoint = car.transform.position - Vector3.right * 2f - Vector3.forward * 0.2f; }
        if (car.direction == Car.Direction.W) { targetAngle = 0f; pivotPoint = car.transform.position + Vector3.right * 2f + Vector3.forward * 0.2f; }
        if (car.direction == Car.Direction.E) { targetAngle = 180f; pivotPoint = car.transform.position - Vector3.right * 2f - Vector3.forward * 0.2f; }

        while ((int)car.transform.rotation.eulerAngles.y != (int)targetAngle)
        {
            car.transform.RotateAround(pivotPoint, Vector3.up, angle * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator TurnLeft(Car car)
    {
        var targetAngle = 270f;
        var pivotPoint = car.transform.position + Vector3.left * 2f + Vector3.forward * 0.2f;
        var angle = -50f;

        if (car.direction == Car.Direction.N) { targetAngle = 270f; pivotPoint = car.transform.position - Vector3.right * 2f + Vector3.forward * 0.2f; }
        if (car.direction == Car.Direction.S) { targetAngle = 90f; pivotPoint = car.transform.position + Vector3.right * 2f - Vector3.forward * 0.2f; }
        if (car.direction == Car.Direction.W) { targetAngle = 180f; pivotPoint = car.transform.position - Vector3.right * 2f - Vector3.forward * 0.2f; }
        if (car.direction == Car.Direction.E) { targetAngle = 0f; pivotPoint = car.transform.position + Vector3.right * 2f + Vector3.forward * 0.2f; }

        while ((int)car.transform.rotation.eulerAngles.y != (int)targetAngle)
        {
            car.transform.RotateAround(pivotPoint, Vector3.up, angle * Time.deltaTime);
            yield return null;
        }
    }
}
