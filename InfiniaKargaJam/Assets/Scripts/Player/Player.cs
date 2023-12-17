using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;


public class Player:MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;
    private Tile currentTile;
    private bool isMoving;
    private static readonly int isMovingParameter = Animator.StringToHash("isMoving");

    public int coinCount;

    [SerializeField] private float stunDuration = 2f;

    [SerializeField] private Ability[] abilities = new Ability[4];

    [SerializeField] private GameObject boxToSpawn;

    [SerializeField] private GameObject trapToSpawn;
    public void Move(Vector2 vector)
    {
        if (isMoving)
        {
            return;
        }
        Tile tile=currentTile.FindNextTile(vector);
        if (tile==null || tile.spawnable is Obstacle)
        {
            return;
        }
        
        SetTile(tile);
    }
    public void SetTile(Tile tile)
    {
        if (currentTile != null)
        {
            if (Math.Abs(currentTile.transform.position.y - tile.transform.position.y) < 0.1f)
            {
                transform.localScale =
                    new Vector3(currentTile.transform.position.x > tile.transform.position.x ? -1 : 1,1, 1);
            }
            currentTile.currentPlayer = null;
        }
        
        currentTile = tile;
        currentTile.currentPlayer = this;
        GoToTile(currentTile);
    }
    private void GoToTile(Tile tile)
    {
        
        isMoving = true;
        animator.SetBool(isMovingParameter,true);
        transform.DOMove(tile.transform.position, playerSpeed).SetSpeedBased().OnComplete(()=>
        {
            animator.SetBool(isMovingParameter,false);
            isMoving = false;
            if (currentTile.spawnable is Interactable)
            {
                ((Interactable)currentTile.spawnable).Interact(this);
            }
        });
    }
    
    public void MakeReverse()
    {
        transform.localScale = new Vector3(-1, 1 ,1);
    }

    public void LootCoin(int amount)
    {
        coinCount += amount;
    }

    public void TakeDamage()
    {
        DropCoins();
        CameraShakeManager.Instance.StartShake();
        StartCoroutine(DamageTakenCoroutine());
    }

    private void DropCoins()
    {
        List<Tile> availableTiles = new List<Tile>();
        for (int i = 0; i < currentTile.cardinalAdjacentTiles.Count; i++)
        {
            if (currentTile.cardinalAdjacentTiles[i].spawnable == null)
            {
                availableTiles.Add(currentTile.cardinalAdjacentTiles[i]);
            }
        }
        for (int i = 0; i < currentTile.diagonalAdjacentTiles.Count; i++)
        {
            if (currentTile.diagonalAdjacentTiles[i].spawnable == null)
            {
                availableTiles.Add(currentTile.diagonalAdjacentTiles[i]);
            }
        }
        
        var adjacentTileCount = availableTiles.Count;
        var coinsToDrop = coinCount / 2;
        var baseAmountToDrop = coinsToDrop / adjacentTileCount;
        var remainderCoins = coinsToDrop % adjacentTileCount;
        List<int> coinsOnTiles = new List<int>();
        for (int i = 0; i < adjacentTileCount; i++)
        {
            coinsOnTiles.Add(baseAmountToDrop);
        }
        for (int i = 0; i < remainderCoins; i++)
        {
            coinsOnTiles[i]++;
        }

        for (int i = 0; i < availableTiles.Count; i++)
        {
            SpawnableManager.Instance.DropCoin(availableTiles[i], coinsOnTiles[i]);
        }
    }
    
    private IEnumerator DamageTakenCoroutine()
    {
        playerInput.isStunned = true;

        yield return new WaitForSeconds(stunDuration);

        playerInput.isStunned = false;
    }
    public Tile GetCurrentTile()
    {
        return currentTile;
    }

    public void ActivateAbility(int commandIndex)
    {
        if (abilities[commandIndex]!=null)
        {
            abilities[commandIndex].Use(this);
        }
    }

    public void AbilityReleased(int commandIndex)
    {
        if (abilities[commandIndex]!=null)
        {
            abilities[commandIndex].Released(this);
        }
    }

    public GameObject GetBox()
    {
        return boxToSpawn;
    }

    public GameObject GetTrap()
    {
        return trapToSpawn;
    }
}
