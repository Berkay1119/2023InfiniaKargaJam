using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public abstract class Loot : Spawnable, Interactable
{
    public abstract void Collect(Player player);
    public void Interact(Player player)
    {
        Collect(player);
    }
}