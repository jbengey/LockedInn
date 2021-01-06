﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeButton2 : MonoBehaviour
{
    public Animator Cust2, Cust3, Cust4;            //sets animator for each character
    public AudioSource Song1, Song2, Song3;         //references the audio sources
    public GameObject Button;                       //reference to button object
    public Material On, Off;                        //references to materials

    private void Update()
    {
        if (!Song2.isPlaying)                                       //if this song is not playing....
        {
            Button.GetComponent<MeshRenderer>().material = Off;     //set to the off material
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("XRPlayer") | other.gameObject.layer == LayerMask.NameToLayer("Body") | other.gameObject.layer == LayerMask.NameToLayer("Grab"))    //checks that the collider object is the VR hands
        {
            PlaySong();                             //calls play song function
        }
    }

    void PlaySong()
    {
        Song1.Stop();                                           //sets correct song to be playing, others to stop
        Song2.Play();
        Song3.Stop();

        Button.GetComponent<MeshRenderer>().material = On;      //sets button material to the on material

        Cust2.SetBool("HipHopDance", false);                    //sets the various dance animations on or off based on animator bools
        Cust3.SetBool("HipHopDance", false);
        Cust4.SetBool("HipHopDance", false);

        Cust2.SetBool("SalsaDance", true);
        Cust3.SetBool("SalsaDance", true);
        Cust4.SetBool("SalsaDance", true);

        Cust2.SetBool("ChickenDance", false);
        Cust3.SetBool("ChickenDance", false);
        Cust4.SetBool("ChickenDance", false);

    }

}
