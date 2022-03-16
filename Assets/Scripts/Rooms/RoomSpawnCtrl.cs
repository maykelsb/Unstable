using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnCtrl : MonoBehaviour
{
    public enum Doors { top, right, bottom, left };
    public Doors doorNeeded;

    private RoomTemplates templates;
    private int randomRoomIndx;
    private GameObject[] roomList;

    void Start()
    {
        templates = GameObject
            .FindGameObjectWithTag("Rooms")
            .GetComponent<RoomTemplates>();
        Invoke("SpawnRoom", 0.1f);
    }

    void SpawnRoom()
    {
        string thisDoor = "";
        string otherDoor = "";

        switch (doorNeeded) {
            case Doors.top:
                thisDoor = "DoorDown";
                otherDoor = "DoorUp";
                roomList = templates.topRooms;
                break;
            case Doors.right:
                thisDoor = "DoorLeft";
                otherDoor = "DoorRight";
                roomList = templates.rightRooms;
                break;
            case Doors.bottom:
                thisDoor = "DoorUp";
                otherDoor = "DoorDown";
                roomList = templates.bottomRooms;
                break;
            case Doors.left:
                thisDoor = "DoorRight";
                otherDoor = "DoorLeft";
                roomList = templates.leftRooms;
                break;
        }
        randomRoomIndx = Random.Range(0, roomList.Length);

        // -- Attaching new room to its door
        Door door = FindLeadingDoor(thisDoor)
            .GetComponent<Door>();

        GameObject newRoom = Instantiate(
            roomList[randomRoomIndx],
            transform.position,
            Quaternion.identity
        );
        door.leadsTo = newRoom;

        templates.GetFullInteriorWall(newRoom);

        // -- Saving a reference of new room
        templates.rooms.Add(newRoom);

        // -- Attaching last room to this door
        Door oDoor = FindLeadingDoor2(newRoom.transform, otherDoor)
            .GetComponent<Door>();
        oDoor.leadsTo = transform.parent.parent.gameObject;

        CancelInvoke("SpawnRoom");
    }

    private GameObject FindLeadingDoor(string tagName)
    {
        GameObject leadingRoom;
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            leadingRoom = transform.parent.GetChild(i).gameObject;
            if (leadingRoom.CompareTag(tagName))
                return leadingRoom;
        }
        return null;
    }

    // -- Whyyyyyyyyyyyyyyyy????
    private GameObject FindLeadingDoor2(Transform transf, string tagName)
    {
        GameObject leadingRoom;
        for (int i = 0; i < transf.GetChild(1).childCount; i++)
        {
            leadingRoom = transf.GetChild(1).GetChild(i).gameObject;
            if (leadingRoom.CompareTag(tagName))
                return leadingRoom;
        }
        return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
// -- Pensar no que pode ser transferido para RoomTemplate e transforma-lo no Ctrl, aqui vira apenas RoomSpawnPoint
}
