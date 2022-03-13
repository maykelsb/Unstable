using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private enum Direction { Up, Down, Left, Right }
    private enum Status { Opened, Closed, Blocked/*, Locked*/ }

    [SerializeField] private Status status;
    [SerializeField] private Direction direction;
    [SerializeField] private Animator animator;

    public GameObject leadsTo;

    public void Start()
    {
        GameObject
            .FindGameObjectWithTag("Rooms")
            .GetComponent<DoorsCtrl>()
            .doors
            .Add(gameObject);
    }

    public bool IsOpen()
    {
        return (status == Status.Opened);
    }

    public bool IsDeadEnd()
    {
        return (null == leadsTo);
    }

    public void OpenDoor()
    {
        status = Status.Opened;
        animator.SetTrigger("OpenDoor");
    }

    public void CloseDoor()
    {
        status = Status.Closed;
        animator.SetTrigger("CloseDoor");
    }

    public void BlockDoor()
    {
        status = Status.Blocked;
        animator.SetTrigger("BlockDoor");
    }
}
