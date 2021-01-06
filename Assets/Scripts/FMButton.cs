using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMButton : MonoBehaviour
{
    public GameObject button;           //referencess game object for the button
    public bool buttonPressed;          //creates a bool
    public Material on, off;            //references materials

    private void Start()
    {
        button.GetComponent<MeshRenderer>().material = off;     //sets to the off material at start
        buttonPressed = false;                                  //sets bool to false
    }

    private void Update()
    {
        if (buttonPressed)                                          //if true...
        {
            button.GetComponent<MeshRenderer>().material = on;      //set to on material
        }
        else                                                        //if not...
        {
            button.GetComponent<MeshRenderer>().material = off;     //set to off material
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("XRPlayer") | other.gameObject.layer == LayerMask.NameToLayer("Body") | other.gameObject.layer == LayerMask.NameToLayer("Grab"))     //checks colliding game object is VR hands
        {
            buttonPressed = true;       //set bool to true
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("XRPlayer") | other.gameObject.layer == LayerMask.NameToLayer("Body") | other.gameObject.layer == LayerMask.NameToLayer("Grab"))    //checks colliding game object is VR hands
        {
            buttonPressed = false;      //set bool false
        }

    }

}
