using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : Sensor
{
    private MeshRenderer switchRenderer;
    public Material onSwitchMaterial;
    public Material offSwitchMaterial;

    public bool signal { get; private set; } // ENCAPSULATION


    private void Start()
    {
        signal = false;
        GetMeshComponents();
    }

    private void OnMouseDown()
    {
        if (signal == false)
        {
            signal = true;
            ChangeSensorStatus( switchRenderer, onSwitchMaterial);
        }
        else if (signal == true)
        {
            signal = false;
            ChangeSensorStatus(switchRenderer, offSwitchMaterial);
        }
    }

    private void GetMeshComponents() // ABSTRACTION
    {
        switchRenderer = GetComponent<MeshRenderer>();
    }
    
}
