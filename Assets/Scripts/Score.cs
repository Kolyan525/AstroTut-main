using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    //public Text killed;
    public Text highScore;

    void Start() // Awake
    {
        score.text = "SCORE: ";
        highScore.text = $"HIGHEST: {PlayerPrefs.GetInt("HighScore", 0)}";
        //killed.text = $"KILLED 0 ALIENS";
        //highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    void Update()
    {
        score.text = $"SCORE: {GameMasteR.Score}";
        //killed.text = $"KILLED {GameMasteR.Killed} ALIENS";

        if (GameMasteR.Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", GameMasteR.HighScore);
            highScore.text = $"HIGHEST: {GameMasteR.Score}";
        }
    }
}
