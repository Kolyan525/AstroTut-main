using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNickname : MonoBehaviour
{
    public InputField Nickname;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s)
    {
        Nickname.text = s;
        Debug.Log(Nickname.text);
        PlayerPrefs.SetString("Nickname", Nickname.text);
        Nickname.text = "GOT IT!";
    }
}
