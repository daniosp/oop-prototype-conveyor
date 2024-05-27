using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : Sensor
{
    private MeshRenderer switchRenderer;
    public Material onSwitchMaterial;
    public Material offSwitchMaterial;

    public bool sensorSignal;


    private void Start()
    {
        sensorSignal = false;
        GetMeshComponents();
    }

    private void OnMouseDown()
    {
        if (sensorSignal == false)
        {
            sensorSignal = true;
            ChangeSensorStatus( switchRenderer, onSwitchMaterial);
        }
        else if (sensorSignal == true)
        {
            sensorSignal = false;
            ChangeSensorStatus(switchRenderer, offSwitchMaterial);
        }
    }

    private void GetMeshComponents() // ABSTRACTION
    {
        switchRenderer = GetComponent<MeshRenderer>();
    }
    
}
