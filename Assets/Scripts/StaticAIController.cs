using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class StaticAIController : MonoBehaviour
{
    public Animator NPCAni;                 //defines animator
    public Transform player;                //sets up targets for navmeshagent
    public NavMeshAgent NPCMesh;            //defines NavMeshAgent (NMA)
    private bool playerIsHere, turnBack;    //creates bool
    public float turnSpeed;                 //creates public float 
    private Quaternion originalAngle;


    private void Start()
    {
        turnSpeed = 1f;                                     //sets turnspeed float
        originalAngle = NPCMesh.transform.rotation;
    }

// Update is called once per frame
void Update()
    {
        if (playerIsHere)                   //if this is true...
        {
            RotateTowardsPlayer();          //calls this function
        }
        if (turnBack)                       //if this is true...
        {
            TurnBack();                     //call this function
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerIsHere = true;                            //changes bool to true
        RotateTowardsPlayer();                          //calls this function
        NPCAni.SetBool("PlayerIsHere", true);           //changes animator bool to true
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        playerIsHere = false;                           //sets bool to false
        NPCAni.SetBool("PlayerIsHere", false);          //sets animator bool to false, starting new animation   
        turnBack = true;                                //sets 
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;                                          //get difference of the rotation of the player and gameObjects position
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));                    //set lookRotation to the x and y of the player
        NPCMesh.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);    //apply rotation
    }

    private void TurnBack()
    {
        NPCMesh.transform.rotation = Quaternion.Slerp(transform.rotation, originalAngle, Time.deltaTime * turnSpeed);           
        if(NPCMesh.transform.rotation == originalAngle)
        {
            turnBack = false;
        }
    }
}
