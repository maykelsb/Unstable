using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorWall : MonoBehaviour
{
    public enum WallKind { Full, HalfX, HalfY }

    [SerializeField]
    private WallKind kind;

    [SerializeField]
    private bool flipX;
    [SerializeField]
    private bool flipY;

    [SerializeField]
    private int[] allowedX; // -- displace of 9 units (3/-6)
    [SerializeField]
    private int[] allowedY;

    private bool flippedX = false;
    private bool flippedY = false;

    public bool CanFlipX()
    {
        return flipX;
    }

    public bool CanFlipY()
    {
        return flipY;
    }

    public bool IsFullWall()
    {
        return (WallKind.Full == kind);
    }

    public bool IsHalfYWall()
    {
        return (WallKind.HalfY == kind);
    }

    public bool IsHalfXWall()
    {
        return (WallKind.HalfX == kind);
    }

    public bool IsFlippedX()
    {
        return flippedX;
    }

    public bool IsFlippedY()
    {
        return flippedY;
    }
}
