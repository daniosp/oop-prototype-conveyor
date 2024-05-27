using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class SystemControl : MonoBehaviour
{
    // ============================================================================================================
    // This script contains the logical control algorithm of the conveyor belt. It reads Input Signals from sensors
    // and sends Output Signals to indicate the desired movement of the cube gameObject.
    // ===========================================================================================================

    private bool sensorInput;
    public GameObject[] sensorList;

    private int pos_ini;

    public bool turnR;
    public bool turnL;

    public bool START_ = false;


    // The following are custom methods for recieveing each sensor signal as a boolean variable
    bool ReadSensorInput(GameObject sensor)
    {
        sensorInput = sensor.GetComponent<SensorBehavior>().sensorSignal;
        return sensorInput;
    }

    bool ReadSwitchInput(GameObject sensor)
    {
        sensorInput = sensor.GetComponent<SwitchBehavior>().sensorSignal;
        return sensorInput;
    }

    // The following is a Co-Routine used for setting up time delays within the code

    private IEnumerator digitalDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }


    // ===========================================
    //                SYSTEM CONTROL 
    // ===========================================


    /*Sensor Input List (Use ReadSensorInput):
     *      Sensor1 = sensorList[0];
     *      Sensor2 = sensorList[1];
     *      Sensor3 = sensorList[2];
     *      Sensor4 = sensorList[3];
     *
     *Switch Input List (Use ReadSwitchInput)
     *      SwStart = sensorList[4];
     *      SwStop= sensorList[5];
     *      Sw1 = sensorList[6];
     *      Sw2 = sensorList[7];
    */

    // The following are custom methods built on coroutines that indicate the set of actions that the conveyor must execute
    private IEnumerator case_1() // If the cube is located at S2 or S3, then it must reach S4, wait for 2 seconds and return to its initial position
    {
        if (ReadSensorInput(sensorList[1]) == true)
        {
            pos_ini = 2;
        }
        else if (ReadSensorInput(sensorList[2]) == true)
        {
            pos_ini = 3;
        }

        while (ReadSensorInput(sensorList[3]) == false)
        {
            turnL = true;
            yield return null;
        }

        turnL = false;

        yield return StartCoroutine(digitalDelay(2));

        if (pos_ini == 2)
        {
            while (ReadSensorInput(sensorList[1]) == false)
            {
                turnR = true;
                yield return null;
            }

            turnR = false;
        }

        else if (pos_ini == 3)
        {
            while (ReadSensorInput(sensorList[2]) == false)
            {
                turnR = true;
                yield return null;
            }

            turnR = false;
        }

        yield return null;

    }

    private IEnumerator case_2() // If the cube is located at S2 or S3, then it must reach S1, wait for 2 seconds and return to its initial position
    {

        if (ReadSensorInput(sensorList[1]) == true)
        {
            pos_ini = 2;
        }
        else if (ReadSensorInput(sensorList[2]) == true)
        {
            pos_ini = 3;
        }

        while (ReadSensorInput(sensorList[0]) == false)
        {
            turnR = true;
            yield return null;
        }

        turnR = false;

        yield return StartCoroutine(digitalDelay(2));

        if (pos_ini == 2)
        {
            while (ReadSensorInput(sensorList[1]) == false)
            {
                turnL = true; ;
                yield return null;
            }

            turnL = false;
        }

        else if (pos_ini == 3)
        {
            while (ReadSensorInput(sensorList[2]) == false)
            {
                turnL = true;
                yield return null;
            }

            turnL = false;
        }

        yield return null;

    }

    private IEnumerator case_3() // If the cube is located at S2 or S3, then it will move indefinitely between sensors S2 and S3
    {
        while (ReadSwitchInput(sensorList[6]) == true && ReadSwitchInput(sensorList[7]) == false)
        {
            while (ReadSensorInput(sensorList[1]) == false)
            {
                turnR = true;

                yield return null;
            }

            turnR = false;
            turnL = false;

            while (ReadSensorInput(sensorList[2]) == false)
            {
                turnL = true;

                yield return null;
            }

            turnR = false;
            turnL = false;

        }

        yield return null;
    }



    private IEnumerator ControlCoroutine() // This coroutine implements each of the previous cases, it runs each one according to the positions of SW1 and SW2.
    {

        while (ReadSwitchInput(sensorList[4]) == false)
        {
            turnR = false;
            turnL = false;

            yield return null;
        }

        if ((ReadSensorInput(sensorList[1]) == true || ReadSensorInput(sensorList[2]) == true) && ReadSwitchInput(sensorList[6]) == false && ReadSwitchInput(sensorList[7]) == false)
        {
            yield return StartCoroutine(case_1());
        }

        else if ((ReadSensorInput(sensorList[1]) == true || ReadSensorInput(sensorList[2]) == true) && ReadSwitchInput(sensorList[6]) == false && ReadSwitchInput(sensorList[7]) == true)
        {
            yield return StartCoroutine(case_2());
        }

        else if ((ReadSensorInput(sensorList[1]) == true || ReadSensorInput(sensorList[2]) == true) && ReadSwitchInput(sensorList[6]) == true && ReadSwitchInput(sensorList[7]) == false)
        {
            yield return StartCoroutine(case_3());
        }

        else if (ReadSensorInput(sensorList[1]) == false && ReadSensorInput(sensorList[2]) == false)
        {
            turnL = false;
            turnR = false;
            Debug.Log("ERROR: Cube not in sensors S2 or S3 when pressing Start. Please relocate correctly and try again.");
        }

        else if (ReadSwitchInput(sensorList[6]) == true && ReadSwitchInput(sensorList[7]) == true)
        {
            turnL = false;
            turnR = false;
            Debug.Log("ERROR: Switches SW1 and SW2 cannot be activated simultaneously. Please set the switches to an allowed configuration.");
        }
        
    }

    private IEnumerator IterativeCoroutine() // This coroutine is called within the Start() method to ensure the creation of just one control coroutine
    {
        while (START_ == false) { 
            yield return ControlCoroutine();
        }
    }


void Start()
    {
        StartCoroutine(IterativeCoroutine());
    }
}