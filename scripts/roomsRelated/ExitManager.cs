using UnityEngine;

public class RoomExit : MonoBehaviour
{
    public ExitDirection exitDirection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RoomManager.Instance.TransitionToRoom(exitDirection);
        }
    }
}