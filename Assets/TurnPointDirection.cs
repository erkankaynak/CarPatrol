using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPointDirection : MonoBehaviour
{
    public TurnPoint turnPoint;

    private void OnTriggerEnter(Collider other)
    {
        //turnPoint.CarIsComingFrom(this);
    }
}
