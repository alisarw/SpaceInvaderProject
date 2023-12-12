using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public GameObject startUI;
    public GameObject winUI;
    public GameObject retryUI;
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public GameObject enemyParent;
    public GameObject playerShip;
    public AudioSource audioSource;
    public AudioClip _1UpClip;
    public AudioClip deathSound;
    int currentLives;
    int score;
    int scoreToAddLife;
    bool gameStart = false;
    bool win;
    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Lives") <= 0)
        {
            PlayerPrefs.SetInt("Lives", 3);
        }

        currentLives = PlayerPrefs.GetInt("Lives");
        livesText.text = currentLives.ToString();

        if (PlayerPrefs.GetInt("Score") <= 0)
        {
            PlayerPrefs.SetInt("Score", 0);
        }

        score = PlayerPrefs.GetInt("Score");
        scoreText.text = score.ToString();


        if (PlayerPrefs.GetInt("ScoreToAddLife") <= 0)
        {
            PlayerPrefs.SetInt("ScoreToAddLife", 500);
        }
        scoreToAddLife = PlayerPrefs.GetInt("ScoreToAddLife");
    }

    private void Update()
    {
        if (!gameStart)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                startUI.SetActive(false);
                enemyParent.SetActive(true);
                gameStart = true;
            }

        }
        if (gameStart && !gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if(Time.timeScale == 1)
                {
                    pauseUI.SetActive(true);
                    Time.timeScale = 0;             
                }
                else if (Time.timeScale == 0)
                {
                    pauseUI.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
        if (gameOver)
        {
            if (win)
            {
                if (Input.GetKey(KeyCode.Return))
                {
                    if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCount)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        SceneManager.LoadScene(0);
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Return))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }          
        }
    }

    public void UpdateScore(int enemyScore)
    {
        score += enemyScore;
        scoreText.text = score.ToString();
        if(score >= scoreToAddLife)
        {
            audioSource.PlayOneShot(_1UpClip, 0.5f);
            currentLives++;
            PlayerPrefs.SetInt("Lives", currentLives);
            livesText.text = currentLives.ToString();

            scoreToAddLife += scoreToAddLife;
            PlayerPrefs.SetInt("ScoreToAddLife", scoreToAddLife);
        }
    }

    public void GameOver()
    {
        if (!win)
        {
            playerShip.SetActive(false);
            audioSource.PlayOneShot(deathSound);
            currentLives--;
            PlayerPrefs.SetInt("Lives", currentLives);
            livesText.text = currentLives.ToString();
            enemyParent.SetActive(false);
            if (currentLives > 0)
            {
                PlayerPrefs.SetInt("Score", score);
                retryUI.SetActive(true);
                gameOver = true;
            }
            else
            {
                scoreToAddLife = 0;
                PlayerPrefs.SetInt("ScoreToAddLife", scoreToAddLife);
                PlayerPrefs.SetInt("Score", 0);
                gameOverUI.SetActive(true);
                gameOver = true;
            }
        }    
    }

    public void Win()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Lives", currentLives);
        enemyParent.SetActive(false);
        winUI.SetActive(true);
        gameOver = true;
        win = true;
    }
}
