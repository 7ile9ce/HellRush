using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishTrigger : MonoBehaviour
{
    public Text timeText;
    public Text scoreText;
    public Text HighscoreText;
    public Button exitButton;
    public Text finalText;
    public Image fadeImage; 
    public float fadeDuration = 1.5f; 
    private bool levelCompleted = false;

    private void Start()
    {
        
        if (timeText != null) timeText.gameObject.SetActive(false);
        if (scoreText != null) scoreText.gameObject.SetActive(false);
        if (finalText != null) finalText.gameObject.SetActive(false);
        if (exitButton != null) exitButton.gameObject.SetActive(false);
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;
            StartCoroutine(ShowLevelCompletion());
        }
    }

    private IEnumerator ShowLevelCompletion()
    {
       
        float timeSpent = Time.timeSinceLevelLoad;

        
        int score = ScoreCount.instance.scoreCounter;
        int highscore = ScoreCount.instance.highScoreCounter;

        
        if (timeText != null)
        {
            timeText.text = "Time: " + timeSpent.ToString("F2") + " seconds";
        }
        
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (HighscoreText != null)
        {
            HighscoreText.text = "HighScore: " + highscore;
        }

        
        Herо.instance.DisableControl();

        
        if (fadeImage != null)
        {
            float elapsedTime = 0f;
            Color fadeColor = fadeImage.color;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                fadeColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeImage.color = fadeColor;
                yield return null;
            }
        }

        
        if (timeText != null) timeText.gameObject.SetActive(true);
        if (scoreText != null) scoreText.gameObject.SetActive(true);
        if (finalText != null) finalText.gameObject.SetActive(true);
        if (HighscoreText != null) HighscoreText.gameObject.SetActive(true);
        if (exitButton != null) exitButton.gameObject.SetActive(true);
    }
    private void ExitGame()
    {
        
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

    }
}
