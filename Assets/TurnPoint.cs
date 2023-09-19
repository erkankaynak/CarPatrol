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
                if (carComingFrom != N) TurnCar(car, 180, new Vector3(-0.5f, 0, -3f));

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
                if (carComingFrom != E) TurnCar(car, -90, new Vector3(-2f, 0, 1.2f));

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
                if (carComingFrom != W) TurnCar(car, 90, new Vector3(2f, 0, -0.7f));


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
                if (carComingFrom != S) TurnCar(car, 0, new Vector3(1.4f, 0, 3f));

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

    private void TurnCar(Car car, float targetAngle, Vector3 position)
    {
        if (car.isTurning) return;

        

        car.isTurning = true;

        Sequence sequence = DOTween.Sequence();
        sequence.Join(car.transform.DOMove(transform.position + position, 1f).SetEase(Ease.Linear))
                .Join(car.transform.DORotate(new Vector3(0f, targetAngle, 0f), 1f, RotateMode.Fast));

        sequence.Play().OnComplete(() => car.isTurning = false);
        
    }

}
