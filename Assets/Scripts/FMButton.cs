using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMButton : MonoBehaviour
{
    public GameObject button;
    public bool buttonPressed;
    public Material on, off;

    private void Start()
    {
        button.GetComponent<MeshRenderer>().material = off;
        buttonPressed = false;
    }

    private void Update()
    {
        if (buttonPressed)
        {
            button.GetComponent<MeshRenderer>().material = on;
        }
        else
        {
            button.GetComponent<MeshRenderer>().material = off;
        }
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
