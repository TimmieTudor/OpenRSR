using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFreeze : MonoBehaviour
{
    bool gamePaused = true;
    GameObject pausedText;
    float timer = 0f;

    void Start()
    {
        gamePaused = true;
        pausedText = GameObject.Find("PausedText");
    }
    
    void Update()
    {
        if (gamePaused)
        {
            Time.timeScale = 0f;
            pausedText.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausedText.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            gamePaused = false;
            timer = 0f;
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        timer = 0f;
    }
}