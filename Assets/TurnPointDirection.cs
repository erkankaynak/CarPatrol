using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPointDirection : MonoBehaviour
{
    private void Start()
    {
        var renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }
}
