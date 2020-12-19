using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    public bool spin;           //controls when to spin
    public int speed;           //reel spin speed
    public GameObject winLine;
    private Vector3 middlePosition;
    public string middleColour;
     

    // Start is called before the first frame update
    void Start()
    {
        spin = false;
        speed = 15;
        //   middlePosition = RectTransform(0, 0, 0);
   
    }

    // Update is called once per frame
    void Update()
    {
        if (spin)
        {
            foreach(Transform image in transform)
            {
                image.transform.Translate(Vector3.down * Time.smoothDeltaTime * speed, Space.World);

                if (image.transform.position.y <= 0)
                {
                    image.transform.position = new Vector3(image.position.x, image.transform.position.y + 600, image.position.z);
                }
            }
        }
    }

    public void RandomPosition()
    {
        //List<int> middlePart = new List<int>();
        List<float> parts = new List<float>();

        parts.Add(0.25f);
        parts.Add(0.125f);
        parts.Add(0);
        parts.Add(-0.125f);
        parts.Add(-0.25f);
        parts.Add(-0.375f);


        foreach (Transform image in transform)
        {
            int rand = Random.Range(0, parts.Count);

            image.transform.position = new Vector3(image.position.x, parts[rand] + transform.parent.GetComponent<RectTransform>().transform.position.y, image.position.z);

            middlePosition = new Vector3(image.position.x, winLine.transform.position.y, image.position.z);

            if (image.position == middlePosition)
            {               
               // Debug.Log(image.name);
                middleColour = image.name;
            }

            parts.RemoveAt(rand);

        }
    }
}
