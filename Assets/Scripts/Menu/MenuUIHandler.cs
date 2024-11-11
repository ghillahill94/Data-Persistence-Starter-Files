using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame() {
        // Load the scene with the index 1
        if (GameManager.Instance != null) {
            string playerName = GameManager.Instance.GetPlayerName();

            if (!string.IsNullOrEmpty(playerName)) {
                SceneManager.LoadScene(1);
            }
            else {
                Debug.Log("Please enter a name!");
            }

        }
    }
}
