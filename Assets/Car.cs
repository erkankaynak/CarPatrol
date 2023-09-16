using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCar();
    }

    void MoveCar() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
