using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int numberOfLIves;

    private bool isDead = false;

    public Image[] lives;

    public Sprite fullLive;
    public Sprite emptyLive;

    public ScriptFade screenFade;

    // Start is called before the first frame update
    void Start()
    {
        if (screenFade == null)
        {
            Debug.LogError("ScreenFade component is not assigned in the inspector.");
        }
        else
        {
            screenFade.FadeIn(); // Убираем затемнение при старте уровня
        }
        SetHealthBasedOnDifficulty();
    }

    public void SetHealthBasedOnDifficulty()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "medium");
        switch (difficulty)
        {
            case "легкий":
                health = 3;
                break;
            case "средний":
                health = 2;
                break;
            case "сложный":
                health = 1;
                break;
            default:
                health = 2;
                break;
        }
        numberOfLIves = health; // Устанавливаем начальное количество жизней
    }

    // Update is called once per frame
    void Update()
    {
        if (health > numberOfLIves)
        {
            health = numberOfLIves;
        }
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < health)
            {
                lives[i].sprite = fullLive;
            }
            else
            {
                lives[i].sprite = emptyLive;
            }
            if (i < numberOfLIves)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Respawn")
        {
            TakeDamage(1);
            if ( health == 0 && !isDead)
            {
                isDead = true;
                StartCoroutine(RestartLevelWithDelay());
            }
        }
        if (other.tag == "DeathZone")
        {
            StartCoroutine(RestartLevelWithDelay());
        }
    }

    private IEnumerator RestartLevelWithDelay()
    {
        float deathAnimLength = 0; // Уточните длину анимации смерти
        yield return new WaitForSeconds(deathAnimLength);
        if (screenFade != null)
        {
             screenFade.FadeOut();
             yield return new WaitForSeconds(screenFade.fadeDuration + 2f);
        }
        else
        {
            Debug.LogError("ScreenFade component is null. Skipping fade out.");
            yield return new WaitForSeconds(2f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
