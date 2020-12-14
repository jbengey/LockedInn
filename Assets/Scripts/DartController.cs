using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //If colliding with anything that is not a dart
        if (!collision.gameObject.CompareTag("Dart"))
        {
            Rigidbody dartRB = this.gameObject.GetComponent<Rigidbody>(); //reference to the dart rigidbody
            dartRB.constraints = RigidbodyConstraints.FreezeAll; //freeze dart - getting it stuck in whatever it has hit
        }
    }




}
