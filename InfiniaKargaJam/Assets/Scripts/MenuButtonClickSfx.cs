using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonClickSfx : MonoBehaviour
{
    public void PlayButtonSound()
    {
        SoundManager.Instance.sfxAudioSource.PlayOneShot(SoundManager.Instance.menuButtonClip);
    }
}
