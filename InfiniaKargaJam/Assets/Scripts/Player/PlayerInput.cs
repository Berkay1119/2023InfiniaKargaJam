using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float holdThresholdAsSecond=2f;
    private Coroutine[] holdCoroutine = new Coroutine[4];
    private bool[] coroutineIsRunning = new bool[4];
    private bool[] coroutineIsCompleted = new bool[4];
    
    private void OnMessageArrived(string msg)
    {
        string[] stringArray=msg.Split(" ");

        
        if (stringArray[1]=="0")
        {
            if (!coroutineIsRunning.Contains(true))
            {
                coroutineIsRunning[int.Parse(stringArray[0])-1] = true;
                holdCoroutine[int.Parse(stringArray[0])-1]=StartCoroutine(HoldCoroutine(int.Parse(stringArray[0])-1));
            }
        }
        
        if (stringArray[1]=="1")
        {
            
            if (coroutineIsRunning[int.Parse(stringArray[0])-1])
            {
                StopCoroutine(holdCoroutine[int.Parse(stringArray[0])-1]);
                coroutineIsRunning[int.Parse(stringArray[0]) - 1] = false;
            }

            if (coroutineIsCompleted[int.Parse(stringArray[0])-1])
            {
                coroutineIsCompleted[int.Parse(stringArray[0]) - 1] = false;
                return;
            }
            
            
            //Handle Button Up
            if (stringArray[0]=="1")
            {
                // Right Movement
                playerMovement.Move(Vector2.right);
            }
            else if(stringArray[0]=="2")
            {
                // Left Movement
                playerMovement.Move(Vector2.left);
            }
            else if(stringArray[0]=="3")
            {
                // Up Movement
                playerMovement.Move(Vector2.up);
            }
            else if(stringArray[0]=="4")
            {
                // Down Movement
                playerMovement.Move(Vector2.down);
            }
        }
    }

    private IEnumerator HoldCoroutine(int coroutineIndex)
    {
        coroutineIsCompleted[coroutineIndex] = false;
        yield return new WaitForSeconds(holdThresholdAsSecond);
        // TODO:Activate ability
        Debug.Log("Activate Ability");
        coroutineIsRunning[coroutineIndex] = false;
        coroutineIsCompleted[coroutineIndex] = true;
    }

    public void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
