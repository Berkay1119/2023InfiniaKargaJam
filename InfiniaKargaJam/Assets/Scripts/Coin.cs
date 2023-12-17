using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Coin : Loot
{
    public int coinAmount;

    private void Start()
    {
        SoundManager.Instance.sfxAudioSource.PlayOneShot(SoundManager.Instance.coinSpawnClip);
    }

    public override void Collect(Player player)
    {
        SoundManager.Instance.sfxAudioSource.PlayOneShot(SoundManager.Instance.coinLootClip);
        player.LootCoin(coinAmount);
        DestroyObject();
    }
}
