using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Spawnable
{
    private void Start()
    {
        SoundManager.Instance.sfxAudioSource.PlayOneShot(SoundManager.Instance.obstacleSpawnClip);
    }
}