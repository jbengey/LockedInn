using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    public bool spin;           //controls when to spin
    public int speed;           //reel spin speed
    private string middleColour;
   // private RectTransform middlePosition;
    private Vector3 middlePosition;

    // Start is called before the first frame update
    void Start()
    {
        spin = false;
        speed = 1500;
        //   middlePosition = RectTransform(0, 0, 0);
        middlePosition = new Vector3(0f, 0f, 0f);
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
                    image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y + 600, image.transform.position.z);
                }
            }
        }
    }

    public void RandomPosition()
    {
        //List<int> middlePart = new List<int>();
        List<int> parts = new List<int>();

        parts.Add(200);
        parts.Add(100);
        parts.Add(0);
        parts.Add(-100);
        parts.Add(-200);
        parts.Add(-300);


        foreach (Transform image in transform)
        {
            int rand = Random.Range(0, parts.Count);

            image.transform.position = new Vector3(image.transform.position.x, parts[rand] + transform.parent.GetComponent<RectTransform>().transform.position.y, image.transform.position.z);

            //Debug.Log(image.name);

            if (image.position == middlePosition)
            {
                Debug.Log(image.name);
            }

            parts.RemoveAt(rand);

        }
    }
}
