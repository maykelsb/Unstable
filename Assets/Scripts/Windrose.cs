using UnityEngine;

public static class Windrose
{
    public enum Direction { Up, Right, Down, Left} //@TODO transform this in Vector3/Vector2?

    public static Direction getRandomDirection()
    {
        int indx = Random.Range(0, 4);
        switch (indx)
        {
            case 1: return Direction.Down;
            case 2: return Direction.Left;
            case 3: return Direction.Right;
        }
        return Direction.Up;
    }

    public static Direction Up()
    {
        return Direction.Up;
    }

    public static Direction Right()
    {
        return Direction.Right;
    }

    public static Direction Down()
    {
        return Direction.Down;
    }

    public static Direction Left()
    {
        return Direction.Left;
    }
}
