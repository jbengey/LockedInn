using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeButton2 : MonoBehaviour
{
    public Animator Cust2, Cust3, Cust4;
    public AudioSource Song1, Song2, Song3;
    public GameObject Button;
    public Material On, Off;

    private void Update()
    {
        if (!Song2.isPlaying)
        {
            Button.GetComponent<MeshRenderer>().material = Off;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlaySong();
    }

    void PlaySong()
    {
        Song1.Stop();
        Song2.Play();
        Song3.Stop();

        Button.GetComponent<MeshRenderer>().material = On;

        Cust2.SetBool("HipHopDance", false);
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
