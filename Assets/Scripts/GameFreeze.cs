using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFreeze : MonoBehaviour
{
    bool gamePaused = true;
    float timer = 0f;

    void Start()
    {
        gamePaused = true;
    }
    
    void Update()
    {
        if (gamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
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