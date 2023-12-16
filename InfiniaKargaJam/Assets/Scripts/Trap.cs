using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Trap : Spawnable, Interactable
{
    public enum WhoToDamage {Player1, Player2, All}
    public WhoToDamage who;

    public SpriteRenderer renderer;

    private void DealDamage(Player player)
    {
        //TODO damage and stun player
        DestroyObject();
    }

    public void Interact(Player player)
    {
        DealDamage(player);
    }
}
