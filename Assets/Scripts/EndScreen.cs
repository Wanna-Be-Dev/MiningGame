using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    Score score;
    [SerializeField]
    private TMP_Text HighscoreText;
    [SerializeField]
    private TMP_Text scoreText;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }
    public void OpenMenu()
    {
        CheckScore();
        HighscoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        scoreText.text = score.GetScore().ToString();
    }
    void CheckScore()
    {
        if (score.GetScore() > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score.GetScore());
        }
    }
    public void Restart()
    {
        CheckScore();
        SceneManager.LoadScene(0);
    }
}
