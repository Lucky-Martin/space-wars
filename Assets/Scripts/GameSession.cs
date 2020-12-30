using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] Text scoreText;

    int score = 0;

    private void Start()
    {
        ResetScore();
    }

    private void ResetScore()
    {
        scoreText.text = "0";
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }
}
