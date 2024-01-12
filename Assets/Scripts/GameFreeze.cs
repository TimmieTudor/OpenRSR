using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFreeze : MonoBehaviour
{
    bool gamePaused = true;
    GameObject pausedText;
    GameObject editButton;
    float timer;
    public GameObject balus;
    private SphereMovement sphm;
    private SphereDragger sphd;
    private AudioPlayer audioPlayer;

    void Start()
    {
        timer = 0f;
        sphm = balus.GetComponent<SphereMovement>();
        sphd = balus.GetComponent<SphereDragger>();
        audioPlayer = balus.GetComponent<AudioPlayer>();
        gamePaused = true;
        pausedText = GameObject.Find("PausedText");
        editButton = GameObject.Find("EditButton");
    }
    
    void Update()
    {
        if (gamePaused)
        {
            sphm.enabled = false;
            sphd.enabled = false;
            pausedText.SetActive(true);
            editButton.SetActive(true);
        }
        else
        {
            sphm.enabled = true;
            sphd.enabled = true;
            pausedText.SetActive(false);
            editButton.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            // gamePaused = false;
            timer += 0.01f;
        }
        if (timer >= 1f) {
            gamePaused = false;
            audioPlayer.PlayAudio();
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        timer = 0f;
        audioPlayer.PauseAudio();
    }
}