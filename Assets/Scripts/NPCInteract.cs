using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class NPCInteract : MonoBehaviour
{   
    public TextMeshProUGUI interactText;
    public string interaction;
    private bool canInteract;
    public DialogueRunner dialogue;
    public NPC_Data npc_Data;

    //public NPC_Cam_Control camController;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            setText(interaction);
            canInteract = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            setText("");
            canInteract = false;
        }
    }

    public void setText(string txt)
    {
        interactText.text = txt;
    }

    public void Update()
    {
        if (canInteract)
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                setText("");
                dialogue.StartDialogue(npc_Data.startingNode);
                canInteract = false;
            }
        }
    }

    [YarnCommand("dialoguePhaseSetter")]
    public void setYarnPhase()
    {
        InMemoryVariableStorage vData = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
        vData.SetValue("$dialoguePhase", npc_Data.currentPhase.ToString());
    }

    [YarnCommand("dialoguePhaseGetter")]
    public void getYarnPhase(string phase)
    {
        if(phase == "QuestTaken")
        {
            npc_Data.currentPhase = NPC_Data.dialoguePhase.QuestTaken;
        }
        if(phase == "QuestCompleted")
        {
            npc_Data.currentPhase = NPC_Data.dialoguePhase.QuestCompleted;
        }
        if(phase == "QuestFumbled")
        {
            npc_Data.currentPhase = NPC_Data.dialoguePhase.QuestFumbled;
        }
    }

    public void OnApplicationQuit()
    {
        npc_Data.resetData();
    }
}
