using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    
    public int coordinateX, coordinateY;
    
    public List<Tile> diagonalAdjacentTiles;
    public List<Tile> cardinalAdjacentTiles;

    public Spawnable spawnable;
    
    [SerializeField] private Color dangerColor=Color.red;
    public Player currentPlayer;
    public void Init(bool isOffset)
    {
        //_renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    public Tile FindNextTile(Vector2 vector)
    {
        int newX=coordinateX + (int)vector.y;
        int newY = coordinateY + (int)vector.x;
        foreach (var tile in cardinalAdjacentTiles)
        {
            if (tile.coordinateX==newX && tile.coordinateY==newY)
            {
                return tile;
            }
        }

        return null;
    }

    public List<Tile> MakeDangerousInVector(Vector3 arrowDirection,List<Tile> dangerousTiles=null)
    {
        if (dangerousTiles==null)
        {
            dangerousTiles = new List<Tile>();
        }

        if (spawnable is Obstacle)
        {
            return dangerousTiles;
        }
        
        foreach (var tile in cardinalAdjacentTiles)
        {
            if (tile.spawnable is Obstacle)
            {
                continue;
            }
            if ((transform.position-tile.transform.position)==arrowDirection)
            {
                dangerousTiles.Add(tile);
                dangerousTiles=tile.MakeDangerousInVector(arrowDirection,dangerousTiles);
            }
        }

        _renderer.color = dangerColor;
        return dangerousTiles;
    }

    public void MakeUnDangerousInVector(Vector3 arrowDirection)
    {
        foreach (var tile in cardinalAdjacentTiles)
        {
            if ((transform.position-tile.transform.position)==arrowDirection)
            {
                tile.MakeUnDangerousInVector(arrowDirection);
            }
        }

        _renderer.color = Color.white;
    }

    public void MakeUnDangerous()
    {
        _renderer.color = Color.white;
    }
}
