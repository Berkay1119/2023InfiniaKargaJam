
using UnityEngine;

public abstract class Ability:MonoBehaviour
{
    public abstract void Use(Player player);

    public virtual void Released(Player player)
    {
        
    }
}
