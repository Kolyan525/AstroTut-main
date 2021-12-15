using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHighscore : MonoBehaviour
{
    [SerializeField]
    string pressButtonSound = "ButtonPress";
    // Start is called before the first frame update
    AudioManager audioManager;
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audioManager found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Reset()
    {
        audioManager.PlaySound(pressButtonSound);
        PlayerPrefs.DeleteKey("HighScore");
        //Score.highScore.text = "0";
    }
}
