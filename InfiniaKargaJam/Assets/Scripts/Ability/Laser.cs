using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser:Ability
{
    [SerializeField] private GameObject arrow;

    [SerializeField] private float waitSecondForNextDirection=1f;

    private List<Tile> dangerousTiles = new List<Tile>();
    
    private Coroutine holdCoroutine;
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
            }

            tile.MakeUnDangerous();
        }
        dangerousTiles.Clear();
        Destroy(gameObject);
    }
}
