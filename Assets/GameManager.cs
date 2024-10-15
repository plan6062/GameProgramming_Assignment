using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int defeatedEnemies = 0;
    public float gameTime = 20f;
    public bool isGameOver = false;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemyCountText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(GameTimer());
        UpdateUI();
    }

    public void EnemyDefeated()
    {
        defeatedEnemies++;
        UpdateUI();
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (defeatedEnemies >= 5)
        {
            GameOver(true);
        }
    }

    private void GameOver(bool isVictory)
    {
        isGameOver = true;
        StartCoroutine(LoadGameOverScene(isVictory));
    }

    private IEnumerator LoadGameOverScene(bool isVictory)
    {
        yield return new WaitForSeconds(1f); // 잠시 대기 후 씬 전환
        SceneManager.LoadScene(isVictory ? "WinScreen" : "LoseScreen");
    }

    private IEnumerator GameTimer()
    {
        while (gameTime > 0 && !isGameOver)
        {
            yield return new WaitForSeconds(1f);
            gameTime--;
            UpdateUI();

            if (gameTime <= 0)
            {
                GameOver(false);
            }
        }
    }

    private void UpdateUI()
    {
        if (timerText != null)
            timerText.text = "Time: " + Mathf.Round(gameTime).ToString();
        if (enemyCountText != null)
            enemyCountText.text = "Enemies Defeated: " + defeatedEnemies.ToString() + "/5";
    }
}