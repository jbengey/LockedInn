using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TVController : MonoBehaviour
{
    public InMemoryVariableStorage TVControl;
    public GameObject TV1, TV2;
    private int TVTrig;

    // Start is called before the first frame update
    void Start()
    {
        if (TVControl.GetValue("$TVOn") != null)
        {
            TVControl.SetValue("$TVOn", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TVTrig = (int)TVControl.GetValue("$TVOn").AsNumber;

        if (TVTrig == 0)
        {
            TV1.SetActive(true);
            TV2.SetActive(true);
        }

        if (TVTrig == 1)
        {
            TV1.SetActive(false);
            TV2.SetActive(false);
        }

    }
}
