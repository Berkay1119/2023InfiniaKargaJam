
using System.Collections;
using UnityEngine;

public class HasteAbility:Ability
{
    [SerializeField] private int hasteDuration;
    [SerializeField] private float fasterSpeed;
    public override void Use(Player player)
    {
        float originalSpeed=player.GetSpeed();
        StartCoroutine(EndOfHasteCoRoutine(player,originalSpeed));
    }

    private IEnumerator EndOfHasteCoRoutine(Player player,float originalSpeed)
    {
        player.MakeFaster(fasterSpeed);
        sprite = null;
        yield return new WaitForSeconds(hasteDuration);
        player.MakeFaster(originalSpeed);
        Destroy(gameObject);
    }
}
