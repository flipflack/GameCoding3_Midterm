using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    // add check for first scene loaded

    public static GameController instance {  get; private set; }

    public Transform startingLoc;
    public Vector3 loadLoc;
    public Quaternion loadRot;
    public GameObject firstPersonRig;

    public bool firstSceneLoaded = false;

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
}
