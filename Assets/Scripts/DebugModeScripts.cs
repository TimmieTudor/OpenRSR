using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugModeScripts : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private GUIStyle style = new GUIStyle();
    public Camera mainCamera;
    private CrusherAnim crusherAnim;
    private CrusherAnim crusherAnimSus;
    private RiserAnim riserAnim;
    private FrameAnim crusherAnimator1;
    private FrameAnim crusherAnimator2;
    private FrameAnim crusherAnimatorSus1;
    private FrameAnim crusherAnimatorSus2;
    private FrameAnim riserAnimator1;
    private FrameAnim riserAnimator2;
    public float currentFrame;
    public int objectID = 1;
    private bool isPlaying = false;
    private Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        // Set up GUI style for displaying FPS
        style.fontSize = 20;
        style.normal.textColor = Color.white;
        if (currentScene.name == "AnimationDebugScene") {
        crusherAnim = GameObject.Find("DeceBalus_Crusher").GetComponent<CrusherAnim>();
        crusherAnimSus = GameObject.Find("DeceBalus_Crusher_Sus").GetComponent<CrusherAnim>();
        riserAnim = GameObject.Find("Riser 2").GetComponent<RiserAnim>();
        crusherAnimator1 = crusherAnim.animator;
        crusherAnimator2 = crusherAnim.animator2;
        crusherAnimatorSus1 = crusherAnimSus.animator;
        crusherAnimatorSus2 = crusherAnimSus.animator2;
        riserAnimator1 = riserAnim.animator;
        riserAnimator2 = riserAnim.animator2;
        crusherAnim.enabled = false;
        crusherAnimSus.enabled = false;
        riserAnim.enabled = false;
        crusherAnimator1.SetFrame(1, 0f);
        crusherAnimator2.SetFrame(1, 0f);
        crusherAnimatorSus1.SetFrame(1, 0f);
        crusherAnimatorSus2.SetFrame(1, 0f);
        riserAnimator1.SetFrame(1, 0f);
        riserAnimator2.SetFrame(1, 0f);
        }
    }

    public void ScrollLeft() {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - 5f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        objectID--;
    }

    public void ScrollRight() {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 5f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        objectID++;
    }

    public void SetIsPlaying(bool isPlaying) {
        this.isPlaying = isPlaying;
    }

    public void PlayAnimation() {
        if (currentScene.name == "AnimationDebugScene") {
        if (objectID == 1) {
            if (currentFrame >= riserAnimator1.frames.Count - 1) {
                currentFrame = 0;
                isPlaying = false;
                return;
            }
            riserAnimator1.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            riserAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 2) {
            if (currentFrame >= crusherAnimator1.frames.Count - 1) {
                currentFrame = 0;
                isPlaying = false;
                return;
            }
            crusherAnimator1.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            crusherAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 3) {
            if (currentFrame >= crusherAnimatorSus1.frames.Count - 1) {
                currentFrame = 0;
                isPlaying = false;
                return;
            }
            crusherAnimatorSus1.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            crusherAnimatorSus2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        }
        currentFrame += 0.1f;
        }
    }

    private void Update()
    {
        // Calculate FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        if (isPlaying && currentScene.name == "AnimationDebugScene") {
            PlayAnimation();
        }
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
