using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectOnConveyorBehavior : MonoBehaviour
{
    public float speed; // Object speed on the conveyor
    private bool isOnConveyor;

    private SystemControl sc_script;

    //  The follwoing methods control the direction in which the cube must move whenever an output signal is sent from the systemControl script.
    private void Start()
    {
        sc_script = GameObject.Find("SystemControl").GetComponent<SystemControl>();
    }

    void Update()
    {
        if (sc_script.turnL == true && isOnConveyor == true)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (sc_script.turnR == true && isOnConveyor == true)
        {
            transform.Translate(-Vector3.back * speed * Time.deltaTime);
        }
    }

    // The following methods allow the object to be clicked-on and dragged throughout the world

    Vector3 mousePosition;
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
        transform.position = new Vector3(0, 2f, Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition).z);
        isOnConveyor = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyor"))
        {
            isOnConveyor = true;
        }
    }
}
