using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CrusherAnim : MonoBehaviour
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
        Transform baseTransform = gameObject.transform.Find("Base");
        Transform propellerTransform = gameObject.transform.Find("Propeller");
        GameObject baseObject = baseTransform.gameObject;
        GameObject propellerPart = propellerTransform.gameObject;
        baseObject.transform.position = new Vector3(baseObject.transform.position.x, 0.5f, baseObject.transform.position.z);
        propellerPart.transform.position = new Vector3(propellerPart.transform.position.x, 0.5f, propellerPart.transform.position.z);
        Frame initialFrame = new Frame(new Vector3(baseObject.transform.position.x, 0.5f, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.8f, 0.8f, 0.8f), baseTransform.gameObject);
        frames = new List<Frame>();
        frames.Add(initialFrame);
        frames.Add(initialFrame);
        float y = initialFrame.position.y;
        float scaleY = initialFrame.scale.y;
        float yChange = 0.1f;
        for (int i = 0; i < 60; i++)
        {
            yChange = 0.25f * RotationFunction(((float)i - 30f) / 16f) / 3f;
            y += yChange;
            //scaleY += 0.05f;
            frames.Add(new Frame(new Vector3(baseObject.transform.position.x, y, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.8f, scaleY, 0.8f), baseTransform.gameObject));
        }
        for (int i = 0; i < 8; i++) {
            frames.Add(new Frame(new Vector3(baseObject.transform.position.x, y, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.8f, scaleY, 0.8f), baseTransform.gameObject));
        }
        for (int i = 0; i < 6; i++) {
            y -= 0.35f;
            frames.Add(new Frame(new Vector3(baseObject.transform.position.x, y, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.8f, scaleY, 0.8f), baseTransform.gameObject));
        }
        frames.Add(new Frame(new Vector3(baseObject.transform.position.x, 0.5f, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.8f, 0.8f, 0.8f), baseTransform.gameObject));
        animator = new FrameAnim(frames);
        frames2 = new List<Frame>();
        Frame propellerFrame = new Frame(new Vector3(propellerPart.transform.position.x, 0.5f, propellerPart.transform.position.z), Quaternion.identity, new Vector3(0.8f, 0.8f, 0.8f), propellerTransform.gameObject);
        frames2.Add(propellerFrame);
        frames2.Add(propellerFrame);
        float propellerY = propellerFrame.position.y;
        float propellerYChange = 0.1f;
        float propellerRotationYChange = 0f;
        float propellerRotationY = 0f;
        for (int i = 0; i < 60; i++)
        {
            propellerYChange = 0.25f * RotationFunction(((float)i - 30f) / 16f) / 3f;
            propellerY += propellerYChange;
            propellerRotationYChange = RotationFunction(((float)i - 30f) / 16f) * 60f;
            propellerRotationY += propellerRotationYChange;
            frames2.Add(new Frame(new Vector3(propellerPart.transform.position.x, propellerY, propellerPart.transform.position.z), Quaternion.Euler(0f, propellerRotationY, 0f), new Vector3(0.8f, 0.8f, 0.8f), propellerTransform.gameObject));
        }
        for (int i = 0; i < 8; i++) {
            propellerRotationY += propellerRotationYChange;
            frames2.Add(new Frame(new Vector3(propellerPart.transform.position.x, propellerY, propellerPart.transform.position.z), Quaternion.Euler(0f, propellerRotationY, 0f), new Vector3(0.8f, 0.8f, 0.8f), propellerTransform.gameObject));
        }
        for (int i = 0; i < 6; i++)
        {
            propellerY -= 0.35f;
            propellerRotationY += propellerRotationYChange;
            frames2.Add(new Frame(new Vector3(propellerPart.transform.position.x, propellerY, propellerPart.transform.position.z), Quaternion.Euler(0f, propellerRotationY, 0f), new Vector3(0.8f, 0.8f, 0.8f), propellerTransform.gameObject));
        }
        frames2.Add(new Frame(new Vector3(propellerPart.transform.position.x, 0.5f, propellerPart.transform.position.z), Quaternion.Euler(0f, propellerRotationY, 0f), new Vector3(0.8f, 0.8f, 0.8f), propellerTransform.gameObject));
        animator2 = new FrameAnim(frames2);
        //Debug.Log(frames.Count);
        //Debug.Log(frames2.Count);
    }

    // Update is called once per frame
    void Update()
    {
        currentFrame = (int)Math.Floor((balus.transform.position.z - gameObject.transform.position.z + animationOffset) * 15f);
        float curFrame2 = (balus.transform.position.z - gameObject.transform.position.z + animationOffset) * 15f;
        float t = curFrame2 - (int)curFrame2;
        if (currentFrame < 0)
        {
            currentFrame = 0;
            animator.SetFrame(1, 0f);
            animator2.SetFrame(1, 0f);
        }
        else if (currentFrame >= 76)
        {
            animator.SetFrame(76, 0f);
            animator2.SetFrame(76, 0f);
        }
        else
        {
            animator.SetFrame(currentFrame + 1, t);
            animator2.SetFrame(currentFrame + 1, t);
        }
    }
}
