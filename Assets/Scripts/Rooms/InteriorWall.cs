using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorWall : MonoBehaviour
{
    public enum WallKind { Full, HalfX, HalfY }

    [SerializeField] private WallKind kind;

    [SerializeField] private bool flipX;
    [SerializeField] private bool flipY;

    [SerializeField] private float[] allowedX; // -- displace of 9 units (3/-6) on flip
    [SerializeField] private float[] allowedY;

    [SerializeField] private int offsetMirrorX;

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

    public InteriorWall Build(GameObject room)
    {
        return SetParent(room)
            .FlipX()
            .SetPosition();
    }

    private InteriorWall SetParent(GameObject room) //@move to Room.cs
    {
        transform.SetParent(room.transform);
        return this;
    }

    private InteriorWall FlipX()
    {
        if (!CanFlipX())
        {
            return this;
        }

        int newX = ((Random.Range(0, 2) * 2) - 1);
        if (newX < 0)
        {
            flippedX = true;
            transform.localScale = new Vector3(newX, 1f, 1f);
        }

        return this;
    }

    private InteriorWall SetPosition()
    {
        transform.localPosition = new Vector3(
            GetXPos(),
            GetYPos()
        );

        return this;
    }

    private float GetXPos()
    {
        float newX = this.allowedX[Random.Range(0, this.allowedX.Length)];
        if (this.flippedX)
            return newX + this.offsetMirrorX;

        return newX;
    }

    private float GetYPos()
    {
        float newY = this.allowedY[Random.Range(0, this.allowedY.Length)];

        return newY;
    }
}
