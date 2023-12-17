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
}
