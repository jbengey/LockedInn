using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour
{
    public DartboardController dartBoard;
    AudioSource dartHitSound;
    public Rigidbody dartRB;
    bool inAir=false;
    Vector3 lastPosition = Vector3.zero;
    public Transform tip;

    private void Awake()
    {
        dartHitSound = GetComponent<AudioSource>();
        dartRB = GetComponent<Rigidbody>(); //reference to the dart rigidbody
    }


    //Called when the user throws a dart
    public void DartIsThrown()
    {

        dartBoard.dartsThrown++; //Iterate
        lastPosition = tip.position;
        inAir = true;

        dartRB.isKinematic = false; //unfreeze dart
        dartRB.useGravity = true;

        StartCoroutine(RotateWithVelocity());

    }

    //called when user picks up dart
    public void PickupDart()
    {
        dartRB.isKinematic = false; //unfreeze rigidbody so can throw
        dartRB.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If colliding with anything that is not a dart
        if (!collision.gameObject.CompareTag("Dart") & collision.gameObject.layer != LayerMask.NameToLayer("Grab") & collision.gameObject.layer != LayerMask.NameToLayer("Body"))
        {
            dartHitSound.Play(); //Play dart hit sound
            inAir = false;

            dartRB.velocity = Vector3.zero;
            dartRB.angularVelocity = Vector3.zero;

            dartRB.isKinematic = true; //freeze dart - getting it stuck in whatever it has hit
            dartRB.useGravity = false;
        }
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            lastPosition = tip.position;
        }
    }


    //Keep dart rotated towards tarket as it flies
    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(dartRB.velocity, transform.up);
            transform.rotation = newRotation * Quaternion.Euler(0, 180f, 0); //rotate 180 degrees
            yield return null;
        }
    }

 



}
