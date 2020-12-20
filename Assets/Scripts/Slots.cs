using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public FMButton button;
    public Reel reel1, reel2, reel3;
    public Reel[] reel;
    private bool startSpin;           //controls when to spin
    public TMPro.TMP_Text playerMessage;


    // Start is called before the first frame update
    void Start()
    {
        startSpin = false;
        playerMessage.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!startSpin)
        {
            if (button.buttonPressed == true)
            {
                startSpin = true;
                StartCoroutine(Spinning());
                playerMessage.text = "";
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

        if(reel1.middleColour == reel2.middleColour && reel2.middleColour == reel3.middleColour && reel1.middleColour == reel3.middleColour)
        {
            Debug.Log("BIG WIN");
            playerMessage.text = "Big Win!!";
        }
        else
        {
            Debug.Log("Try Again");
            playerMessage.text = "Try Again!!";
        }
    }
}
