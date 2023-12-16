using System;
using DG.Tweening;
using UnityEngine;


public class Player:MonoBehaviour
{
    [SerializeField] private float playerSpeed=1f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;
    private Tile currentTile;
    private bool isMoving;
    private static readonly int isMovingParameter = Animator.StringToHash("isMoving");

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
        if (currentTile != null)
        {
            if (Math.Abs(currentTile.transform.position.y - tile.transform.position.y) < 0.1f)
            {
                transform.localScale =
                    new Vector3(currentTile.transform.position.x > tile.transform.position.x ? -1 : 1,1, 1);
            }
            
            
        }
        currentTile = tile;
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
        });
    }


    public void MakeReverse()
    {
        transform.localScale = new Vector3(-1, 1 ,1);
    }
}
