using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeftHammerLargeAnim : MonoBehaviour
{
    public GameObject balus;
    public float animationOffset = 11f;
    public FrameAnim animator;
    private List<Frame> frames;
    private int currentFrame = 0;

    private float RotationFunction(float x) {
        return Mathf.Exp(-x * x);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Transform hammerTransform = gameObject.transform.Find("DeceBalus_Large_Hammer_Hammer");
        GameObject hammerPart = hammerTransform.gameObject;
        hammerPart.transform.position = new Vector3(hammerPart.transform.position.x, 0.2f, hammerPart.transform.position.z);
        frames = new List<Frame>();
        Frame initialHammerFrame = new Frame(new Vector3(hammerPart.transform.position.x, 0f, hammerPart.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, -75f)), new Vector3(1f, 1f, 1f), hammerTransform.gameObject);
        frames.Add(initialHammerFrame);
        frames.Add(initialHammerFrame);
        float y = initialHammerFrame.position.y;
        float rotChange = 0.1f;
        float rotZ = initialHammerFrame.rotation.eulerAngles.z;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 10; j++) {
                rotChange = RotationFunction(((float)j - 5f) / 16f) * 8.491f * Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(15192f)))) * Mathf.Pow(-1f, (float)i+1f);
                rotZ -= rotChange;
                frames.Add(new Frame(new Vector3(hammerPart.transform.position.x, y, hammerPart.transform.position.z), Quaternion.Euler(0f, 0f, rotZ), new Vector3(1f, 1f, 1f), hammerTransform.gameObject));
            }
            for (int j = 0; j < 20; j++) {
                frames.Add(new Frame(new Vector3(hammerPart.transform.position.x, y, hammerPart.transform.position.z), Quaternion.Euler(0f, 0f, rotZ), new Vector3(1f, 1f, 1f), hammerTransform.gameObject));
            }
        }
        animator = new FrameAnim(frames);
    }

    public void ResetAnimation(Vector3 newPos) {
        Transform hammerTransform = gameObject.transform.Find("DeceBalus_Large_Hammer_Hammer");
        GameObject hammerPart = hammerTransform.gameObject;
        hammerPart.transform.position = new Vector3(newPos.x, 0.2f, newPos.z);
        frames = new List<Frame>();
        Frame initialHammerFrame = new Frame(new Vector3(newPos.x, 0f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, -75f)), new Vector3(1f, 1f, 1f), hammerTransform.gameObject);
        frames.Add(initialHammerFrame);
        frames.Add(initialHammerFrame);
        float y = initialHammerFrame.position.y;
        float rotChange = 0.1f;
        float rotZ = initialHammerFrame.rotation.eulerAngles.z;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 10; j++) {
                rotChange = RotationFunction(((float)j - 5f) / 16f) * 8.491f * Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(15192f)))) * Mathf.Pow(-1f, (float)i+1f);
                rotZ -= rotChange;
                frames.Add(new Frame(new Vector3(newPos.x, y, newPos.z), Quaternion.Euler(0f, 0f, rotZ), new Vector3(1f, 1f, 1f), hammerTransform.gameObject));
            }
            for (int j = 0; j < 20; j++) {
                frames.Add(new Frame(new Vector3(newPos.x, y, newPos.z), Quaternion.Euler(0f, 0f, rotZ), new Vector3(1f, 1f, 1f), hammerTransform.gameObject));
            }
        }
        animator = new FrameAnim(frames);
    }

    // Update is called once per frame
    void Update()
    {
        currentFrame = (int)Math.Floor((balus.transform.position.z - gameObject.transform.position.z + animationOffset) * 7f);
        float curFrame2 = (balus.transform.position.z - gameObject.transform.position.z + animationOffset) * 7f;
        float t = curFrame2 - (int)curFrame2;
        if (currentFrame < 0)
        {
            currentFrame = 0;
            animator.SetFrame(1, 0f);
            //animator2.SetFrame(1, 0f);
        }
        else if (currentFrame >= 120)
        {
            animator.SetFrame(120, 0.99f);
            //animator2.SetFrame(120, 0f);
        }
        else
        {
            animator.SetFrame(currentFrame + 1, t);
            //animator2.SetFrame(currentFrame + 1, t);
        }
    }
}
