using UnityEngine;

public enum ExitDirection { Top, Bottom, Left, Right }

public class RoomMeta : MonoBehaviour
{
    public Transform topExit;
    public Transform bottomExit;
    public Transform leftExit;
    public Transform rightExit;

    public Transform GetExit(ExitDirection dir)
    {
        switch (dir)
        {
            case ExitDirection.Top: return topExit;
            case ExitDirection.Bottom: return bottomExit;
            case ExitDirection.Left: return leftExit;
            case ExitDirection.Right: return rightExit;
        }
        return null;
    }
}