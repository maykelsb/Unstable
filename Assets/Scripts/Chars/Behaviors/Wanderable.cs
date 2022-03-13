using UnityEngine;

public class Wanderable
{
    private Windrose.Direction wanderingDirection;

    public Windrose.Direction WanderingDirection
    {
        get => wanderingDirection;
        set => wanderingDirection = value;
    }

    public void Wander(Monster monster)
    {
        float moveX = 0f, moveY = 0f;

        if (Windrose.Up() == WanderingDirection) moveY = 0.001f;
        if (Windrose.Down() == WanderingDirection) moveY = -0.001f;
        if (Windrose.Left() == WanderingDirection) moveX = -0.001f;
        if (Windrose.Right() == WanderingDirection) moveX = 0.001f;

        monster.Flip((moveX) < 0);
        monster.SetVelocity(new Vector3(moveX, moveY).normalized);
     }
}
