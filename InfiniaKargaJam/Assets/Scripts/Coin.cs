using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Coin : Loot
{
    public float coinAmount;
    
    public override void Collect(Player player)
    {
        //TODO increase player coin amount
        DestroyObject();
    }
}
