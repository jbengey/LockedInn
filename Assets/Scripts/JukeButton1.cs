using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeButton1 : MonoBehaviour
{
    public Animator Cust2, Cust3, Cust4;
    public AudioSource Song1, Song2, Song3;
    public GameObject Button;
    public Material On, Off;

    // Start is called before the first frame update
    void Start()
    {
        PlaySong();
    }

    private void Update()
    {
        if (!Song1.isPlaying)
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
        Song1.Play();
        Song2.Stop();
        Song3.Stop();

        Button.GetComponent<MeshRenderer>().material = On;

        Cust2.SetBool("HipHopDance", true);
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
