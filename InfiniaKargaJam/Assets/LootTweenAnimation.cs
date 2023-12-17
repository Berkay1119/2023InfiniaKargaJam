using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LootTweenAnimation : MonoBehaviour
{
    public float moveDuration = 1f;
    public float moveDistance = 1f;
    public Ease ease;
    
    void Start()
    {
        AnimateLootItem();
    }
    
    void AnimateLootItem()
    {
        transform.DOMoveY(transform.position.y + moveDistance, moveDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease);
    }
}
