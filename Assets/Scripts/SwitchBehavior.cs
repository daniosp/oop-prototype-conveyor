using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour
{
    private MeshRenderer SwitchRenderer;
    public Material onSwitchMaterial;
    public Material offSwitchMaterial;

    public bool sensorSignal;


    private void Start()
    {
        SwitchRenderer = GetComponent<MeshRenderer>();
        sensorSignal = false;
    }

    private void OnMouseDown()
    {
        if (sensorSignal == false)
        {
            SwitchRenderer.material = onSwitchMaterial;
            sensorSignal = true;
            
        }
        else if (sensorSignal == true)
        {
            SwitchRenderer.material = offSwitchMaterial;
            sensorSignal = false;
        }

    }
}
