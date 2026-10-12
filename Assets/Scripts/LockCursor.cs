using UnityEngine;

public class LockCursor : MonoBehaviour
{
    private void Start()
    {
        CursorLock();
    }

    public void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
