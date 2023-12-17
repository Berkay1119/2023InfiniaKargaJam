using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isKeyboardPlayer;
    
    [SerializeField] private Player player;
    [SerializeField] private float holdThresholdAsSecond=2f;
    private Coroutine[] holdCoroutine = new Coroutine[4];
    private bool[] coroutineIsRunning = new bool[4];
    private bool[] coroutineIsCompleted = new bool[4];
    public bool isStunned;

    
    private void OnMessageArrived(string msg)
    {
        if(isStunned) return;
        
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
                player.AbilityReleased(int.Parse(stringArray[0]) - 1);
                return;
            }
            
            
            //Handle Button Up
            if (stringArray[0]=="1")
            {
                // Right Movement
                player.Move(Vector2.right);
            }
            else if(stringArray[0]=="2")
            {
                // Left Movement
                player.Move(Vector2.left);
            }
            else if(stringArray[0]=="3")
            {
                // Up Movement
                player.Move(Vector2.up);
            }
            else if(stringArray[0]=="4")
            {
                // Down Movement
                player.Move(Vector2.down);
            }
        }
    }


    private void Update()
    {
        if (!isKeyboardPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int playerNumber = 1;
            OnMessageArrived(playerNumber.ToString()+" "+0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            int playerNumber = 2;
            OnMessageArrived(playerNumber.ToString()+" "+0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            int playerNumber = 3;
            OnMessageArrived(playerNumber.ToString()+" "+0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            int playerNumber = 4;
            OnMessageArrived(playerNumber.ToString()+" "+0);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            int playerNumber = 1;
            OnMessageArrived(playerNumber.ToString()+" "+1);
        }
        else if(Input.GetKeyUp(KeyCode.Alpha4))
        {
            int playerNumber = 2;
            OnMessageArrived(playerNumber.ToString()+" "+1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            int playerNumber = 3;
            OnMessageArrived(playerNumber.ToString()+" "+1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            int playerNumber = 4;
            OnMessageArrived(playerNumber.ToString()+" "+1);
        }
        
    }

    private IEnumerator HoldCoroutine(int coroutineIndex)
    {
        coroutineIsCompleted[coroutineIndex] = false;
        yield return new WaitForSeconds(holdThresholdAsSecond);
        player.ActivateAbility(coroutineIndex);
        Debug.Log("Activate Ability");
        coroutineIsRunning[coroutineIndex] = false;
        coroutineIsCompleted[coroutineIndex] = true;
    }

    public void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
