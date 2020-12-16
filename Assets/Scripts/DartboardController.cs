using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DartboardController : MonoBehaviour
{
    public Transform centre; //reference to centre of dartboard gameobject
    public int playerScore = 0;


    private void OnCollisionEnter(Collision other)
    {

        //Check that the entered collider is a dart
        if (other.gameObject.CompareTag("Dart"))
        {
            Vector3 collisionPoint = other.contacts[0].point; //reference to the exact point of the collider that hit the dartboard
            playerScore += CalculateScore(collisionPoint);
        }
    }


    private int CalculateScore(Vector3 CollisionPoint)
    {
        //local variable declaration
        int dartMultiplier;
        int calculatedScore;
        float dartDistance, dartDistanceX, dartDistanceY;
        double dartAngle;

        dartDistance = Vector2.Distance(new Vector2(centre.position.x,centre.position.y), new Vector2(CollisionPoint.x, CollisionPoint.y) );    //Calculate distance between dart and the bullseye (centre)
        dartDistanceX = CollisionPoint.x - centre.position.x ; //x direction of dart
        dartDistanceY = CollisionPoint.y - centre.position.y ; //Y direction of dart
        dartAngle = Math.Atan2(dartDistanceY,dartDistanceX); //Trig to caluclate angle of dart relative to centre, in radians
        dartAngle = dartAngle * 180 / Math.PI; //convert radians to degrees
        

        //Check for bull 50 and 25
        if (dartDistance < 0.0302680f)
        {
            calculatedScore = 50; //Bullseye!
        }
        else if (dartDistance >= 0.0302680f && dartDistance < 0.0706981f)
        {
            calculatedScore = 25; //Bull!
        }
        else
        {
            // Detection based on distance (Score multiplier)
            if ( dartDistance > 0.4357793f && dartDistance < 0.4637211f)
            {
                //Inner triple ring
                dartMultiplier = 3;
            }
            else if (dartDistance> 0.7226701f && dartDistance < 0.7645695f)
            {
                //Outer double ring
                dartMultiplier = 2;
            }
            else if (dartDistance > 0.7645695f)
            {
                //Out of bounds (miss)
                dartMultiplier = 0;
            }
            else
            {
                //Normal area - single scores
                dartMultiplier = 1;
            }

            //Detection based on angle (Hit score number)
            int hitNumber = dartAngle < 9 ? 6
                          : dartAngle > 9 && dartAngle < 27 ? 13
                          : dartAngle > 27 && dartAngle < 45 ? 4
                          : dartAngle > 45 && dartAngle < 63 ? 18
                          : dartAngle > 63 && dartAngle < 81 ? 1
                          : dartAngle > 81 && dartAngle < 99 ? 20
                          : dartAngle > 99 && dartAngle < 117 ? 5
                          : dartAngle > 117 && dartAngle < 135 ? 12
                          : dartAngle > 135 && dartAngle < 153 ? 9
                          : dartAngle > 153 && dartAngle < 171 ? 14
                          : dartAngle > 171 && dartAngle < 189 ? 11
                          : dartAngle > 189 && dartAngle < 207 ? 8
                          : dartAngle > 207 && dartAngle < 225 ? 16
                          : dartAngle > 225 && dartAngle < 243 ? 7
                          : dartAngle > 243 && dartAngle < 261 ? 19
                          : dartAngle > 261 && dartAngle < 279 ? 3
                          : dartAngle > 279 && dartAngle < 297 ? 17
                          : dartAngle > 297 && dartAngle < 315 ? 2
                          : dartAngle > 315 && dartAngle < 333 ? 15
                          : dartAngle > 333 && dartAngle < 351 ? 10
                          : dartAngle > 351 && dartAngle < 360 ? 6
                          : 0;

            calculatedScore = hitNumber * dartMultiplier;
        }

        return calculatedScore;
    }


}
