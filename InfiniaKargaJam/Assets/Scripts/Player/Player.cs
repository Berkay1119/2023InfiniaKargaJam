using DG.Tweening;
using UnityEngine;


public class Player:MonoBehaviour
{
    [SerializeField] private float playerSpeed=1f;
    private Tile currentTile;
    private bool isMoving;

    public void Move(Vector2 vector)
    {
        if (isMoving)
        {
            return;
        }
        Tile tile=currentTile.FindNextTile(vector);
        if (tile==null)
        {
            return;
        }
        
        SetTile(tile);
    }
    public void SetTile(Tile tile)
    {
        currentTile = tile;
        GoToTile(currentTile);
    }
    private void GoToTile(Tile tile)
    {
        isMoving = true;
        transform.DOMove(tile.transform.position, playerSpeed).SetSpeedBased().OnComplete(()=>isMoving=false);
    }
}
