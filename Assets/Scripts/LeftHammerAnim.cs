using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeftHammerAnim : MonoBehaviour
{
    public GameObject balus;
    public float animationOffset = 11f;
    public FrameAnim animator;
    public FrameAnim animator2;
    private List<Frame> frames;
    private List<Frame> frames2;
    private int currentFrame = 0;

    private float RotationFunction(float x) {
        return Mathf.Exp(-x * x);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Transform baseTransform = gameObject.transform.Find("DeceBalus_Small_Hammer_Base");
        Transform hammerTransform = gameObject.transform.Find("DeceBalus_Small_Hammer_Hammer");
        GameObject baseObject = baseTransform.gameObject;
        GameObject hammerPart = hammerTransform.gameObject;
        baseObject.transform.position = new Vector3(baseObject.transform.position.x, 0f, baseObject.transform.position.z);
        hammerPart.transform.position = new Vector3(hammerPart.transform.position.x, 0f, hammerPart.transform.position.z);
        Frame initialFrame = new Frame(new Vector3(baseObject.transform.position.x, 0f, baseObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 180f, 0f)), new Vector3(1f, 1f, 1f), baseTransform.gameObject);
        frames = new List<Frame>();
        frames2 = new List<Frame>();
        frames.Add(initialFrame);
        frames.Add(initialFrame);
        Frame initialHammerFrame = new Frame(new Vector3(hammerPart.transform.position.x, 0f, hammerPart.transform.position.z), Quaternion.Euler(new Vector3(0f, 180f, 0f)), new Vector3(1f, 1f, 1f), hammerTransform.gameObject);
        frames2.Add(initialHammerFrame);
        frames2.Add(initialHammerFrame);
        float y = initialFrame.position.y;
        float rotChange = 0.1f;
        float rotY = initialFrame.rotation.eulerAngles.y;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 15; j++) {
                rotChange = RotationFunction(((float)j - 7.5f) / 16f) * 12.898f;
                rotY -= rotChange;
                frames.Add(new Frame(new Vector3(baseObject.transform.position.x, y, baseObject.transform.position.z), Quaternion.Euler(0f, rotY, 0f), new Vector3(1f, 1f, 1f), baseTransform.gameObject));
                frames2.Add(new Frame(new Vector3(hammerPart.transform.position.x, y, hammerPart.transform.position.z), Quaternion.Euler(0f, rotY, 0f), new Vector3(1f, 1f, 1f), hammerTransform.gameObject));
            }
            for (int j = 0; j < 15; j++) {
                frames.Add(new Frame(new Vector3(baseObject.transform.position.x, y, baseObject.transform.position.z), Quaternion.Euler(0f, rotY, 0f), new Vector3(1f, 1f, 1f), baseTransform.gameObject));
                frames2.Add(new Frame(new Vector3(hammerPart.transform.position.x, y, hammerPart.transform.position.z), Quaternion.Euler(0f, rotY, 0f), new Vector3(1f, 1f, 1f), hammerTransform.gameObject));
            }
        }
        animator = new FrameAnim(frames);
        animator2 = new FrameAnim(frames2);
    }

    public void ResetAnimation(Vector3 newPos) {
        Transform baseTransform = gameObject.transform.Find("DeceBalus_Small_Hammer_Base");
        Transform hammerTransform = gameObject.transform.Find("DeceBalus_Small_Hammer_Hammer");
        GameObject baseObject = baseTransform.gameObject;
        GameObject hammerPart = hammerTransform.gameObject;
        baseObject.transform.position = new Vector3(newPos.x, 0f, newPos.z);
        hammerPart.transform.position = new Vector3(newPos.x, 0f, newPos.z);
        Frame initialFrame = new Frame(new Vector3(newPos.x, 0f, newPos.z), Quaternion.Euler(new Vector3(0f, 180f, 0f)), new Vector3(1f, 1f, 1f), baseTransform.gameObject);
        frames = new List<Frame>();
        frames2 = new List<Frame>();
        frames.Add(initialFrame);
        frames.Add(initialFrame);
        Frame initialHammerFrame = new Frame(new Vector3(newPos.x, 0f, newPos.z), Quaternion.Euler(new Vector3(0f, 180f, 0f)), new Vector3(1f, 1f, 1f), hammerTransform.gameObject);
        frames2.Add(initialHammerFrame);
        frames2.Add(initialHammerFrame);
        float y = initialFrame.position.y;
        float rotChange = 0.1f;
        float rotY = initialFrame.rotation.eulerAngles.y;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 15; j++) {
                rotChange = RotationFunction(((float)j - 7.5f) / 16f) * 12.898f;
                rotY -= rotChange;
                frames.Add(new Frame(new Vector3(newPos.x, y, newPos.z), Quaternion.Euler(0f, rotY, 0f), new Vector3(1f, 1f, 1f), baseTransform.gameObject));
                frames2.Add(new Frame(new Vector3(newPos.x, y, newPos.z), Quaternion.Euler(0f, rotY, 0f), new Vector3(1f, 1f, 1f), hammerTransform.gameObject));
            }
            for (int j = 0; j < 15; j++) {
                frames.Add(new Frame(new Vector3(newPos.x, y, newPos.z), Quaternion.Euler(0f, rotY, 0f), new Vector3(1f, 1f, 1f), baseTransform.gameObject));
                frames2.Add(new Frame(new Vector3(newPos.x, y, newPos.z), Quaternion.Euler(0f, rotY, 0f), new Vector3(1f, 1f, 1f), hammerTransform.gameObject));
            }
        }
        animator = new FrameAnim(frames);
        animator2 = new FrameAnim(frames2);
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
            animator2.SetFrame(1, 0f);
        }
        else if (currentFrame >= 120)
        {
            animator.SetFrame(120, 0.99f);
            animator2.SetFrame(120, 0.99f);
        }
        else
        {
            animator.SetFrame(currentFrame + 1, t);
            animator2.SetFrame(currentFrame + 1, t);
        }
    }
}
