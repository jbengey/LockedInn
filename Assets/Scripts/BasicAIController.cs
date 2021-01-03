using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BasicAIController : MonoBehaviour
{
    public Animator NPCAni;                                //defines animator
    public Transform target1, target2, player;             //sets up targets for navmeshagent
    public NavMeshAgent NPCMesh;                           //defines NavMeshAgent (NMA)
    private bool playerIsHere;                             //creates bool
    public float rotationSpeed = 15f, distance;

    private void Start()
    {
        NPCMesh.SetDestination(target2.position);            //sets initial target for NMA
        NPCMesh.speed = 1;                               //defines speed for NMA
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);

        if (playerIsHere)
        {
            RotateTowards();
        }

        if (Vector3.Distance(NPCMesh.destination, NPCMesh.transform.position) <= 0.5f && playerIsHere == false)            //compares NMA location and target location, and if less than or equal to 1 distance away AND bool is false...
        {
            NPCMesh.SetDestination(target2.position);                                                                        //change to target2

            if (Vector3.Distance(NPCMesh.destination, NPCMesh.transform.position) <= 0.5f && playerIsHere == false)       //compares NMA location and target location, and if less than or equal to 1 distance away AND bool is false...
            {
                NPCMesh.SetDestination(target1.position);                                                                    //change to target1
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NPCMesh.SetDestination(player.position);             //sets NMA target to player location
        playerIsHere = true;                                    //changes bool to true
        NPCAni.SetBool("PlayerIsHere", true);                //changes animator bool to true, starting transition
    }

    private void OnTriggerStay(Collider other)
    {
        RotateTowards();
        if (Vector3.Distance(NPCMesh.destination, NPCMesh.transform.position) <= 4.0f)        //compares NMA location with target and if distance is less then or equal to 4....
        {
           NPCMesh.isStopped = true;                                                            //change NMA to isStopped, makes sure NPC doesn't get too close
        }
    }

    private void OnTriggerExit(Collider other)
    {
        NPCMesh.isStopped = false;                           //sets bool back to false on exit, allowing NMA to move again
        NPCMesh.SetDestination(target2.position);            //sets new target for NMA
        playerIsHere = false;                                   //sets bool to false
        NPCAni.SetBool("PlayerIsHere", false);               //sets animator bool to false, starting new animation        
    }

    private void RotateTowards()
    {
        //get difference of the rotation of the player and gameObjects position
        Vector3 direction = (player.position - transform.position).normalized;

        //set lookRotation to the x and y of the player
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //apply rotation
        NPCMesh.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3);
    }
}
