using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Laser:Ability
{
    [SerializeField] private GameObject ball;
    [SerializeField] private float ballSpeed;
    [SerializeField] private float ballRotateSpeed=120f;

    [SerializeField] private float waitSecondForNextDirection=1f;

    private List<Tile> dangerousTiles = new List<Tile>();
    
    private Coroutine holdCoroutine;
    private static readonly int Property = Animator.StringToHash("throw");
    [SerializeField] private float distanceThreshold=1f;

    public override void Use(Player player)
    {
        transform.position = player.GetCurrentTile().transform.position;
        holdCoroutine=StartCoroutine(TraverseTiles(player));
    }

    private IEnumerator TraverseTiles(Player player)
    {
        while (true)
        {
            dangerousTiles.Clear();
            foreach (var tile in player.GetCurrentTile().cardinalAdjacentTiles)
            {
            
                dangerousTiles=tile.MakeDangerousInVector(player.GetCurrentTile().transform.position-tile.transform.position);
                dangerousTiles.Add(tile);
                yield return new WaitForSeconds(waitSecondForNextDirection);
                tile.MakeUnDangerousInVector(player.GetCurrentTile().transform.position-tile.transform.position);
            }
        }
        
    }

    public override void Released(Player player)
    {  
        base.Released(player);
        player.GetAnimator().SetTrigger(Property);
        
        if (holdCoroutine!=null)
        {
            StopCoroutine(holdCoroutine);
        }
        
        foreach (var tile in dangerousTiles)
        {
            if (tile.currentPlayer!=null && tile.currentPlayer!=player)
            {
                //TODO:Take Damage
                Debug.Log("Damage taken");
                ball.transform.parent = null;
                ball.gameObject.SetActive(true);
                ball.transform.DORotate(Vector3.forward*360f, ballRotateSpeed,RotateMode.FastBeyond360).SetSpeedBased();
                ball.transform.DOMove(tile.transform.position, ballSpeed).SetSpeedBased().OnUpdate(() =>
                {
                    if ((ball.transform.position-tile.transform.position).magnitude<distanceThreshold)
                    {
                        tile.currentPlayer.TakeDamage();
                    }
                }).OnComplete(() =>
                {
                    Destroy(ball);
                });
            }

            tile.MakeUnDangerous();
        }
        dangerousTiles.Clear();
        Destroy(gameObject);
    }
}
