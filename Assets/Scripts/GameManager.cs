using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    public Text userInputField;
    string playerName;


    public void Awake()
    {
        if ( Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    public string GetPlayerName() {
        return playerName = userInputField.text;
    }

}
