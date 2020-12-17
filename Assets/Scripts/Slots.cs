using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public Reel reel1, reel2, reel3;
    public Reel[] reel;
    private bool startSpin;           //controls when to spin


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
            yield return new WaitForSeconds(Random.Range(1, 2));
            reel[i].spin = false;
            reel[i].RandomPosition();
        }
        WinChecker();
        startSpin = false;

    }

    void WinChecker()
    {
        Debug.Log(reel1.middleColour + reel2.middleColour + reel3.middleColour);

        if(reel1.middleColour == reel2.middleColour && reel2.middleColour == reel3.middleColour)
        {
            Debug.Log("BIG WIN");
        }
        else
        {
            Debug.Log("Try Again");
        }
    }
}
