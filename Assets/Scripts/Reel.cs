using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    public bool spin;                   //controls when to spin
    public int speed;                   //reel spin speed
    public GameObject winLine;          //object to give a positional reference
    private Vector3 middlePosition;     //variablie for comparison     
    public string middleColour;         //string to contain 
    public float spinPosition;          //value to help align images


    void Start()
    {
        spin = false;               //sets bool to false
        speed = 35;                 //sets spin speed
        spinPosition = 2;           //sets value to help set image positions
    }

    void Update()
    {
        if (spin)                                                                                                                                   //if true...
        {
            foreach(Transform image in transform)                                                                                                   //targets all objects in the parent object
            {
                image.transform.Translate(Vector3.down * Time.smoothDeltaTime * speed, Space.World);                                                //diection and speed of movement

                if (image.transform.position.y <= 0)                                                                                                //if image goes below 0
                {
                    image.transform.position = new Vector3(image.position.x, image.transform.position.y + spinPosition, image.position.z);          //set new position
                }
            }
        }
    }

    public void RandomPosition()
    {
        List<float> parts = new List<float>();          //creates new list of floats
                
        parts.Add(0.25f);                               //sets values based Y positions of images
        parts.Add(0.125f);
        parts.Add(0);
        parts.Add(-0.125f);
        parts.Add(-0.25f);
        parts.Add(-0.375f);


        foreach (Transform image in transform)                  //targets all objects in the parent object
        {
            int rand = Random.Range(0, parts.Count);            //creates randome numbers based on the above list values

            image.transform.position = new Vector3(image.position.x, parts[rand] + transform.parent.GetComponent<RectTransform>().transform.position.y, image.position.z);      //change image postion

            middlePosition = new Vector3(image.position.x, winLine.transform.position.y, image.position.z);         //sets Vector3 to value of winlines y and image x and z

            if (image.position == middlePosition)                                                                   //if the image y is equal to middle position value
            {               
                middleColour = image.name;                                                                          //set string to the image name
            }

            parts.RemoveAt(rand);

        }
    }
}
