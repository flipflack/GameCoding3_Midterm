using Unity.VisualScripting;
using UnityEngine;

public class HallwaySpawn : MonoBehaviour
{
    private void Start()
    {
        GameController.instance.SpawnCharacter(this.transform.position, this.transform.rotation);
    }
}
