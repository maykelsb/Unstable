using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] rightRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;

    public GameObject[] noRooms;

    [SerializeField]
    private GameObject[] interiorWalls;

    public List<GameObject> rooms = new List<GameObject>();

    public GameObject boss;

    private void Start()
    {
        Invoke("FinishDungeon", 3f);
    }

    private void FinishDungeon()
    {
        // -- Create boss room & inactivate rooms
        for (int i = 0; i < rooms.Count; i++)
        {
            if (i == (rooms.Count - 1))
                Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
            if (i != 0)
                Debug.Log(true);
            //rooms[i].SetActive(false);
        }

        CancelInvoke("FinishDungeon");
    }

    public void BuildInteriorWalls(GameObject room)
    {
        switch (Random.Range(0, 3))
        {
            case 0:
            case 1:
                GameObject interiorWall = Instantiate(
                    interiorWalls[Random.Range(0, interiorWalls.Length)]
                );
                interiorWall
                    .GetComponent<InteriorWall>()
                    .Build(room);
                break;
        }
    }
}