using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRPlayerCollisionManager : MonoBehaviour
{
    public Transform vRCamera;
    CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        if(characterController.center.x != vRCamera.localPosition.x | characterController.center.z != vRCamera.localPosition.z)
        {
            //Align character controller to the camera - avoids issues if player moves inside VR playzone
            characterController.center = new Vector3(vRCamera.localPosition.x, 0.86f, vRCamera.localPosition.z);
        }

    }
}
