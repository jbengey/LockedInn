using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class NPC3D : MonoBehaviour
{
    public string characterName = "";
    public string talkToNode = "";
    public YarnProgram scriptToLoad;
    DialogueRunner dialogueRunner; //refernce to the dialogue control
    private GameObject dialogueCanavas; //refernce to the canvas
    public GameObject NPC;                                              //GameObject of Keeper
    private Vector3 PostionSpeechBubble = new Vector3(0f, 1.1f, 0f);
    private Vector3 ResetSpeechBubble = new Vector3(0f, -5f, 0f);     //Vector3 to reset canvas location 
    private Vector3 NPCRoatation;                                       //Vector3 for the Keeper roation

    // Start is called before the first frame update
    void Start()
    {
        if (scriptToLoad == null) Debug.LogError("NPC3D not set up with yarn scriptToLoad ",this);
        if (string.IsNullOrEmpty(characterName)) Debug.LogWarning("NPC3D not set up with characterName", this);
        if (string.IsNullOrEmpty(talkToNode)) Debug.LogError("NPC3D not set up with talkToNode", this);

        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>(); //this is bad way to do this but hey we doing this quickly
        if (dialogueRunner == null) Debug.LogError("dialogueRunner not set up", this);
        dialogueCanavas = GameObject.Find("Dialogue Canvas"); //this is bad way to do this but hey we doing this quickly
        if (dialogueCanavas == null) Debug.LogError("Dialogue Canvas not set up",this);
        if (scriptToLoad != null && dialogueRunner != null && dialogueRunner != null) 
        {
            dialogueRunner.Add(scriptToLoad); //adds the script to the dialogue system
        }        
    }

    private void Update()
    {

          var angleX = NPC.transform.eulerAngles.x;                  //finds and sets to variable Keeper x rotation
          var angleY = NPC.transform.eulerAngles.y - 180f;            //finds and sets to variable Keeper y rotation, - 180 so is correct
          var angleZ = NPC.transform.eulerAngles.z;                  //finds and sets to variable keeper z rotation
          NPCRoatation = new Vector3(angleX, angleY, angleZ);        //sets Vector3 to current Keeper rotation
    }

    private void OnTriggerEnter(Collider other)
    {
        //if other is player
        if (other.gameObject.CompareTag("XRPlayer"))
        {
            if (!string.IsNullOrEmpty(talkToNode))
            {
                if (dialogueCanavas != null)
                {
                    //move the Canvas to the object and off set
                    dialogueCanavas.transform.SetParent(transform.parent.transform); // use the root to prevent scaling
                    dialogueCanavas.GetComponent<RectTransform>().anchoredPosition3D = transform.parent.TransformVector(PostionSpeechBubble);

                    Quaternion target = Quaternion.Euler(NPCRoatation);             //finds and stores the roation of the keeper              
                    dialogueCanavas.transform.rotation = target;                    //sets dialogue canvas to the same, ensuring the are aligned    
                }

                if (dialogueRunner.IsDialogueRunning)
                {
                    dialogueRunner.Stop();
                }
                dialogueRunner.StartDialogue(talkToNode);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dialogueCanavas.transform.SetParent(transform.parent.transform); // use the root to prevent scaling
        dialogueCanavas.GetComponent<RectTransform>().anchoredPosition3D = transform.parent.TransformVector(ResetSpeechBubble);     //ensures dialougue canvas is hidden when not in use
    }

}
