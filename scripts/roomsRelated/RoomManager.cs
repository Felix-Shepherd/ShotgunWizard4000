using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    public Transform player;
    public GameObject currentRoom;
    public RoomContainer roomContainer;
    public GameObject startRoom;
    public GameObject startingRoom;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    void Start()
    {
        //startroom
        currentRoom = startingRoom;
    }

    public void TransitionToRoom(ExitDirection exitDirectionFromCurrentRoom)
    {
        ExitDirection neededEntrance = GetOppositeExitDirection(exitDirectionFromCurrentRoom);

        GameObject newRoomPrefab = roomContainer.GetRandomRoomWithEntrance(neededEntrance);
        if (newRoomPrefab == null)
        {
            Debug.LogError("No valid room found for " + neededEntrance);
            return;
        }
        
        if (currentRoom != null)
        {
            Destroy(currentRoom);
        }
        foreach (var enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Destroy(enemy);
        }
        foreach (var coin in GameObject.FindGameObjectsWithTag("coin"))
        {
            Destroy(coin);
        }
        foreach (var soundfxs in GameObject.FindGameObjectsWithTag("soundFx"))
        {Destroy(soundfxs);}
        
        
        
        currentRoom = Instantiate(newRoomPrefab, Vector3.zero, Quaternion.identity);
        
        RoomMeta newRoomMeta = currentRoom.GetComponent<RoomMeta>();
        Transform entryPoint = newRoomMeta.GetExit(neededEntrance);

    if (entryPoint != null)
    {
        Vector3 offset = Vector3.zero;
        switch (neededEntrance)
        {
            case ExitDirection.Left:
                offset = new Vector3(1f, 0f, 0f); 
                break;
            case ExitDirection.Right:
                offset = new Vector3(-1f, 0f, 0f); 
                break;
            case ExitDirection.Top:
                offset = new Vector3(0f, -1f, 0f); 
                break;
            case ExitDirection.Bottom:
                offset = new Vector3(0f, 1f, 0f); 
                break;
      }

        player.position = entryPoint.position + offset;

        Collider2D exitCollider = entryPoint.GetComponent<Collider2D>();
        if (exitCollider != null)
        {
            Destroy(exitCollider.gameObject);
        }
}
        else
        {
            Debug.LogError("Entry point is null for " + neededEntrance);
        }
    }

    private ExitDirection GetOppositeExitDirection(ExitDirection dir)
    {
        switch (dir)
        {
            case ExitDirection.Top: return ExitDirection.Bottom;
            case ExitDirection.Bottom: return ExitDirection.Top;
            case ExitDirection.Left: return ExitDirection.Right;
            case ExitDirection.Right: return ExitDirection.Left;
            default: return dir;
            
        }
    }
}