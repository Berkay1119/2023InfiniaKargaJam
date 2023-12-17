using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Coin : Loot
{
    public int coinAmount;
    
    public override void Collect(Player player)
    {
        player.LootCoin(coinAmount);
        DestroyObject();
    }
}
