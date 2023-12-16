using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager Instance { get; private set; }

    private Transform cameraTransform;
    private Vector3 originalPosition;
    private bool isShaking = false;

    // Adjust these variables to control the screen shake intensity and duration
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.2f;

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

    void Start()
    {
        cameraTransform = Camera.main.transform;
        originalPosition = cameraTransform.position;
    }

    public void StartShake()
    {
        if (!isShaking)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeIntensity;
            float y = originalPosition.y + Random.Range(-0.3f, 0.3f) * shakeIntensity;

            cameraTransform.position = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = originalPosition;
        isShaking = false;
    }
}
