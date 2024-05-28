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

    Vector3 mousePosition;

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

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3 (0,2f,Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition).z);
    }
}
