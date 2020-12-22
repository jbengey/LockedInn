using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BarKeepAIController : MonoBehaviour
{
    public Animator NPCAni;                                //defines animator
    public Transform target1, target2, player;             //sets up targets for navmeshagent
    public NavMeshAgent NPCMesh;                           //defines NavMeshAgent (NMA)
    private bool playerIsHere, targetRotate;                             //creates bool
    public float turnSpeed = 1f;
    private Vector3 currentTarget;

    private void Start()
    {
        NPCAni.SetBool("Idle", false);
        NPCMesh.SetDestination(target2.position);            //sets initial target for NMA
        NPCMesh.speed = 0.9f;                               //defines speed for NMA

    }

    // Update is called once per frame
    void Update()
    {
        currentTarget = NPCMesh.destination;

        if (playerIsHere)
        {
            RotateTowardsPlayer();
        }
        if (targetRotate)
        {
            RotateTowardsTarget();
        }

        if (Vector3.Distance(NPCMesh.destination, NPCMesh.transform.position) <= 0.5f && playerIsHere == false)       //compares NMA location and target location, and if less than or equal to 1 distance away AND bool is false...
        {
            //targetRotate = true;
            StartCoroutine(WaitAtEnd());
            NPCMesh.SetDestination(target1.position);                                                                    //change to target1


            if (Vector3.Distance(NPCMesh.destination, NPCMesh.transform.position) <= 0.5f && playerIsHere == false)            //compares NMA location and target location, and if less than or equal to 1 distance away AND bool is false...
            {
                //targetRotate = true;
                StartCoroutine(WaitAtEnd());
                NPCMesh.SetDestination(target2.position);                                                                        //change to target2
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NPCMesh.isStopped = true;
        playerIsHere = true;                                    //changes bool to true
        NPCAni.SetBool("PlayerIsHere", true);                //changes animator bool to true, starting transition
    }

    private void OnTriggerStay(Collider other)
    {
        NPCMesh.isStopped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        NPCMesh.isStopped = false;                           //sets bool back to false on exit, allowing NMA to move again
        NPCMesh.SetDestination(target2.position);            //sets new target for NMA
        playerIsHere = false;                                   //sets bool to false
        NPCAni.SetBool("PlayerIsHere", false);               //sets animator bool to false, starting new animation   
        NPCMesh.isStopped = false;
    }

    private void RotateTowardsPlayer()
    {   
        Vector3 direction = (player.position - transform.position).normalized;                                    //get difference of the rotation of the player and gameObjects position
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));              //set lookRotation to the x and y of the player
        NPCMesh.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);      //apply rotation
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = (currentTarget - transform.position).normalized;                                                        //get difference of the rotation of the player and gameObjects position
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));              //set lookRotation to the x and y of the player
        NPCMesh.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);      //apply rotation

        if (NPCMesh.transform.rotation == lookRotation)
        {
            targetRotate = false;
        }

        StopNotNeededAnis();
    }

    void StopNotNeededAnis()
    {
        NPCAni.SetBool("TurnLeft", false);
        NPCAni.SetBool("TurnRight", false);
        NPCAni.SetBool("Idle", false);
    }


    IEnumerator WaitAtEnd()
    {
        NPCMesh.isStopped = true;
        NPCAni.SetBool("Idle", true);
        int wait_time = Random.Range(2, 5);
        yield return new WaitForSeconds(wait_time);
        if (Vector3.Distance(NPCMesh.destination, target1.transform.position) <= 1f)
        {
            NPCAni.SetBool("TurnRight", true);
        }
        if (Vector3.Distance(NPCMesh.destination, target2.transform.position) <= 1f)
        {
            NPCAni.SetBool("TurnLeft", true);
        }
        targetRotate = true;
        NPCMesh.isStopped = false;
    }
}
