using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsCtrl : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();

    public void BlockDeadDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].GetComponent<Door>().IsDeadEnd())
                doors[i].GetComponent<Door>().BlockDoor();
        }
    }
}
