using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnCtrl : MonoBehaviour
{
    public enum Doors { top, right, bottom, left };
    public Doors doorNeeded;

    private RoomTemplates scptTemplates;
    private Dungeon scptDungeon;

    void Start()
    {
        GameObject goGameController = GameObject //@todo review this
            .FindGameObjectWithTag("Rooms");
        scptTemplates = goGameController.GetComponent<RoomTemplates>();
        scptDungeon = goGameController.GetComponent<Dungeon>();

        Invoke("SpawnRoom", 0.1f);
    }

    void SpawnRoom()
    {
        string thisDoor = "";
        string otherDoor = "";

        GameObject goNewRoom;

        switch (doorNeeded) {
            case Doors.top:
                thisDoor = "DoorDown"; //@todo shouldn't be an enum?
                otherDoor = "DoorUp";
                goNewRoom = this.InstantiateRoom(scptTemplates.topRooms);
                break;
            case Doors.right:
                thisDoor = "DoorLeft";
                otherDoor = "DoorRight";
                goNewRoom = this.InstantiateRoom(scptTemplates.rightRooms);
                break;
            case Doors.bottom:
                thisDoor = "DoorUp";
                otherDoor = "DoorDown";
                goNewRoom = this.InstantiateRoom(scptTemplates.bottomRooms);
                break;
            case Doors.left:
            default:
                thisDoor = "DoorRight";
                otherDoor = "DoorLeft";
                goNewRoom = this.InstantiateRoom(scptTemplates.leftRooms);
                break;
        }

        // -- Attaching new room to its door
        Door door = FindLeadingDoor(thisDoor)
            .GetComponent<Door>();
        door.leadsTo = goNewRoom;

        // -- Attaching last room to this door
        Door oDoor = FindLeadingDoor2(goNewRoom.transform, otherDoor)
            .GetComponent<Door>();
        oDoor.leadsTo = transform.parent.parent.gameObject;

        CancelInvoke("SpawnRoom");
    }

    private GameObject InstantiateRoom(GameObject[] availableRooms)
    {
        GameObject goNewRoom = Instantiate(
            availableRooms[Random.Range(0, availableRooms.Length)],
            transform.position,
            Quaternion.identity
        );

        scptTemplates.BuildInteriorWalls(goNewRoom); //@todo this should be call upon Room.cs and not RoomTemplate.cs

        //@question Couldn't this generate an overhead?
        scptDungeon.PopulateRoom(goNewRoom); //@question Is a global control needed?

        scptTemplates.rooms.Add(goNewRoom); //@todo this should be call upon Room.cs and not RoomTemplate.cs

        return goNewRoom;
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
