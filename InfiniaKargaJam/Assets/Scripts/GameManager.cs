using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player player1;
    public Player player2;

    public TMP_Text timeLeftText;
    public TMP_Text p1CoinCountText;
    public TMP_Text p2CoinCountText;

    [SerializeField] private float gameLenght;
    [SerializeField] private float gameLenghtLeft;
    
    [SerializeField] private Image score1Renderer;
    [SerializeField] private Image score2Renderer;
    [SerializeField] private Image score3Renderer;

    [SerializeField] private Sprite p1ScoreSprite;
    [SerializeField] private Sprite p2ScoreSprite;
    [SerializeField] private Sprite drawScoreSprite;

    [SerializeField] private GameObject p1RoundWinObj;
    [SerializeField] private GameObject p1GameWinObj;
    
    [SerializeField] private GameObject p2RoundWinObj;
    [SerializeField] private GameObject p2GameWinObj;
    
    [SerializeField] private GameObject drawRoundObj;
    [SerializeField] private GameObject drawGameObj;

    private bool roundEnded = false;

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

        gameLenghtLeft = gameLenght;
        roundEnded = false;
    }

    private void Update()
    {
        if(!roundEnded) 
            gameLenghtLeft -= Time.deltaTime;

        if (gameLenghtLeft <= 0 && !roundEnded)
        {
            roundEnded = true;
            EndRound();
        }

        var timeLeft = Mathf.CeilToInt(gameLenghtLeft);
        timeLeftText.text = timeLeft.ToString();

        p1CoinCountText.text = player1.coinCount.ToString();
        p2CoinCountText.text = player2.coinCount.ToString();
    }

    private void EndRound()
    {
        var player1CoinCount = player1.coinCount;
        var player2CoinCount = player2.coinCount;

        if (player1CoinCount > player2CoinCount) ScoreKeeper.Instance.player1Score++;
        if (player2CoinCount > player1CoinCount) ScoreKeeper.Instance.player2Score++;
        else
        {
            ScoreKeeper.Instance.player1Score++;
            ScoreKeeper.Instance.player2Score++;
        }

        FillScoreBoard();
        
        if (ScoreKeeper.Instance.player1Score >= 2 || ScoreKeeper.Instance.player2Score >= 2)
        {
            EndGame();
            return;
        }

        if (player1CoinCount > player2CoinCount)
            p1RoundWinObj.SetActive(true);
        if (player2CoinCount > player1CoinCount)
            p2RoundWinObj.SetActive(true);
        else
            drawRoundObj.SetActive(true);
    }

    private void EndGame()
    {
        if (ScoreKeeper.Instance.player1Score == ScoreKeeper.Instance.player2Score)
        {
            drawGameObj.SetActive(true);
        }
        else
        {
            if (ScoreKeeper.Instance.player1Score > ScoreKeeper.Instance.player2Score)
            {
                p1GameWinObj.SetActive(true);
            }
            else
            {
                p2GameWinObj.SetActive(true);
            }
        }
    }

    private void FillScoreBoard()
    {
        var scoreSum = ScoreKeeper.Instance.player1Score + ScoreKeeper.Instance.player2Score;
        if (scoreSum == 4)
        {
            score1Renderer.sprite = p1ScoreSprite;
            score2Renderer.sprite = drawScoreSprite;
            score3Renderer.sprite = p2ScoreSprite;
        }
        else if(scoreSum == 3)
        {
            if (ScoreKeeper.Instance.player1Score == 2)
            {
                score1Renderer.sprite = p1ScoreSprite;
                score2Renderer.sprite = p1ScoreSprite;
                score3Renderer.sprite = p2ScoreSprite;
            }
            else
            {
                score1Renderer.sprite = p1ScoreSprite;
                score2Renderer.sprite = p2ScoreSprite;
                score3Renderer.sprite = p2ScoreSprite;
            }
        }
        else if (scoreSum == 2)
        {
            if (ScoreKeeper.Instance.player1Score == 2)
            {
                score1Renderer.sprite = p1ScoreSprite;
                score2Renderer.sprite = p1ScoreSprite;
            }
            else if (ScoreKeeper.Instance.player2Score == 2)
            {
                score2Renderer.sprite = p2ScoreSprite;
                score3Renderer.sprite = p2ScoreSprite;
            }
            else
            {
                score1Renderer.sprite = p1ScoreSprite;
                score3Renderer.sprite = p2ScoreSprite;
            }
        }
        else if (scoreSum == 1)
        {
            if (ScoreKeeper.Instance.player1Score == 1)
            {
                score1Renderer.sprite = p1ScoreSprite;
            }
            else
            {
                score3Renderer.sprite = p2ScoreSprite;
            }
        }
    }
}
