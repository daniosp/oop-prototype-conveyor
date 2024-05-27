using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBehavior : MonoBehaviour
{
    private MeshRenderer SensorRenderer;
    public Material onSensorMaterial;
    public Material offSensorMaterial;

    public GameObject sensorLed;
    private MeshRenderer ledRenderer;
    public Material onLedMaterial;
    public Material offLedMaterial;
    public bool sensorSignal;

    private void Start()
    {
        SensorRenderer = GetComponent<MeshRenderer>();
        ledRenderer = sensorLed.GetComponent<MeshRenderer>();
        sensorSignal = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        SensorRenderer.material = onSensorMaterial;
        ledRenderer.material = onLedMaterial;
        sensorSignal = true;
    }

    private void OnTriggerExit(Collider other)
    {
        SensorRenderer.material = offSensorMaterial;
        ledRenderer.material = offLedMaterial;
        sensorSignal = false;
    }
}
