using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawnable : MonoBehaviour
{
    public Tile spawnedTile;
    [SerializeField] private float lifeTime;
    [SerializeField] private float destroyAnimationStartOffset = 3f;
    private SpriteRenderer _renderer;

    public void Begin()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Invoke("DestroyObject", lifeTime);
        Invoke("FadeInOutAnimation", lifeTime - destroyAnimationStartOffset);
    }

    protected void DestroyObject()
    {
        spawnedTile.spawnable = null;
        Destroy(gameObject);
    }

    private void FadeInOutAnimation()
    {
        //TODO dotween alpha yoyo
    }

    public void MakeTime(float second)
    {
        lifeTime=second;
    }
}
