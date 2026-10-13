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
    public LockCursor lockCursor;

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
                canInteract = false;
                setPlayerMovement(true);

                if (dialogue != null && dialogue.VariableStorage != null)
                {
                    dialogue.VariableStorage.SetValue("$ayanokoji_DialoguePhase", npc_Data.currentPhase.ToString());
                    dialogue.VariableStorage.SetValue("$suspiciousGuy_DialoguePhase", npc_Data.currentPhase.ToString());
                }

                dialogue.StartDialogue(npc_Data.startingNode);
                if (lockCursor != null) lockCursor.CursorUnlock();
            }
        }
    }
    [YarnCommand("dialoguePhaseSetter")]
    public void setYarnPhase()
    {
        InMemoryVariableStorage vData = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
        vData.SetValue("$ayanokoji_DialoguePhase", npc_Data.currentPhase.ToString());
        vData.SetValue("$suspiciousGuy_DialoguePhase", npc_Data.currentPhase.ToString());
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
    public void setPlayerMovement(bool b)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = !b;
        }
    }

    public void moveForDialog(Transform movePos)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && movePos != null)
        {
            player.transform.position = movePos.position;
        }
    }
    public void UnlockPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = true;
        }
        if (lockCursor != null)
        {
            lockCursor.CursorLock();
        }
    }
}
