using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class GameController : MonoBehaviour
{
    // add check for first scene loaded
    public static GameController instance {  get; private set; }

    [Header("Spawn References")]
    public Transform startingLoc;
    public Vector3 loadLoc;
    public Quaternion loadRot;
    public GameObject firstPersonRig;

    [Header("Scene Load Check")]
    public bool firstSceneLoaded = false;

    [Header("Checks")]
    public static bool ranFadeSequence = false;
    public static bool ranTitleSequence = false;

    public NPC_Data ayanokojiData; 

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);    
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            if(!firstSceneLoaded)
            {
                Debug.Log("Spawned Character at New Positions");
                SpawnCharacter(startingLoc.position, startingLoc.rotation);
                firstSceneLoaded = true;
            }
            else
            {
                SpawnCharacter(loadLoc, loadRot);
            }
        }
    }
    public void SpawnCharacter(Vector3 pos, Quaternion rot) 
    {
        Instantiate(firstPersonRig, pos, rot);
    }

    public void SetSpawnPoint(Vector3 loc, Quaternion rot)
    {
        loadLoc = loc;
        loadRot = rot;
    }

    
    [YarnCommand("set_quest_phase")]
    public void SetQuestPhase(string phase)
    {
        if (ayanokojiData == null)
        {
            Debug.LogError("[GameController] ayanokojiData is NOT assigned in the Inspector!");
            return;
        }

        if (phase == "QuestTaken")
        {
            ayanokojiData.currentPhase = NPC_Data.dialoguePhase.QuestTaken;
        }
        else if (phase == "QuestCompleted")
        {
            ayanokojiData.currentPhase = NPC_Data.dialoguePhase.QuestCompleted;
        }
        else if (phase == "QuestFumbled")
        {
            ayanokojiData.currentPhase = NPC_Data.dialoguePhase.QuestFumbled;
        }

        Debug.Log($"[GameController] Ayanokoji phase is now: {ayanokojiData.currentPhase}");
    }
}
