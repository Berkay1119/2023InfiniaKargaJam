
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box:Ability
{
    [SerializeField] private float waitSecondForNextDirection=1f;
    private Tile tileToPlace;
    private Coroutine holdCoroutine;
    public override void Use(Player player)
    {
        holdCoroutine=StartCoroutine(TraverseTiles(player));
    }

    private IEnumerator TraverseTiles(Player player)
    {
        while (true)
        {
            foreach (var tile in player.GetCurrentTile().cardinalAdjacentTiles)
            {
                if (tile.spawnable!=null)
                {
                    continue;
                }
                tileToPlace = tile;
                tile.Glow(true);
                yield return new WaitForSeconds(waitSecondForNextDirection);
                tile.Glow(false);
            }
        }
    }

    public override void Released(Player player)
    {
        base.Released(player);
        if (holdCoroutine!=null)
        {
            StopCoroutine(holdCoroutine);
        }
        
        foreach (var tile in player.GetCurrentTile().cardinalAdjacentTiles)
        {
            tile.Glow(false);
        }
        SpawnableManager.Instance.Spawn(player.GetBox(),tileToPlace);
        Destroy(gameObject);
    }
}

