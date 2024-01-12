using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugModeScripts : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private GUIStyle style = new GUIStyle();

    private void Start()
    {
        // Set up GUI style for displaying FPS
        style.fontSize = 20;
        style.normal.textColor = Color.white;
    }

    private void Update()
    {
        // Calculate FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        // Display FPS if in development build or in the Unity Editor
        if (Debug.isDebugBuild || Application.isEditor)
        {
            int fps = Mathf.RoundToInt(1.0f / deltaTime);
            string text = $"FPS: {fps}";

            // Display FPS on the screen
            GUI.Label(new Rect(Screen.width / 2, 10, 100, 20), text, style);
        }
    }
}
