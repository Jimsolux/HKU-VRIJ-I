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
                break;
            case TurnDirection.Right:
                break;
        }
    }

    public bool Turn()
    {
        deltaAngle += speed;

        switch (direction)
        {
            case TurnDirection.Left:
                if (deltaAngle >= 180) { return true; }
                Debug.Log("Left");
                break;

            case TurnDirection.Right:
                logbook.ClosePage(previous);
                if (deltaAngle <= -180) { return true; }
                Debug.Log("Right");
                break;
        }

        return false; 
    }
}
