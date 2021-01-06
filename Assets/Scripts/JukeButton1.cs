using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeButton1 : MonoBehaviour
{
    public Animator Cust2, Cust3, Cust4;            //sets animator for each character
    public AudioSource Song1, Song2, Song3;         //references the audio sources
    public GameObject Button;                       //reference to button object
    public Material On, Off;                        //references to materials

    void Start()
    {
        PlaySong();                                 //calls play song function
    }

    private void Update()
    {
        if (!Song1.isPlaying)                                       //if this song is not playing....
        {
            Button.GetComponent<MeshRenderer>().material = Off;     //set the button material to off
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("XRPlayer") | other.gameObject.layer == LayerMask.NameToLayer("Body") | other.gameObject.layer == LayerMask.NameToLayer("Grab"))    //checks that the collider object is the VR hands
        {
            PlaySong();                             //calls function
        }
    }

    void PlaySong()
    {
        Song1.Play();                                           //sets correct song to be playing, others to stop
        Song2.Stop();
        Song3.Stop();

        Button.GetComponent<MeshRenderer>().material = On;      //sets button material to the on material

        Cust2.SetBool("HipHopDance", true);                     //sets the various dance animations on or off based on animator bools
        Cust3.SetBool("HipHopDance", true);
        Cust4.SetBool("HipHopDance", true);

        Cust2.SetBool("SalsaDance", false);
        Cust3.SetBool("SalsaDance", false);
        Cust4.SetBool("SalsaDance", false);

        Cust2.SetBool("ChickenDance", false);
        Cust3.SetBool("ChickenDance", false);
        Cust4.SetBool("ChickenDance", false);

    }

}
