using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsTranslator
{
    private static ArrowsTranslator _sharedInstance;

    public static ArrowsTranslator GetInstance() => _sharedInstance ??= new ArrowsTranslator();
    private ArrowsTranslator(){}
    public enum ArrowDirection
    {
        None = 0,
        Up = 1,
        Down = 2,
        Right = 3,
        Left = 4,
        FinishUp = 5,
        FinishDown = 6,
        FinishRight = 7,
        FinishLeft = 8,
        TopRight = 9,
        TopLeft = 10,
        BottomRight = 11,
        BottomLeft = 12
    }
    public ArrowDirection CalculateArrow(Vector2 previousTile, Vector2 currentTile, Vector2 futureTile)
    {
        bool isFinal = futureTile == Vector2.zero;
        Vector2 pastDirection = previousTile != Vector2.zero ? (currentTile - previousTile) : new Vector2(0, 0);
        Vector2 futureDirection = futureTile != Vector2.zero ? (futureTile - currentTile) : new Vector2(0, 0);
        Vector2 direction = pastDirection != futureDirection ? pastDirection + futureDirection : futureDirection;

        if (direction == Vector2.up && !isFinal) return ArrowDirection.Up;
        if (direction == Vector2.down && !isFinal) return ArrowDirection.Down;
        if (direction == Vector2.right && !isFinal) return ArrowDirection.Right;
        if (direction == Vector2.left && !isFinal) return ArrowDirection.Left;

        if (direction == Vector2.up && isFinal) return ArrowDirection.FinishUp;
        if (direction == Vector2.down && isFinal) return ArrowDirection.FinishDown;
        if (direction == Vector2.right && isFinal) return ArrowDirection.FinishRight;
        if (direction == Vector2.left && isFinal) return ArrowDirection.FinishLeft;

        if (direction == new Vector2(1, 1))
        {
            if (pastDirection.y < futureDirection.y) return ArrowDirection.BottomLeft;
            
            else return ArrowDirection.TopRight;
        }
        if (direction == new Vector2(-1, 1))
        {
            if (pastDirection.y < futureDirection.y) return ArrowDirection.BottomRight;

            else return ArrowDirection.TopLeft;   
        }
        if (direction == new Vector2(1, -1))
        {
            if (pastDirection.y > futureDirection.y) return ArrowDirection.TopLeft;
            
            else return ArrowDirection.BottomRight;  
        }
        if (direction == new Vector2(-1, -1))
        {
            if (pastDirection.y > futureDirection.y) return ArrowDirection.TopRight;

            else return ArrowDirection.BottomLeft;
        }
        return ArrowDirection.None;
    }
}
