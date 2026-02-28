using System.Collections.Generic;
using UnityEngine;

public class RoomContainer : MonoBehaviour
{
    public List<GameObject> roomsTop;    
    public List<GameObject> roomsBottom; 
    public List<GameObject> roomsLeft;   
    public List<GameObject> roomsRight;
    public RoomManager roommanager;
    
    //start rooms   
    public List<GameObject> startRooms;


    void Start()
    {
        setup();
    }

    [ContextMenu("Setup Rooms")]
    public void setup()
    {


        for (int i = 0; i < startRooms.Count; i++)
        {
            var roomPrefab = startRooms[i];
            if (roomPrefab == null) continue;

            RoomMeta meta = roomPrefab.GetComponent<RoomMeta>();
            if (meta == null)
            {
                Debug.LogWarning($"Start room '{roomPrefab.name}' has no RoomMeta component.");
                continue;
            }

            if (meta.topExit != null)
            {
                roomsTop.Add(roomPrefab);
            } 
            if (meta.bottomExit != null)
            {
                roomsBottom.Add(roomPrefab);
            } 
            if (meta.leftExit != null)
            {
                roomsLeft.Add(roomPrefab);
            } 
            if (meta.rightExit != null)
            {
                roomsRight.Add(roomPrefab);
            } 
        }

        Debug.Log($"Sorted rooms: Top={roomsTop.Count}, Bottom={roomsBottom.Count}, Left={roomsLeft.Count}, Right={roomsRight.Count}");
    }
    
    public GameObject GetRandomRoomWithEntrance(ExitDirection entranceDirection)
    {
        List<GameObject> listToChooseFrom = null;

        switch (entranceDirection)
        {
            case ExitDirection.Top:
                listToChooseFrom = roomsTop;
                break;
            case ExitDirection.Bottom:
                listToChooseFrom = roomsBottom;
                break;
            case ExitDirection.Left:
                listToChooseFrom = roomsLeft;
                break;
            case ExitDirection.Right:
                listToChooseFrom = roomsRight;
                break;
        }

        if (listToChooseFrom == null || listToChooseFrom.Count == 0)
        {
            Debug.LogError("no rooms or none that match what it wants idk");
            return null;
        }
        return listToChooseFrom[Random.Range(0, listToChooseFrom.Count)];
        
    }
}