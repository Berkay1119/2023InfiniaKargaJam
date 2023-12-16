using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDrop : Loot
{
    public enum AbilityType {Laser, Trap, Explosion, Wall, Haste}
    public AbilityType abilityType;
    
    public override void Collect(Player player)
    {
        //TODO add ability to random slot
        DestroyObject();
    }
}
