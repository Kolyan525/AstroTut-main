using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    public Text killed;
    public Text highScore;

    void Start() // Awake
    {
        score.text = "SCORE: ";
        highScore.text = $"HIGHEST: {PlayerPrefs.GetInt("HighScore", 0)}";
        killed.text = $"F*CKED UP 0 ALIEN MOTHERFUCKERS";
        //highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    void Update()
    {
        score.text = $"SCORE: {GameMasteR.Score}";
        killed.text = $"F*CKED UP {GameMasteR.Killed} ALIEN MOTHERFUCKERS";

        if (GameMasteR.Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", GameMasteR.HighScore);
            highScore.text = $"HIGHEST: {GameMasteR.Score}";
        }
    }
}
