using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseEnter : MonoBehaviour
{
    // in game, add a enter position
    public Transform HouseReturn;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.instance.SetSpawnPoint(HouseReturn.position, HouseReturn.rotation);
            SceneManager.LoadScene(1);
        }
    }
}
