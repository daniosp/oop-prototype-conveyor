using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{   // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // Sensor Parent Class:
    // This class is created as an example of INHERITANCES but also displays POLYMORPHISM by using to method overriding.
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    // The ChangeSensor Status can be used both for when a sensor is activated or deactivated. It is important to input the correct materials for each case.
    public virtual void ChangeSensorStatus( MeshRenderer sensorObjectRenderer, Material sensorMaterial)
    {
        sensorObjectRenderer.material = sensorMaterial;
    }

    public virtual void ChangeSensorStatus( MeshRenderer sensorObjectRenderer, Material sensorMaterial, MeshRenderer miscObjectRenderer, Material miscMaterial)
    {
        sensorObjectRenderer.material = sensorMaterial;
        miscObjectRenderer.material = miscMaterial;
    }

}
