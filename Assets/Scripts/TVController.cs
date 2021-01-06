using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TVController : MonoBehaviour
{
    public InMemoryVariableStorage TVControl;                       //refers to the yarn variable storage
    public GameObject TV1, TV2;                                     //tv screen game objects
    private int TVTrig;                                             //int for tv trigeer

    // Start is called before the first frame update
    void Start()
    {
        if (TVControl.GetValue("$TVOn") != null)                    // if the yarn value has no value...
        {
            TVControl.SetValue("$TVOn", 0);                         //set it to 0
        }
    }

    // Update is called once per frame
    void Update()
    {
        TVTrig = (int)TVControl.GetValue("$TVOn").AsNumber;         //get the yarn value of $TVOn and set it to TVTrig int 

        if (TVTrig == 0)                                            //if is 0...
        {
            TV1.SetActive(true);                                    //TV's on
            TV2.SetActive(true);
        }

        if (TVTrig == 1)                                            //if is 1...
        {
            TV1.SetActive(false);                                   //TVs off
            TV2.SetActive(false);
        }

    }
}
