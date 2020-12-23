using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public Animator Cust2, Cust3, Cust4;
    public AudioSource Song1, Song2;
    private bool playSong1, playSong2;

    // Start is called before the first frame update
    void Start()
    {
        Cust2.SetBool("ChickenDance", false);
        Cust3.SetBool("ChickenDance", false);
        Cust4.SetBool("ChickenDance", false);
        PlaySong1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (playSong1)
        {
            PlaySong1();
        }
        else if (playSong2)
        {
            PlaySong2();
        }
    }

    public void PlaySong1()
    {
        Song2.Stop();
        Song1.Play();
        Cust2.SetBool("ChickenDance", false);
        Cust3.SetBool("ChickenDance", false);
        Cust4.SetBool("ChickenDance", false);
        playSong1 = false;
        playSong2 = true;
    }

    public void PlaySong2()
    {
        Song1.Stop();
        Song2.Play();
        Cust2.SetBool("ChickenDance", true);
        Cust3.SetBool("ChickenDance", true);
        Cust4.SetBool("ChickenDance", true);
        playSong1 = true;
        playSong2 = false;
    }
}
