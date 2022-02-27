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
    //private bool spawned = false;

    void Start()
    {
        templates = GameObject
            .FindGameObjectWithTag("Rooms")
            .GetComponent<RoomTemplates>();
        Invoke("SpawnRoom", 5.0f);
    }

    void SpawnRoom()
    {
        switch (doorNeeded) {
            case Doors.top: roomList = templates.topRooms; break;
            case Doors.right: roomList = templates.rightRooms; break;
            case Doors.bottom: roomList = templates.bottomRooms; break;
            case Doors.left: roomList = templates.leftRooms; break;
        }
        randomRoomIndx = Random.Range(0, roomList.Length);
        Instantiate(
            roomList[randomRoomIndx],
            transform.position,
            roomList[randomRoomIndx].transform.rotation
        );
        //spawned = true;
        CancelInvoke("SpawnRoom");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("colidou");
        if (other.CompareTag("SpawnPoint")/* && other.GetComponent<RoomSpawnCtrl>().spawned*/)
        {
            Debug.Log("here");
            Destroy(gameObject);
        }
    }
}
