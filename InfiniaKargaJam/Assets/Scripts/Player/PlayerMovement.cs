using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public void Move(Vector2 vector)
    {
        if (vector==Vector2.left)
        {
            Debug.Log("Left");
        }
        else if (vector==Vector2.right)
        {
            Debug.Log("Right");
        }
        else if (vector == Vector2.up)
        {
            Debug.Log("Up");
        }
        else if (vector == Vector2.down)
        {
            Debug.Log("Down");
        }
    }
}
