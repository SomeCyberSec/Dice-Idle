using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionStatus
{
    public Vector2 Position { get; set; }
    public bool IsOccupied { get; set; }

    public PositionStatus(Vector2 position, bool isOccupied)
    {
        Position = position;
        IsOccupied = isOccupied;
    }
}
