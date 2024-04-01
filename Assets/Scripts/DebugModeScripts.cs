using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugModeScripts : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private GUIStyle style = new GUIStyle();
    public Camera mainCamera;
    public GameObject balus;
    private CrusherAnim crusherAnim;
    private CrusherAnim crusherAnimSus;
    private RiserAnim riserAnim;
    private LeftHammerAnim hammerAnim;
    private LeftHammerLargeAnim hammerLargeAnim;
    private LargeTreeAnim largeTreeAnim;
    private MediumTreeAnim mediumTreeAnim;
    private SmallTreeAnim smallTreeAnim;
    private LaserAnim laserAnim;
    private FloaterAnim floaterAnim;
    private SpotlightAnim spotlightAnim;
    private FrameAnim crusherAnimator1;
    private FrameAnim crusherAnimator2;
    private FrameAnim crusherAnimatorSus1;
    private FrameAnim crusherAnimatorSus2;
    private FrameAnim riserAnimator1;
    private FrameAnim riserAnimator2;
    private FrameAnim hammerAnimator;
    private FrameAnim hammerAnimator2;
    private FrameAnim hammerLargeAnimator;
    private FrameAnim largeTreeAnimator;
    private FrameAnim largeTreeAnimator2;
    private FrameAnim largeTreeAnimator3;
    private FrameAnim largeTreeAnimator4;
    private FrameAnim largeTreeAnimator5;
    private FrameAnim largeTreeAnimator6;
    private FrameAnim largeTreeAnimator7;
    private FrameAnim largeTreeAnimator8;
    private FrameAnim mediumTreeAnimator;
    private FrameAnim mediumTreeAnimator2;
    private FrameAnim mediumTreeAnimator3;
    private FrameAnim mediumTreeAnimator4;
    private FrameAnim mediumTreeAnimator5;
    private FrameAnim smallTreeAnimator;
    private FrameAnim smallTreeAnimator2;
    private FrameAnim smallTreeAnimator3;
    private FrameAnim smallTreeAnimator4;
    private FrameAnim smallTreeAnimator5;
    private FrameAnim smallTreeAnimator6;
    private FrameAnim smallTreeAnimator7;
    private FrameAnim laserAnimator;
    private FrameAnim laserAnimator2;
    private FrameAnim laserAnimator3;
    private FrameAnim floaterAnimator;
    private FrameAnim floaterAnimator2;
    private FrameAnim spotlightAnimator;
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
        hammerAnim = GameObject.Find("DeceBalus_Small_Hammer_Left").GetComponent<LeftHammerAnim>();
        hammerLargeAnim = GameObject.Find("DeceBalus_Large_Hammer_Left").GetComponent<LeftHammerLargeAnim>();
        largeTreeAnim = GameObject.Find("DeceBalus_Large_Tree").GetComponent<LargeTreeAnim>();
        mediumTreeAnim = GameObject.Find("DeceBalus_Medium_Tree").GetComponent<MediumTreeAnim>();
        smallTreeAnim = GameObject.Find("DeceBalus_Small_Tree").GetComponent<SmallTreeAnim>();
        laserAnim = GameObject.Find("DeceBalus_Laser").GetComponent<LaserAnim>();
        floaterAnim = GameObject.Find("DeceBalus_Floater").GetComponent<FloaterAnim>();
        spotlightAnim = GameObject.Find("DeceBalus_Spotlight").GetComponent<SpotlightAnim>();
        crusherAnimator1 = crusherAnim.animator;
        crusherAnimator2 = crusherAnim.animator2;
        crusherAnimatorSus1 = crusherAnimSus.animator;
        crusherAnimatorSus2 = crusherAnimSus.animator2;
        riserAnimator1 = riserAnim.animator;
        riserAnimator2 = riserAnim.animator2;
        hammerAnimator = hammerAnim.animator;
        hammerAnimator2 = hammerAnim.animator2;
        hammerLargeAnimator = hammerLargeAnim.animator;
        largeTreeAnimator = largeTreeAnim.animator;
        largeTreeAnimator2 = largeTreeAnim.animator2;
        largeTreeAnimator3 = largeTreeAnim.animator3;
        largeTreeAnimator4 = largeTreeAnim.animator4;
        largeTreeAnimator5 = largeTreeAnim.animator5;
        largeTreeAnimator6 = largeTreeAnim.animator6;
        largeTreeAnimator7 = largeTreeAnim.animator7;
        largeTreeAnimator8 = largeTreeAnim.animator8;
        mediumTreeAnimator = mediumTreeAnim.animator;
        mediumTreeAnimator2 = mediumTreeAnim.animator2;
        mediumTreeAnimator3 = mediumTreeAnim.animator3;
        mediumTreeAnimator4 = mediumTreeAnim.animator4;
        mediumTreeAnimator5 = mediumTreeAnim.animator5;
        smallTreeAnimator = smallTreeAnim.animator;
        smallTreeAnimator2 = smallTreeAnim.animator2;
        smallTreeAnimator3 = smallTreeAnim.animator3;
        smallTreeAnimator4 = smallTreeAnim.animator4;
        smallTreeAnimator5 = smallTreeAnim.animator5;
        smallTreeAnimator6 = smallTreeAnim.animator6;
        smallTreeAnimator7 = smallTreeAnim.animator7;
        laserAnimator = laserAnim.animator;
        laserAnimator2 = laserAnim.animator2;
        laserAnimator3 = laserAnim.animator3;
        floaterAnimator = floaterAnim.animator;
        floaterAnimator2 = floaterAnim.animator2;
        spotlightAnimator = spotlightAnim.animator;
        crusherAnim.enabled = false;
        crusherAnimSus.enabled = false;
        riserAnim.enabled = false;
        hammerAnim.enabled = false;
        hammerLargeAnim.enabled = false;
        largeTreeAnim.enabled = false;
        mediumTreeAnim.enabled = false;
        smallTreeAnim.enabled = false;
        laserAnim.enabled = false;
        floaterAnim.enabled = false;
        spotlightAnim.enabled = false;
        crusherAnimator1.SetFrame(1, 0f);
        crusherAnimator2.SetFrame(1, 0f);
        crusherAnimatorSus1.SetFrame(1, 0f);
        crusherAnimatorSus2.SetFrame(1, 0f);
        riserAnimator1.SetFrame(1, 0f);
        riserAnimator2.SetFrame(1, 0f);
        hammerAnimator.SetFrame(1, 0f);
        hammerAnimator2.SetFrame(1, 0f);
        hammerLargeAnimator.SetFrame(1, 0f);
        largeTreeAnimator.SetFrame(1, 0f);
        largeTreeAnimator2.SetFrame(1, 0f);
        largeTreeAnimator3.SetFrame(1, 0f);
        largeTreeAnimator4.SetFrame(1, 0f);
        largeTreeAnimator5.SetFrame(1, 0f);
        largeTreeAnimator6.SetFrame(1, 0f);
        largeTreeAnimator7.SetFrame(1, 0f);
        largeTreeAnimator8.SetFrame(1, 0f);
        mediumTreeAnimator.SetFrame(1, 0f);
        mediumTreeAnimator2.SetFrame(1, 0f);
        mediumTreeAnimator3.SetFrame(1, 0f);
        mediumTreeAnimator4.SetFrame(1, 0f);
        mediumTreeAnimator5.SetFrame(1, 0f);
        smallTreeAnimator.SetFrame(1, 0f);
        smallTreeAnimator2.SetFrame(1, 0f);
        smallTreeAnimator3.SetFrame(1, 0f);
        smallTreeAnimator4.SetFrame(1, 0f);
        smallTreeAnimator5.SetFrame(1, 0f);
        smallTreeAnimator6.SetFrame(1, 0f);
        smallTreeAnimator7.SetFrame(1, 0f);
        laserAnimator.SetFrame(1, 0f);
        laserAnimator2.SetFrame(1, 0f);
        laserAnimator3.SetFrame(1, 0f);
        floaterAnimator.SetFrame(1, 0f);
        floaterAnimator2.SetFrame(1, 0f);
        spotlightAnimator.SetFrame(1, 0f);
        }
    }

    public void ScrollLeft() {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - 6f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        objectID--;
    }

    public void ScrollRight() {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 6f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        objectID++;
    }

    public void SetIsPlaying(bool isPlaying) {
        this.isPlaying = isPlaying;
        //OffsetBalus();
    }

    public void PlayAnimation() {
        if (currentScene.name == "AnimationDebugScene") {
        if (objectID == 1) {
            if (currentFrame >= riserAnimator1.frames.Count - 1) {
                currentFrame = 0;
                riserAnimator1.SetFrame(1, 0f);
                riserAnimator2.SetFrame(1, 0f);
                isPlaying = false;
                return;
            }
            riserAnimator1.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            riserAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 2) {
            if (currentFrame >= crusherAnimator1.frames.Count - 1) {
                currentFrame = 0;
                crusherAnimator1.SetFrame(1, 0f);
                crusherAnimator2.SetFrame(1, 0f);
                isPlaying = false;
                return;
            }
            crusherAnimator1.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            crusherAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 3) {
            if (currentFrame >= crusherAnimatorSus1.frames.Count - 1) {
                currentFrame = 0;
                crusherAnimatorSus1.SetFrame(1, 0f);
                crusherAnimatorSus2.SetFrame(1, 0f);
                isPlaying = false;
                return;
            }
            crusherAnimatorSus1.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            crusherAnimatorSus2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 4) {
            if (currentFrame >= hammerAnimator.frames.Count - 1) {
                currentFrame = 0;
                hammerAnimator.SetFrame(1, 0f);
                hammerAnimator2.SetFrame(1, 0f);
                isPlaying = false;
                return;
            }
            hammerAnimator.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            hammerAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 5) {
            if (currentFrame >= hammerLargeAnimator.frames.Count - 1) {
                currentFrame = 0;
                hammerLargeAnimator.SetFrame(1, 0f);
                isPlaying = false;
                return;
            }
            hammerLargeAnimator.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 6) {
            if (currentFrame >= largeTreeAnimator.frames.Count - 1) {
                currentFrame = 0;
                largeTreeAnimator.SetFrame(1, 0f);
                largeTreeAnimator2.SetFrame(1, 0f);
                largeTreeAnimator3.SetFrame(1, 0f);
                largeTreeAnimator4.SetFrame(1, 0f);
                largeTreeAnimator5.SetFrame(1, 0f);
                largeTreeAnimator6.SetFrame(1, 0f);
                largeTreeAnimator7.SetFrame(1, 0f);
                largeTreeAnimator8.SetFrame(1, 0f);
                isPlaying = false;
                return;
            }
            largeTreeAnimator.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            largeTreeAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            largeTreeAnimator3.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            largeTreeAnimator4.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            largeTreeAnimator5.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            largeTreeAnimator6.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            largeTreeAnimator7.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            largeTreeAnimator8.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 7) {
            if (currentFrame >= mediumTreeAnimator.frames.Count - 1) {
                currentFrame = 0;
                mediumTreeAnimator.SetFrame(1, 0f);
                mediumTreeAnimator2.SetFrame(1, 0f);
                mediumTreeAnimator3.SetFrame(1, 0f);
                mediumTreeAnimator4.SetFrame(1, 0f);
                mediumTreeAnimator5.SetFrame(1, 0f);
                isPlaying = false;
                return;
            }
            mediumTreeAnimator.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            mediumTreeAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            mediumTreeAnimator3.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            mediumTreeAnimator4.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            mediumTreeAnimator5.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 8) {
            if (currentFrame >= smallTreeAnimator.frames.Count - 1) {
                currentFrame = 0;
                smallTreeAnimator.SetFrame(1, 0f);
                smallTreeAnimator2.SetFrame(1, 0f);
                smallTreeAnimator3.SetFrame(1, 0f);
                smallTreeAnimator4.SetFrame(1, 0f);
                smallTreeAnimator5.SetFrame(1, 0f);
                smallTreeAnimator6.SetFrame(1, 0f);
                smallTreeAnimator7.SetFrame(1, 0f);
                isPlaying = false;
                return;
            }
            smallTreeAnimator.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            smallTreeAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            smallTreeAnimator3.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            smallTreeAnimator4.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            smallTreeAnimator5.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            smallTreeAnimator6.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            smallTreeAnimator7.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        } else if (objectID == 9) {
            if (currentFrame >= laserAnimator.frames.Count - 1) {
                currentFrame = 0;
                laserAnimator.SetFrame(1, 0f);
                laserAnimator2.SetFrame(1, 0f);
                laserAnimator3.SetFrame(1, 0f);
                laserAnim.laserObject.SetActive(false);
                isPlaying = false;
                return;
            }
            laserAnimator.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            laserAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            laserAnimator3.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            if (currentFrame >= 80 && currentFrame < 120) {
                laserAnim.laserObject.SetActive(true);
            }
        } else if (objectID == 10) {
            if (currentFrame >= floaterAnimator.frames.Count - 1) {
                currentFrame = 0;
                floaterAnimator.SetFrame(1, 0f);
                floaterAnimator2.SetFrame(1, 0f);
                floaterAnim.part1Object.SetActive(true);
                floaterAnim.part2Object.SetActive(false);
                floaterAnim.part3Object.SetActive(false);
                isPlaying = false;
                return;
            }
            floaterAnimator.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            floaterAnimator2.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
            if (currentFrame >= 100 && currentFrame < 120) {
                floaterAnim.part1Object.SetActive(false);
                floaterAnim.part2Object.SetActive(true);
                floaterAnim.part3Object.SetActive(true);
            }
        } else if (objectID == 11) {
            if (currentFrame >= spotlightAnimator.frames.Count - 1) {
                currentFrame = 0;
                spotlightAnimator.SetFrame(1, 0f);
                isPlaying = false;
                return;
            }
            spotlightAnimator.SetFrame((int)currentFrame + 1, currentFrame - (int)currentFrame);
        }
        currentFrame += 0.1f;
        }
    }

    public void OffsetBalus() {
        if (currentScene.name == "AnimationDebugScene") {
            if (objectID == 1) {
                balus.transform.position = new Vector3(balus.transform.position.x, balus.transform.position.y, balus.transform.position.z - riserAnim.animationOffset);
            } else if (objectID == 2) {
                balus.transform.position = new Vector3(balus.transform.position.x, balus.transform.position.y, balus.transform.position.z - crusherAnim.animationOffset);
            } else if (objectID == 3) {
                balus.transform.position = new Vector3(balus.transform.position.x, balus.transform.position.y, balus.transform.position.z - crusherAnimSus.animationOffset);
            } else if (objectID == 4) {
                balus.transform.position = new Vector3(balus.transform.position.x, balus.transform.position.y, balus.transform.position.z - hammerAnim.animationOffset);
            } else if (objectID == 5) {
                balus.transform.position = new Vector3(balus.transform.position.x, balus.transform.position.y, balus.transform.position.z - hammerLargeAnim.animationOffset);
            } else if (objectID == 6) {
                balus.transform.position = new Vector3(balus.transform.position.x, balus.transform.position.y, balus.transform.position.z - largeTreeAnim.animationOffset);
            } else if (objectID == 7) {
                balus.transform.position = new Vector3(balus.transform.position.x, balus.transform.position.y, balus.transform.position.z - mediumTreeAnim.animationOffset);
            } else if (objectID == 8) {
                balus.transform.position = new Vector3(balus.transform.position.x, balus.transform.position.y, balus.transform.position.z - smallTreeAnim.animationOffset);
            }
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
