using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _height, _width;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cameraTrnasform;
    [SerializeField] public Tile[,] map;
    [SerializeField] private GameObject playerOnePrefab;
    [SerializeField] private GameObject playerTwoPrefab;
    [SerializeField] private float sizeMultiplier;

    private void Start()
    {
        GenerateGrid();

        //_cameraTrnasform.position = new Vector3(_width / 2, _height / 2, -5);
    }
    
    private void GenerateGrid()
    {
        map = new Tile[_height, _width];
        for (int x = 0; x < _height; x++)
        {
            for (int y = 0; y < _width; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(y * sizeMultiplier, x * sizeMultiplier), Quaternion.identity);
                map[x, y] = spawnedTile.GetComponent<Tile>();
                spawnedTile.name = $"Tile {x}, {y} ";

                spawnedTile.coordinateX = x;
                spawnedTile.coordinateY = y;

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }

        for (int x = 0; x < _height; x++)
        {
            for (int y = 0; y < _width; y++)
            {
                Debug.Log(x + " " + y);
                SetDiagonalTiles(map[x,y]);
                SetCardinalTiles(map[x,y]);
            }
        }

        Player player=Instantiate(playerOnePrefab).GetComponent<Player>();
        player.transform.position = map[2, 0].transform.position;
        player.SetTile(map[2,0]);
        
        player=Instantiate(playerTwoPrefab).GetComponent<Player>();
        player.transform.position = map[2, 8].transform.position;
        player.SetTile(map[2,8]);
        player.MakeReverse();
    }

    private void SetCardinalTiles(Tile tile)
    {
        if (tile.coordinateX - 1 >= 0)
            tile.cardinalAdjacentTiles.Add(map[tile.coordinateX - 1, tile.coordinateY]);
        
        if (tile.coordinateX + 1 <  map.GetLength(0))
            tile.cardinalAdjacentTiles.Add(map[tile.coordinateX + 1, tile.coordinateY]);
        
        if (tile.coordinateY - 1 >= 0)
            tile.cardinalAdjacentTiles.Add(map[tile.coordinateX, tile.coordinateY - 1]);
        
        if (tile.coordinateY + 1 <  map.GetLength(1))
            tile.cardinalAdjacentTiles.Add(map[tile.coordinateX, tile.coordinateY + 1]);
    }

    private void SetDiagonalTiles(Tile tile)
    {
        if (tile.coordinateX - 1 >= 0 && tile.coordinateY - 1 >= 0)
            tile.diagonalAdjacentTiles.Add(map[tile.coordinateX - 1, tile.coordinateY - 1]);

        if (tile.coordinateX - 1 >= 0 && tile.coordinateY + 1 < map.GetLength(1))
            tile.diagonalAdjacentTiles.Add(map[tile.coordinateX - 1, tile.coordinateY + 1]);

        if (tile.coordinateX + 1 < map.GetLength(0) && tile.coordinateY - 1 >= 0)
            tile.diagonalAdjacentTiles.Add(map[tile.coordinateX + 1, tile.coordinateY - 1]);

        if (tile.coordinateX + 1 < map.GetLength(0) && tile.coordinateY + 1 < map.GetLength(1))
            tile.diagonalAdjacentTiles.Add(map[tile.coordinateX + 1, tile.coordinateY + 1]);
    }

    
}
