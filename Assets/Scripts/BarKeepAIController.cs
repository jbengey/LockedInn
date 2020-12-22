using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BarKeepAIController : MonoBehaviour
{
    public Animator NPCAni;                             //defines animator
    public Transform target1, target2, player;          //sets up targets for navmeshagent
    public NavMeshAgent NPCMesh;                        //defines NavMeshAgent (NMA)
    private bool playerIsHere, targetRotate;            //creates bool
    public float turnSpeed = 1f;                        //creates and defines float for turnspeed
    private Vector3 currentTarget;                      //creates Vector3 for current target

    private void Start()
    {
        NPCAni.SetBool("Idle", false);                  //ensures animtaor bool is false
        NPCMesh.SetDestination(target2.position);       //sets initial target for NMA
        NPCMesh.speed = 0.9f;                           //defines speed for NMA

    }

    // Update is called once per frame
    void Update()
    {
        currentTarget = NPCMesh.destination;            //defines currentTarget vector3 to that of the NMA destination's Vector3

        if (playerIsHere)                               //if this is true....
        {
            RotateTowardsPlayer();                      //call this funcion
        }
        if (targetRotate)                               //if this is true...
        {
            RotateTowardsTarget();                      //call this function
        }

        if (Vector3.Distance(NPCMesh.destination, NPCMesh.transform.position) <= 0.5f && playerIsHere == false)         //compares NMA location and target location, and if less than or equal to 1 distance away AND bool is false...
        {
            StartCoroutine(WaitAtEnd());                                                                                //call this co routine
            NPCMesh.SetDestination(target1.position);                                                                   //change to target1


            if (Vector3.Distance(NPCMesh.destination, NPCMesh.transform.position) <= 0.5f && playerIsHere == false)     //compares NMA location and target location, and if less than or equal to 1 distance away AND bool is false...
            {
                StartCoroutine(WaitAtEnd());                                                                            //call this co routine
                NPCMesh.SetDestination(target2.position);                                                               //change to target2
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StopNotNeededAnis();                            //call this funstion
        NPCMesh.isStopped = true;                       //sets this bool/internal funstion to true
        playerIsHere = true;                            //changes bool to true
        NPCAni.SetBool("PlayerIsHere", true);           //changes animator bool to true, starting transition
    }

    private void OnTriggerStay(Collider other)
    {
        NPCMesh.isStopped = true;                       //ensures this stays true whilst staying in the trigger
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsHere = false;                          //sets bool to false
        NPCAni.SetBool("PlayerIsHere", false);         //sets animator bool to false, starting new animation   
        NPCMesh.isStopped = false;                     //sets bool back to false on exit, allowing NMA to move again
        NPCMesh.SetDestination(target2.position);      //sets new target for NMA
    }

    private void RotateTowardsPlayer()
    {   
        Vector3 direction = (player.position - transform.position).normalized;                                          //get difference of the rotation of the player and gameObjects position
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));                    //set lookRotation to the x and y of the player
        NPCMesh.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);    //apply rotation
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = (currentTarget - transform.position).normalized;                                                //get difference of the rotation of the player and gameObjects position
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));                        //set lookRotation to the x and y of the player
        NPCMesh.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);        //apply rotation

        if (NPCMesh.transform.rotation == lookRotation)         //if the NMA and the lookRotation are equal....
        {
            targetRotate = false;                               //set bool to false
        }

        StopNotNeededAnis();                                    //call this funstion
    }

    void StopNotNeededAnis()
    {
        NPCAni.SetBool("TurnLeft", false);          //sets animator bool to false
        NPCAni.SetBool("TurnRight", false);         //sets animator bool to false
        NPCAni.SetBool("Idle", false);              //sets animator bool to false
    }


    IEnumerator WaitAtEnd()
    {
        NPCMesh.isStopped = true;                                                       //sets this bool/internal funstion to true
        NPCAni.SetBool("Idle", true);                                                   //sets animator bool to true
        int wait_time = Random.Range(7, 15);                                            //sets a range of integers 
        yield return new WaitForSeconds(wait_time);                                     //waits for a number of seconds from the range of integers set
        if (Vector3.Distance(NPCMesh.destination, target1.transform.position) <= 1f)    //if NMA is within 1f of target1...
        {
            NPCAni.SetBool("TurnRight", true);                                          //set this animator bool to true
        }
        if (Vector3.Distance(NPCMesh.destination, target2.transform.position) <= 1f)    //if NMA is within 1f of target2....
        {
            NPCAni.SetBool("TurnLeft", true);                                           //set this animator bool to true
        }
        targetRotate = true;                                                            //sets bool to true
        NPCMesh.isStopped = false;                                                      //sets this bool/internal function to true
    }
}
