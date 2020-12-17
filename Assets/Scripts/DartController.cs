using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour
{
    public DartboardController dartBoard;
    AudioSource dartHitSound;
    Rigidbody dartRB;


    private void Awake()
    {
        dartHitSound = GetComponent<AudioSource>();
        dartRB = GetComponent<Rigidbody>(); //reference to the dart rigidbody
    }


    //Called when the user throws a dart
    public void DartIsThrown()
    {
        dartBoard.dartsThrown++; //Iterate
    }

    //called when user picks up dart
    public void PickupDart()
    {
        if((dartRB.constraints & RigidbodyConstraints.FreezeAll) == RigidbodyConstraints.FreezeAll) //if rigidbody is frozen
        {
            dartRB.constraints = RigidbodyConstraints.None; //unfreeze rigidbody so can throw
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //If colliding with anything that is not a dart
        if (!collision.gameObject.CompareTag("Dart"))
        {
            dartRB.constraints = RigidbodyConstraints.FreezeAll; //freeze dart - getting it stuck in whatever it has hit
            dartHitSound.Play(); //Play dart hit sound
        }
    }

    //Possible force needed on throw of dart?

    //Possible need to shade dart with overlay to display the fact it is interactable





}
