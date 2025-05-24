using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gamePauseText;
    public Button restartButton;
    public GameObject titleScreenObject;
    public bool isGameActive;
    private int score;
    private int lives;
    private float times;
    private float spawnRate = 1.5f;
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameActive = false;
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        times = 30.5f;

        spawnRate /= difficulty;

        UpdateScore(0);
        StartCoroutine(SpawnTarget());

        titleScreenObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGameActive)
        {
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;
                gamePauseText.gameObject.SetActive(true);
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
                gamePauseText.gameObject.SetActive(false);
            }
        }
        if (isGameActive)
        {
            times -= Time.deltaTime;
            if (times < 0.5f)
                GameOver();
            timeText.text = "Times: " + Mathf.Floor(times);
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLive()
    {
        if (isGameActive)
        {
            lives--;
            livesText.text = "Lives: " + lives;
            if (lives == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
