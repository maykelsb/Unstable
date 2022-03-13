using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public void CheckDoors()
    {
        for (var i = 0; i < transform.GetChild(1).transform.childCount; i++)
        {
            Door doorScpt = transform.GetChild(1).transform.GetChild(i).GetComponent<Door>();
            if (doorScpt && doorScpt.IsDeadEnd())
                doorScpt.BlockDoor();
        }
    }
}
