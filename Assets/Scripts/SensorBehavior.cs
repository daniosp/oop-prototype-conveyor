

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBehavior : Sensor
{
    private MeshRenderer sensorRenderer;
    public Material onSensorMaterial;
    public Material offSensorMaterial;

    public GameObject sensorLed;
    private MeshRenderer ledRenderer;
    public Material onLedMaterial;
    public Material offLedMaterial;

    public bool sensorSignal;

    private void Start()
    {
        sensorSignal = false;
        GetMeshComponents();
    }

    private void OnTriggerEnter(Collider other)
    {
        sensorSignal = true;
        ChangeSensorStatus(sensorRenderer, onSensorMaterial, ledRenderer, onLedMaterial);
    }

    private void OnTriggerExit(Collider other)
    {
        sensorSignal = false;
        ChangeSensorStatus(sensorRenderer, offSensorMaterial, ledRenderer, offLedMaterial);
    }

    private void GetMeshComponents() // ABSTRACTION
    {
        sensorRenderer = GetComponent<MeshRenderer>();
        ledRenderer = sensorLed.GetComponent<MeshRenderer>();
    }
}
