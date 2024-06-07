using System.IO;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PageTurner 
{
    public enum TurnDirection
    {
        Left = 1, Right = -1
    }

    LogbookManager logbook;
    TurnDirection direction;
    private float deltaAngle;
    private float speed = 3f;
    private int previous;
    private int current; 

    public PageTurner(LogbookManager logbook, TurnDirection direction, int previousPage, int currentPage)
    {
        this.logbook = logbook;
        this.direction = direction;
        this.previous = previousPage;
        this.current = currentPage;

        switch (direction)
        {
            case TurnDirection.Left:
                logbook.leftPages[previous].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
                logbook.rightPages[current].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case TurnDirection.Right:
                logbook.leftPages[current].transform.localRotation = Quaternion.Euler(new Vector3(-180, 0, -90));   
                logbook.rightPages[previous].transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
        }
    }

    public bool Turn()
    {
        deltaAngle += speed;

        switch (direction)
        {
            case TurnDirection.Left:
                logbook.leftPages[previous].transform.localRotation = Quaternion.Euler(Vector3.RotateTowards(new Vector3(0, 0, -90), new Vector3(0, 180, -90), speed, speed));
                Quaternion.Euler(Vector3.RotateTowards(new Vector3(180, 0, 0), new Vector3(0, 0, 0), speed, speed));
                logbook.rightPages[current].transform.localRotation = Quaternion.Euler(Vector3.RotateTowards(new Vector3(180, 0, 0), new Vector3(0, 0, 0), speed, speed));
                logbook.ClosePage(previous);
                if (deltaAngle >= 180) { return true; }
                Debug.Log("Left");
                break;

            case TurnDirection.Right:
                logbook.leftPages[current].transform.localRotation = Quaternion.Euler(logbook.leftPages[current].transform.localRotation.eulerAngles + new Vector3(0, speed, 0));
                logbook.rightPages[previous].transform.localRotation = Quaternion.Euler(logbook.rightPages[previous].transform.localRotation.eulerAngles + new Vector3(speed, 0, 0));
                logbook.ClosePage(previous);
                if (deltaAngle <= -180) { return true; }
                Debug.Log("Right");
                break;
        }

        return false; 
    }
}
