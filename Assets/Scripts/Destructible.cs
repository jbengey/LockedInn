using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Destructible : MonoBehaviour
{
    public GameObject originalModel,destroyedObject; //ref to the seperate gameobject version
    public float timeToDespawn = 0.0f;
    public float velocityNeededToBreak = 0.5f;

    XRGrabInteractable grabInteractable;
    GameObject instanciated;


    private void Awake()
    {
       TryGetComponent(out grabInteractable); //ref to grab iteractor component if exists

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyedObject == null) Debug.LogError("Destructible scirpt has no destroyed object provided", this);

        if (destroyedObject != null && collision.relativeVelocity.magnitude > velocityNeededToBreak && collision.gameObject.layer != LayerMask.NameToLayer("Grab"))
        {
            if (grabInteractable != null)
            {
                Destroy(grabInteractable); //remove interactor to avoid update errors
            }

            //Destroy(originalModel);
            originalModel.SetActive(false);

            //Instanciate the destroyed model
            instanciated = Instantiate(destroyedObject, transform.position, transform.rotation);

            foreach (Transform child in instanciated.transform)
            {
                //add force relative to the speed of the collison to each rigidbody, at center of the collision
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(5 * collision.relativeVelocity.magnitude, collision.contacts[0].point,1.5f);
            }

            //Coroutine to remove shattered object after specified time
            StartCoroutine(DestroyAfterTime(instanciated, timeToDespawn));


        }
    }


    private IEnumerator DestroyAfterTime(GameObject myobject, float time)
    {
        if (time != 0)
        {
            yield return new WaitForSeconds(time);
            Destroy(myobject); //remove instanciated broken object
        }

    }



}
