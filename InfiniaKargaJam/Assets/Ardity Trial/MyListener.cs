using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    private void OnMessageArrived(string msg)
    {
        //Debug.Log(msg);
        if (msg.Contains(" "))
        {
            string[] stringArray=msg.Split(" ");
            Debug.Log(stringArray[0]+stringArray[1]);
        }
    }

    public void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
