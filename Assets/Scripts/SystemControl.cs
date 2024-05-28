using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

public class SystemControl : MonoBehaviour
{
    // ============================================================================================================
    // This script contains the logical control algorithm of the conveyor belt. It reads Input Signals from sensors
    // and sends Output Signals to indicate the desired movement of the cube gameObject.
    // ===========================================================================================================

    private int pos_ini;
    private bool START_ = false;

    public bool turnR { get; private set; } // ENCAPSULATION
    public bool turnL { get; private set; } // ENCAPSULATION

    public TextMeshProUGUI infoText;

    public SensorBehavior seS1;
    public SensorBehavior seS2;
    public SensorBehavior seS3;
    public SensorBehavior seS4;
    public SwitchBehavior swSTART;
    public SwitchBehavior swSTOP;
    public SwitchBehavior sw1;
    public SwitchBehavior sw2;

    void Start()
    {
        StartCoroutine(IterativeCoroutine());
    }

    private IEnumerator IterativeCoroutine() // This coroutine is called within the Start() method to ensure the creation of just one control coroutine
    {
        while (START_ == false)
        {
            yield return ControlCoroutine();
        }
    }


    // The following is a Co-Routine used for setting up time delays within the code

    private IEnumerator digitalDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    // The following is a Co-Routine for pausing the conveyor belt at any time

    private IEnumerator ConveyorPause()
    {
        while (swSTOP.signal == true)
        {
            turnR = false;
            turnL = false;

            yield return null;
        }

    }

    // ===========================================
    //                SYSTEM CONTROL 
    // ===========================================

    // The following are custom methods built on coroutines that indicate the set of actions that the conveyor must execute

    private IEnumerator ControlCoroutine() // This coroutine implements each of the previous cases, it runs each one according to the positions of SW1 and SW2.
    {

        while (swSTART.signal == false)
        {
            turnR = false;
            turnL = false;

            yield return null;
        }

        if ((seS2.signal == true || seS3.signal == true) && sw1.signal == false && sw2.signal == false)
        {
            infoText.text = null;
            yield return StartCoroutine(case_1());
        }

        else if ((seS2.signal == true || seS3.signal == true) && sw1.signal == false && sw2.signal == true)
        {
            infoText.text = null;
            yield return StartCoroutine(case_2());
        }

        else if ((seS2.signal == true || seS3.signal == true) && sw1.signal == true && sw2.signal == false)
        {
            infoText.text = null;
            yield return StartCoroutine(case_3());
        }

        else if (seS2.signal == false && seS3.signal == false)
        {
            turnL = false;
            turnR = false;
            infoText.text = "ERROR: Cube not in sensors S2 or S3 when pressing Start. Please relocate correctly and try again.";
        }

        else if (sw1.signal == true && sw2.signal == true)
        {
            turnL = false;
            turnR = false;
            infoText.text = "ERROR: Switches SW1 and SW2 cannot be activated simultaneously. Please set the switches to an allowed configuration.";
            
        }

    }



    private IEnumerator case_1() // If the cube is located at S2 or S3, then it must reach S4, wait for 2 seconds and return to its initial position
    {
        if (seS2.signal == true)
        {
            pos_ini = 2;
        }
        else if (seS3.signal == true)
        {
            pos_ini = 3;
        }

        while (seS4.signal == false)
        {
            turnL = true;
            yield return ConveyorPause();
        }

        turnL = false;

        yield return StartCoroutine(digitalDelay(2));

        if (pos_ini == 2)
        {
            while (seS2.signal == false)
            {
                turnR = true;
                yield return ConveyorPause();
            }

            turnR = false;
        }

        else if (pos_ini == 3)
        {
            while (seS3.signal == false)
            {
                turnR = true;
                yield return ConveyorPause();
            }

            turnR = false;
        }

        yield return null;

    }

    private IEnumerator case_2() // If the cube is located at S2 or S3, then it must reach S1, wait for 2 seconds and return to its initial position
    {

        if (seS2.signal == true)
        {
            pos_ini = 2;
        }
        else if (seS3.signal == true)
        {
            pos_ini = 3;
        }

        while (seS1.signal == false)
        {
            turnR = true;
            yield return ConveyorPause();
        }

        turnR = false;

        yield return StartCoroutine(digitalDelay(2));

        if (pos_ini == 2)
        {
            while (seS2.signal == false)
            {
                turnL = true; ;
                yield return ConveyorPause();
            }

            turnL = false;
        }

        else if (pos_ini == 3)
        {
            while (seS3.signal == false)
            {
                turnL = true;
                yield return ConveyorPause();
            }

            turnL = false;
        }

        yield return null;

    }

    private IEnumerator case_3() // If the cube is located at S2 or S3, then it will move indefinitely between sensors S2 and S3
    {
        while (sw1.signal == true && sw2.signal == false)
        {
            while (seS2.signal == false)
            {
                turnR = true;

                yield return ConveyorPause();
            }

            turnR = false;
            turnL = false;

            while (seS3.signal == false)
            {
                turnL = true;

                yield return ConveyorPause();
            }

            turnR = false;
            turnL = false;

        }

        yield return null;
    }

}