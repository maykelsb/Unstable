using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private enum States { Fresh, FirstActivation, Populated, Cleared };
    private States roomState;

    [SerializeField] private int monsterSpawnPoints;

    //public global::System.Int32 MonsterSpawnPoints { get => monsterSpawnPoints; set => monsterSpawnPoints = value; }

    //private void Start()
    //{
    //    this.monsterSpawnPoints = (Random.Range(1, 7)
    //        + Random.Range(1, 7));

    //    Debug.Log(this.transform + "/MSP: " + this.monsterSpawnPoints);
    //}


    //private void Awake()
    //{
    //    switch (roomState)
    //    {
    //        case States.FirstActivation:
    //            GameObject
    //                .FindGameObjectWithTag("Rooms")
    //                .GetComponent<Dungeon>()
    //                .PopulateRoom(gameobject);

    //            this.roomState = States.Populated;
    //            break;

    //        case States.Fresh: //@question if a global control is needed move to RoomSpawnCtrl.cs
    //            this.MonsterSpawnPoints = Random.Range(1, 7)
    //                + Random.Range(1, 7);
    //            break;
    //        default:
    //            Debug.Log("Room created");
    //            break;
    //    }






    //    Debug.Log("Back to work!!");
    //    //switch (roomState)
    //    //{
    //    //    case States.Fresh:
    //    //        // CloseDoors();
    //    //        return;
    //    //    case States.Populated:
    //    //        return;
    //    //    case States.Inactive:
    //    //        return;
    //    //    case States.Reactivated:
    //    //        return;
    //    //    case States.Cleared:
    //    //        return;
    //    //}
    //}

    public void CheckDoors()
    {
        for (var i = 0; i < transform.GetChild(1).transform.childCount; i++)
        {
            Door doorScpt = transform.GetChild(1).transform.GetChild(i).GetComponent<Door>();
            if (doorScpt && doorScpt.IsDeadEnd())
                doorScpt.BlockDoor();
        }
    }

    public Room SpawnMonsters(GameObject[] monsters)
    {
        if (States.Fresh != this.roomState)
            return this;

        this.monsterSpawnPoints = (Random.Range(1, 7)
            + Random.Range(1, 7));

        do
        {
            GameObject goMonster = Instantiate(
                monsters[Random.Range(0, monsters.Length)]
            );

            Monster scptMonster = goMonster.GetComponent<Monster>();


            Debug.Log(this.transform + "/MSP: " + this.monsterSpawnPoints + "/" + scptMonster.ThreatLevel);

            this.monsterSpawnPoints = (this.monsterSpawnPoints - scptMonster.ThreatLevel);

            Debug.Log(this.transform + "/MSP: " + this.monsterSpawnPoints + "/" + scptMonster.ThreatLevel);


            goMonster.transform.SetParent(transform); //@todo move to Room.cs
            goMonster.transform.localPosition = new Vector3();
            // check colisions, reposition


        } while (this.monsterSpawnPoints > 0);

        //Debug.Log(this.MonsterSpawnPoints);
        //Debug.Log(monsters.ThreatLevel);

        this.roomState = States.Populated;

        return this;
    }
}

