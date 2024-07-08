using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{
    public int currenthealth;
    public int numOfHearts;
    public GameObject _GameOver;
    public bool IsPaused;
    private float startingTime = 0f;

    public GameObject PauseMenu;

    public Text timerText;
    public float currentTime = 0f;

    public TextMeshProUGUI HeartsText;
    //public Image[] hearts;
    //public Sprite fullHeart;
    //public Sprite emptyHeart;
    void Start()
    {
        currenthealth = numOfHearts;
        HeartsText.text = "" + currenthealth;

        startingTime = Time.time;

    }
    void Update()
    {
        currentTime = Time.time - startingTime;
        UpdateTimerDisplay();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    // Update is called once per frame
    //void Update()
    //{

    //    if (currenthealth > numOfHearts)
    //    {
    //        currenthealth = numOfHearts;
    //    }

    //    for (int i = 0; i < hearts.Length; i++)
    //    {
    //        if (i < currenthealth)
    //        {
    //            hearts[i].sprite = fullHeart;
    //            Color color = Color.white;
    //            color.a = 255;
    //            hearts[i].color = color;
    //        }
    //        else
    //        {
    //            hearts[i].sprite = fullHeart;
    //            Color color = Color.red;
    //            color.a = 0.5f;
    //            hearts[i].color = color;
    //        }

    //        if (i < numOfHearts)
    //        {
    //            hearts[i].enabled = true;
    //        }
    //        else
    //        {
    //            hearts[i].enabled = false;
    //        }
    //    }
    //}
    public void hurt()
    {
        currenthealth -= 1;
        HeartsText.text = "" + currenthealth;
        if (currenthealth <= 0)
        {
            StartCoroutine(GameOverAfterDelay(0f));
        }
    }
    public void gameOver()
    {
        StartCoroutine(GameOverAfterDelay(1f));
        _GameOver.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
    IEnumerator GameOverAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            hurt();
        }
    }
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = "" + timeString;
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}