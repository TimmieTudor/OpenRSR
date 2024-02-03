using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RiserAnim : MonoBehaviour
{
    public GameObject balus;

    public FrameAnim animator;
    public FrameAnim animator2;
    private List<Frame> frames;
    private List<Frame> frames2;
    private int currentFrame = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform baseTransform = gameObject.transform.Find("Base_2");
        Transform coloredTransform = gameObject.transform.Find("ColoredPart");
        GameObject baseObject = baseTransform.gameObject;
        GameObject coloredPart = coloredTransform.gameObject;
        baseObject.transform.position = new Vector3(baseObject.transform.position.x, -0.2f, baseObject.transform.position.z);
        baseObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        coloredPart.transform.position = new Vector3(coloredPart.transform.position.x, -1f, coloredPart.transform.position.z);
        Frame initialFrame = new Frame(new Vector3(baseObject.transform.position.x, -0.2f, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f), baseTransform.gameObject);
        frames = new List<Frame>();
        frames.Add(initialFrame);
        frames.Add(initialFrame);
        float y = initialFrame.position.y;
        float scaleY = initialFrame.scale.y;
        for (int i = 0; i < 16; i++)
        {
            y += 0.049f;
            //scaleY += 0.05f;
            frames.Add(new Frame(new Vector3(baseObject.transform.position.x, y, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.4f, scaleY, 0.4f), baseTransform.gameObject));
        }
        for (int i = 0; i < 10; i++) {
            frames.Add(new Frame(new Vector3(baseObject.transform.position.x, 0.5f, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f), baseTransform.gameObject));
        }
        animator = new FrameAnim(frames);
        frames2 = new List<Frame>();
        Frame coloredFrame = new Frame(new Vector3(coloredPart.transform.position.x, -1f, coloredPart.transform.position.z), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f), coloredTransform.gameObject);
        frames2.Add(coloredFrame);
        frames2.Add(coloredFrame);
        float coloredY = coloredFrame.position.y;
        for (int i = 0; i < 18; i++)
        {
            //coloredY += 0.2f;
            frames2.Add(new Frame(new Vector3(coloredPart.transform.position.x, coloredY, coloredPart.transform.position.z), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f), coloredTransform.gameObject));
        }
        for (int i = 0; i < 8; i++)
        {
            coloredY += 0.025f;
            frames2.Add(new Frame(new Vector3(coloredPart.transform.position.x, 0.5f, coloredPart.transform.position.z), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f), baseTransform.gameObject));
        }
        animator2 = new FrameAnim(frames2);
        //Debug.Log(frames.Count);
        //Debug.Log(frames2.Count);
    }

    // Update is called once per frame
    void Update()
    {
        currentFrame = (int)Math.Floor((balus.transform.position.z - gameObject.transform.position.z + 6f) * 10f);
        float curFrame2 = (balus.transform.position.z - gameObject.transform.position.z + 6f) * 10f;
        float t = curFrame2 - (int)curFrame2;
        if (currentFrame < 0)
        {
            currentFrame = 0;
            animator.SetFrame(1, 0f);
            animator2.SetFrame(1, 0f);
        }
        else if (currentFrame >= 27)
        {
            animator.SetFrame(27, 0f);
            animator2.SetFrame(27, 0f);
        }
        else
        {
            animator.SetFrame(currentFrame + 1, t);
            animator2.SetFrame(currentFrame + 1, t);
        }
    }
}
