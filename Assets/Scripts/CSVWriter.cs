using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVWriter : MonoBehaviour
{
    static string filename = "";
    static public TextAsset textAssetData;
    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/astrotut-highscore.csv";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void WriteCSV(PlayerInfo playerInfo)
    {
        //string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        //Debug.Log(textAssetData.text);
        //for (int i = 0; i < data.Length; i++)
        //{
        //    if (!data[i].Contains("Nickname, Score, Money, Enemies Killed"))
        //    {
        //        TextWriter tw = new StreamWriter(filename, false);
        //        tw.WriteLine("Nickname, Score, Money, Enemies Killed");
        //        tw.Close();
        //    }
        //}

        if (playerInfo.Nickname != "" && playerInfo.Killed != 0)
        {
            TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine($"{playerInfo.Nickname}, {playerInfo.Score}, {playerInfo.Money}, {playerInfo.Killed}");
           
            tw.Close();
        }
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public string Nickname;
        public int Money;
        public int Score;
        public int Killed;

        public PlayerInfo(string Nickname, int Money, int Score, int Killed)
        {
            this.Nickname = Nickname;
            this.Money = Money;
            this.Score = Score;
            this.Killed = Killed;
        }

        public override string ToString()
        {
            return $"{Nickname} killed {Killed}, has {Score} score and {Money} money";
        }
    }
}
