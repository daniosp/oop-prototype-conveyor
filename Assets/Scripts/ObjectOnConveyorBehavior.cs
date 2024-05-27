using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectOnConveyorBehavior : MonoBehaviour
{
    // ===============================================================================================================================
    // This script controls the direction in which the cube must move whenever an output signal is sent from the systemControl script.
    // ===============================================================================================================================

    public float speed;
    public GameObject systemControl;

    void Update()
    {
        if (systemControl.GetComponent<SystemControl>().turnL == true) 
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (systemControl.GetComponent<SystemControl>().turnR == true)
        {
            transform.Translate(-Vector3.back * speed * Time.deltaTime);
        }


    }
}
