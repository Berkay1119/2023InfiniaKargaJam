using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{ 
    public static SoundManager Instance { get; private set; }

    public AudioSource sfxAudioSource;
    
    public AudioClip coinLootClip;
    public AudioClip coinSpawnClip;
    public AudioClip gameOverClip;
    public AudioClip hasteClip;
    public AudioClip laserClip;
    public AudioClip lootClip;
    public AudioClip menuButtonClip;
    public AudioClip obstacleSpawnClip;
    public AudioClip takeDamage1Clip;
    public AudioClip takeDamage2Clip;
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }
}
