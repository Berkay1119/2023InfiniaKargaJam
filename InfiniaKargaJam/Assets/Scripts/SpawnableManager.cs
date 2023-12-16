using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnableManager : MonoBehaviour
{
    private GridManager _gridManager;
    private List<Tile> emptyTiles;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private List<GameObject> rewardHazardPrefabs ;

    private void Start()
    {
        _gridManager = GameObject.Find("Grid Manager").GetComponent<GridManager>();
        
        Invoke("SpawnCoin", 4f);
        Invoke("SpawnRewardOrHazard", 5f);
    }

    private void GetEmptyTiles()
    {
        emptyTiles.Clear();
        
        for (int x = 0; x < _gridManager.map.GetLength(0); x++)
        {
            for (int y = 0; y < _gridManager.map.GetLength(1); y++)
            {
                if (_gridManager.map[x, y].spawnable == null)
                {
                    emptyTiles.Add(_gridManager.map[x, y]);
                }
            }
        }
    }

    public void Spawn(GameObject spawnable, Tile tile)
    {
        var spawnedObj = Instantiate(spawnable, tile.transform.position, Quaternion.identity);
        tile.spawnable = spawnedObj.GetComponent<Spawnable>();
    }
    
    public void SpawnCoin()
    {
        StartCoroutine(SpawnCoinRoutine());
    }

    private IEnumerator SpawnCoinRoutine()
    {
        while (true)
        {
            for (int i = 0; i < 20; i++)
            {
                GetEmptyTiles();
                if (emptyTiles.Count > 0)
                {
                    Tile randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
                    Spawn(coinPrefab, randomTile);
                }

                yield return new WaitForSeconds(Random.Range(3, 7));
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

            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }
}
