using UnityEngine;

public class Bumpable
{
    protected bool bumped = false;

    public void Bump(Monster monster, Windrose.Direction attackDirection)
    {
        Vector2 impulseDirection = GetImpulseDirection(attackDirection);
        monster.SetVelocity(new Vector3())
            .Impulse(impulseDirection *3.0f)
            .remainingTime = 0.5f;

        return;
    }

    private Vector2 GetImpulseDirection(Windrose.Direction attackDirection)
    {
        switch (attackDirection)
        {
            case Windrose.Direction.Up:
                return Vector2.up;
            case Windrose.Direction.Down:
                return Vector2.down;
            case Windrose.Direction.Left:
                return Vector2.left;
            case Windrose.Direction.Right:
                return Vector2.right;
            default:
                return new Vector2();
        }
    }
}
