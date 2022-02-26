using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnCtrl : MonoBehaviour
{
    public enum Doors { top, right, bottom, left };
    public Doors doorNeeded;

    void Update()
    {
        switch (doorNeeded) {
            case Doors.top:
                break;
            case Doors.right:
                break;
            case Doors.bottom:
                break;
            case Doors.left:
                break;
        }
    }
}
