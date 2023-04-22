using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RiserAnim : MonoBehaviour
{
    public GameObject balus;

    private FrameAnim animator;
    private FrameAnim animator2;
    private List<Frame> frames;
    private List<Frame> frames2;
    private int currentFrame = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform baseTransform = gameObject.transform.Find("Base");
        Transform coloredTransform = gameObject.transform.Find("ColoredPart");
        GameObject baseObject = baseTransform.gameObject;
        GameObject coloredPart = coloredTransform.gameObject;
        baseObject.transform.position = new Vector3(baseObject.transform.position.x, 0.15f, baseObject.transform.position.z);
        baseObject.transform.localScale = new Vector3(0.9f, 0.2f, 0.9f);
        coloredPart.transform.position = new Vector3(coloredPart.transform.position.x, 10.55f, coloredPart.transform.position.z);
        Frame initialFrame = new Frame(new Vector3(baseObject.transform.position.x, 0.15f, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.9f, 0.2f, 0.9f), baseTransform.gameObject);
        frames = new List<Frame>();
        frames.Add(initialFrame);
        frames.Add(initialFrame);
        float y = initialFrame.position.y;
        float scaleY = initialFrame.scale.y;
        for (int i = 0; i < 14; i++)
        {
            y += 0.025f;
            scaleY += 0.05f;
            frames.Add(new Frame(new Vector3(baseObject.transform.position.x, y, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.9f, scaleY, 0.9f), baseTransform.gameObject));
        }
        frames.Add(new Frame(new Vector3(baseObject.transform.position.x, 0.55f, baseObject.transform.position.z), Quaternion.identity, new Vector3(0.9f, 0.9f, 0.9f), baseTransform.gameObject));
        animator = new FrameAnim(frames);
        frames2 = new List<Frame>();
        Frame coloredFrame = new Frame(new Vector3(coloredPart.transform.position.x, 10.55f, coloredPart.transform.position.z), Quaternion.identity, new Vector3(0.45f, 0.9f, 0.45f), coloredTransform.gameObject);
        frames2.Add(coloredFrame);
        frames2.Add(coloredFrame);
        float coloredY = coloredFrame.position.y;
        for (int i = 0; i < 10; i++)
        {
            coloredY --;
            frames2.Add(new Frame(new Vector3(coloredPart.transform.position.x, coloredY, coloredPart.transform.position.z), Quaternion.identity, new Vector3(0.45f, 0.9f, 0.45f), coloredTransform.gameObject));
        }
        for (int i = 0; i < 2; i++)
        {
            frames2.Add(new Frame(new Vector3(coloredPart.transform.position.x, 0.55f, coloredPart.transform.position.z), Quaternion.identity, new Vector3(0.45f, 0.9f, 0.45f), baseTransform.gameObject));
        }
        animator2 = new FrameAnim(frames2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Floor((balus.transform.position.z - gameObject.transform.position.z + 4.5f) * 10f) < 0)
        {
            currentFrame = 0;
        }
        else if (Math.Floor((balus.transform.position.z - gameObject.transform.position.z + 4.5f) * 10f) >= frames.Count - 2 && Math.Floor((balus.transform.position.z - gameObject.transform.position.z + 4.5f) * 10f) < 27)
        {
            animator.SetFrame(frames.Count - 2);
            currentFrame = (int)Math.Floor((balus.transform.position.z - gameObject.transform.position.z + 4.5f) * 10f);
            animator2.SetFrame(currentFrame - 15);
        }
        else if (Math.Floor((balus.transform.position.z - gameObject.transform.position.z + 4.5f) * 10f) >= (frames.Count - 3) + (frames2.Count - 3))
        {
            
            animator.SetFrame(frames.Count - 2);
            animator2.SetFrame(frames2.Count - 2);
        }
        else
        {
            currentFrame = (int)Math.Floor((balus.transform.position.z - gameObject.transform.position.z + 4.5f) * 10f);
            animator.SetFrame(currentFrame);
        }
    }
}
