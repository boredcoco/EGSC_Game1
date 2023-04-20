using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private string scoreString = "Score: ";
    private int currentScore = 0;

    private void Start()
    {
        scoreText.text = scoreString + currentScore;
    }

    public void UpdateScore()
    {
        currentScore += 1;
        scoreText.text = scoreString + currentScore;
    }
}
