using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFreeze : MonoBehaviour
{
    bool gamePaused = true;
    GameObject pausedText;
    float timer = 0f;
    public GameObject balus;
    private SphereMovement sphm;
    private SphereDragger sphd;

    void Start()
    {
        sphm = balus.GetComponent<SphereMovement>();
        sphd = balus.GetComponent<SphereDragger>();
        gamePaused = true;
        pausedText = GameObject.Find("PausedText");
    }
    
    void Update()
    {
        if (gamePaused)
        {
            sphm.enabled = false;
            sphd.enabled = false;
            pausedText.SetActive(true);
        }
        else
        {
            sphm.enabled = true;
            sphd.enabled = true;
            pausedText.SetActive(false);
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