using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LootTweenAnimation : MonoBehaviour
{
    public float moveDuration = 1f;
    public float moveDistance = 1f;
    public float smallScale = 0.5f;
    public Ease ease;
    public GameObject shadow;
    
    void Start()
    {
        AnimateLootItem();
    }
    
    void AnimateLootItem()
    {
        transform.DOMoveY(transform.position.y + moveDistance, moveDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease);
        shadow.transform.DOScale(smallScale,moveDuration).SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease);
    }
}
