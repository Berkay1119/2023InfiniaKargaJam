using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using DefaultNamespace;
using UnityEngine;

public class Trap : Spawnable, Interactable
{
    public Player owner;

    public SpriteRenderer renderer;

    
    private void DealDamage(Player player)
    {
        if (owner != player)
        {
            player.TakeDamage();
            DestroyObject();
        }
    }

    public void Interact(Player player)
    {
        DealDamage(player);
    }
}
