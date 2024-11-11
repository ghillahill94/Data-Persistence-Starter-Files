using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text BestScore;
    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private string m_PlayerNameHighScore;
    private int m_BestHighScore;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        m_PlayerNameHighScore = PlayerPrefs.GetString("PlayerName");
        m_BestHighScore = PlayerPrefs.GetInt("HighScore");

        BestScore.text = $"Best Score : {m_PlayerNameHighScore}: {m_BestHighScore}";


        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (m_Points > m_BestHighScore) {
            PlayerPrefs.SetInt("HighScore", m_Points);
            SetBestScore(m_Points);
        }
        SetBestScore(m_Points);
    }

    public void SetBestScore(int score) {

        string playerName = GameManager.Instance.GetPlayerName();
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("HighScore", score);

        BestScore.text = $"Best Score : {playerName}: {score}";
    }

}
