using UnityEngine;

public class Stallable
{
    //protected bool stalled = false;

    public void Stall(Monster monster)
    {
        //if (!stalled)
        //{
            monster.SetVelocity(new Vector3());
            //stalled = true;
            //return;
        //}
    }
}
