using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    protected int dungeonLevel = 1;

    [SerializeField]
    protected GameObject[] floor1Monsters;
    protected GameObject[] floor2Monsters;

    public Dungeon PopulateRoom(GameObject room)
    {
        room.GetComponent<Room>().SpawnMonsters(floor1Monsters);
        return this;
    }

    public Dungeon IncreaseLevel()
    {
        this.dungeonLevel++;
        return this;
    }
}
