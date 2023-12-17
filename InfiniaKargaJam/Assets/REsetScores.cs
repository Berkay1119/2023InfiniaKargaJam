using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REsetScores : MonoBehaviour
{
    void Start()
    {
        ScoreKeeper.Instance.player1Score = 0;
        ScoreKeeper.Instance.player2Score = 0;
    }
}