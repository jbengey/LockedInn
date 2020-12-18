using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMButton : MonoBehaviour
{
    public bool buttonPressed;

    private void Start()
    {
        buttonPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("XRPlayer"))
        {
            buttonPressed = true;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("XRPlayer"))
        {
            buttonPressed = false;
        }

    }

}
