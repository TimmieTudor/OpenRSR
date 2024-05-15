using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFreeze : MonoBehaviour
{
    public bool gamePaused = true;
    GameObject pausedText;
    GameObject editButton;
    GameObject settingsButton;
    float timer;
    public GameObject balus;
    private SphereMovement sphm;
    private SphereDragger sphd;
    private AudioPlayer audioPlayer;
    private GameManager manager;

    void Start()
    {
        timer = 0f;
        sphm = balus.GetComponent<SphereMovement>();
        sphd = balus.GetComponent<SphereDragger>();
        audioPlayer = balus.GetComponent<AudioPlayer>();
        manager = balus.GetComponent<GameManager>();
        gamePaused = true;
        pausedText = GameObject.Find("PausedText");
        editButton = GameObject.Find("EditButton");
        settingsButton = GameObject.Find("SettingsButton");
    }
    
    void Update()
    {
        if (gamePaused)
        {
            sphm.enabled = false;
            sphd.enabled = false;
            if (pausedText != null && editButton != null && settingsButton != null) {
            pausedText.SetActive(true);
            editButton.SetActive(true);
            settingsButton.SetActive(true);
            }
        }
        else
        {
            //sphm.enabled = true;
            //sphd.enabled = true;
            if (pausedText != null && editButton != null && settingsButton != null) {
            pausedText.SetActive(false);
            editButton.SetActive(false);
            settingsButton.SetActive(false);
            }
        }

        if (!manager.isGameOver) {
            if (Input.GetMouseButton(0))
            {
                // gamePaused = false;
                timer += 0.01f;
            }
            if (!Input.GetMouseButton(0)) {
                timer = 0f;
            }
            if (timer >= 1f) {
                gamePaused = false;
                manager.isGamePaused = false;
                audioPlayer.PlayAudio();
                sphm.enabled = true;
                sphd.enabled = true;
            }
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        if (sphm != null && sphd != null) {
            sphm.isNotFalling = true;
            sphm.enabled = false;
            sphd.enabled = false;
        }
        timer = 0f;
        if (audioPlayer != null) {
            audioPlayer.PauseAudio();
        }
    }
}