using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDrop : Loot
{
    [SerializeField] private GameObject prefabForAbility;
    
    public override void Collect(Player player)
    {
        player.TakeAbility(prefabForAbility);
        SoundManager.Instance.sfxAudioSource.PlayOneShot(SoundManager.Instance.lootClip);
        DestroyObject();
    }
}
