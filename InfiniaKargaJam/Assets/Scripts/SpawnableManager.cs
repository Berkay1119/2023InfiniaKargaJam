using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnableManager : MonoBehaviour
{
    public static SpawnableManager Instance { get; private set; }

    
    [SerializeField] private GridManager _gridManager;
    private List<Tile> emptyTiles = new List<Tile>();
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject obstaclesPrefab;
    [SerializeField] private List<GameObject> rewardHazardPrefabs ;

    [Header("Coin")] 
    [SerializeField] private float firstCoinDelayAsSecond;
    [SerializeField] private float minSpawnSecondForCoin=5f;
    [SerializeField] private float maxSpawnSecondForCoin=10f;
    [SerializeField] private int totalCoinCount=20;

    [Header("Other Spawnable")] 
    [SerializeField] private float firstOtherDelayAsSecond;
    [SerializeField] private float minSpawnSecondForOther=3f;
    [SerializeField] private float maxSpawnSecondForOther=15f;

    [Header("AbilityDrop")] 
    [SerializeField] private float firstDropDelaySecond;
    [SerializeField] private float minSpawnSecondForAbilityDrop=3f;
    [SerializeField] private float maxSpawnSecondForAbilityDrop=10f;
    [SerializeField] private List<GameObject> dropPrefabs;
    
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
    
    private void Start()
    {
        Invoke("SpawnCoin", firstCoinDelayAsSecond);
        Invoke("SpawnRewardOrHazard", firstOtherDelayAsSecond);
        Invoke("SpawnAbility",firstDropDelaySecond);
    }

    private void GetEmptyTiles()
    {
        emptyTiles.Clear();
        
        for (int x = 0; x < _gridManager.map.GetLength(0); x++)
        {
            for (int y = 0; y < _gridManager.map.GetLength(1); y++)
            {
                if (_gridManager.map[x, y].spawnable == null && _gridManager.map[x,y].currentPlayer==null)
                {
                    emptyTiles.Add(_gridManager.map[x, y]);
                }
            }
        }
    }

    public Spawnable Spawn(GameObject spawnable, Tile tile)
    {
        var spawnedObj = Instantiate(spawnable, tile.transform.position, Quaternion.identity);
        Spawnable spawnableScript =spawnedObj.GetComponent<Spawnable>();
        tile.spawnable = spawnableScript;
        spawnableScript.spawnedTile = tile;
        // if (spawnedObj.CompareTag("Trap"))
        // {
        //     ((Trap)spawnableScript).owner = Trap.WhoToDamage.All;
        // }
        spawnableScript.Begin();
        return spawnableScript;
    }
    
    public void SpawnInfiniteObstacles(Tile tile)
    {
        var spawnedObj = Instantiate(obstaclesPrefab, tile.transform.position, Quaternion.identity);
        Spawnable spawnableScript =spawnedObj.GetComponent<Spawnable>();
        spawnableScript.MakeTime(Single.MaxValue);
        tile.spawnable = spawnableScript;
        spawnableScript.spawnedTile = tile;
        spawnableScript.Begin();
    }
    
    public void SpawnCoin()
    {
        StartCoroutine(SpawnCoinRoutine());
    }

    public void DropCoin(Tile tile, int amount)
    {
        if (amount==0)
        {
            return;
        }
        var spawnedObj = Instantiate(coinPrefab, tile.transform.position, Quaternion.identity);
        Spawnable spawnableScript = spawnedObj.GetComponent<Spawnable>();
        tile.spawnable = spawnableScript;
        spawnableScript.spawnedTile = tile;
        ((Coin)spawnableScript).coinAmount = amount;

    }

    private IEnumerator SpawnCoinRoutine()
    {
        while (true)
        {
            for (int i = 0; i < totalCoinCount; i++)
            {
                GetEmptyTiles();
                if (emptyTiles.Count > 0)
                {
                    Tile randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
                    Spawn(coinPrefab, randomTile);
                }

                yield return new WaitForSeconds(Random.Range(minSpawnSecondForCoin, maxSpawnSecondForCoin));
            }
        }
    }

    public void SpawnRewardOrHazard()
    {
        StartCoroutine(SpawnRewardOrHazardRoutine());
    }

    private IEnumerator SpawnRewardOrHazardRoutine()
    {
        while (true)
        {
            GetEmptyTiles();
            if (emptyTiles.Count > 0)
            {
                var randomSpawnObj = rewardHazardPrefabs[Random.Range(0, rewardHazardPrefabs.Count)];
                Tile randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
                Spawn(randomSpawnObj, randomTile);
            }

            yield return new WaitForSeconds(Random.Range(minSpawnSecondForOther, maxSpawnSecondForOther));
        }
    }

    private void SpawnAbility()
    {
        StartCoroutine(SpawnAbilityRoutine());
    }

    private IEnumerator SpawnAbilityRoutine()
    {
        while (true)
        {
            GetEmptyTiles();
            if (emptyTiles.Count > 0)
            {
                Tile randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
                GameObject gameObject = dropPrefabs[Random.Range(0, dropPrefabs.Count)];
                Spawn(gameObject, randomTile);
            }

            yield return new WaitForSeconds(Random.Range(minSpawnSecondForAbilityDrop, maxSpawnSecondForAbilityDrop));
        }
    }
}
