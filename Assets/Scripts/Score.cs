using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score = 0;

    [SerializeField]
    private TMP_Text scoreText;
    private void Start()
    {
        EventManager.OnAddScore += UpdateScore;
    }
    private void UpdateScore(int _score)
    {
        score += _score;
        scoreText.text = score.ToString();
    }
}
