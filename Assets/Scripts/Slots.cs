using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public Reel[] reel;
    bool startSpin;

    // Start is called before the first frame update
    void Start()
    {
        startSpin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startSpin)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                startSpin = true;
                StartCoroutine(Spinning());
            }
        }
    }

    IEnumerator Spinning()
    {
        foreach (Reel spinner in reel)
        {
            spinner.spin = true;
        }

        for (int i = 0; i < reel.Length; i++)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));
            reel[i].spin = false;
            reel[i].RandomPosition();
        }

        startSpin = false;

    }

}
