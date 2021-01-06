using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public FMButton button;                     //button to control activation
    public Reel reel1, reel2, reel3;            //references to each reels script
    public Reel[] reel;                         //sets an array of reels
    private bool startSpin;                     //controls when to spin
    public TMPro.TMP_Text playerMessage;        //creates text
    public AudioSource spinning, win, lose;     //audio sources for sound effects

    void Start()
    {
        startSpin = false;                      //ensures not spinning
        playerMessage.text = "";                //ensures text field is blank
    }

    void Update()
    {
        if (!startSpin)                         //if not spinning...
        {
            if (button.buttonPressed == true)   //references button script for bool and if true...
            {
                startSpin = true;               //set to true
                StartCoroutine(Spinning());     //start coroutine
                playerMessage.text = "";        //ensures text field is blank
            }
        }
    }

    IEnumerator Spinning()
    {
        spinning.Play();                                                //plays spinning sound effect
        foreach (Reel spinner in reel)                                  //for each of the reels...
        {
            spinner.spin = true;                                        //sets to true and starts spinning            
        }

        for (int i = 0; i < reel.Length; i++)                           //allows the each reel t spin for its own random time between values
        {
            yield return new WaitForSeconds(Random.Range(1, 2));
            reel[i].spin = false;
            reel[i].RandomPosition();
        }
        WinChecker();                               //calls win checker function
        startSpin = false;                          //allows spin to be able to occur again
        spinning.Stop();                            //stops spinning sound effect
    }

    void WinChecker()
    {
        Debug.Log(reel1.middleColour + reel2.middleColour + reel3.middleColour);        //debug of images on winline

        if(reel1.middleColour == reel2.middleColour && reel2.middleColour == reel3.middleColour && reel1.middleColour == reel3.middleColour)    //if the images on each reel have the same name... 
        {
            playerMessage.text = "Big Win!!";           //text field shows...
            win.Play();                                 //plays win sound effect
        }
        else
        {
            playerMessage.text = "Try Again!!";         //text field shows...
            lose.Play();                                //plays lose sound effect
        }
    }
}
