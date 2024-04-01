using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MediumTreeAnim : MonoBehaviour
{
    public GameObject balus;
    public float animationOffset = 13f;
    public FrameAnim animator;
    public FrameAnim animator2;
    public FrameAnim animator3;
    public FrameAnim animator4;
    public FrameAnim animator5;
    private List<Frame> frames;
    private List<Frame> frames2;
    private List<Frame> frames3;
    private List<Frame> frames4;
    private List<Frame> frames5;
    private int currentFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        Transform stemTransform = gameObject.transform.Find("DeceBalus_Tree_Stem");
        Transform part2Transform = gameObject.transform.Find("DeceBalus_Tree_Part2");
        Transform part3Transform = gameObject.transform.Find("DeceBalus_Tree_Part3");
        Transform part4Transform = gameObject.transform.Find("DeceBalus_Tree_Part4");
        Transform part5Transform = gameObject.transform.Find("DeceBalus_Tree_Part5");
        GameObject stemObject = stemTransform.gameObject;
        GameObject part2Object = part2Transform.gameObject;
        GameObject part3Object = part3Transform.gameObject;
        GameObject part4Object = part4Transform.gameObject;
        GameObject part5Object = part5Transform.gameObject;
        Frame stemInitialFrame = new Frame(new Vector3(stemObject.transform.position.x, -0.6f, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject);
        frames = new List<Frame>();
        frames.Add(stemInitialFrame);
        frames.Add(stemInitialFrame);
        float YPos = stemInitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            YPos += 0.6f / 20f;
            frames.Add(new Frame(new Vector3(stemObject.transform.position.x, YPos, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
        }
        for (int i = 0; i < 100; i++) {
            frames.Add(new Frame(new Vector3(stemObject.transform.position.x, YPos, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
        }
        Frame part2InitialFrame = new Frame(new Vector3(part2Object.transform.position.x, -1f, part2Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object);
        frames2 = new List<Frame>();
        frames2.Add(part2InitialFrame);
        frames2.Add(part2InitialFrame);
        float part2YPos = part2InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part2YPos += 0.6f / 20f;
            frames2.Add(new Frame(new Vector3(part2Object.transform.position.x, part2YPos, part2Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        for (int i = 0; i < 100; i++) {
            frames2.Add(new Frame(new Vector3(part2Object.transform.position.x, part2YPos, part2Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        Frame part3InitialFrame = new Frame(new Vector3(part3Object.transform.position.x, -1.39f, part3Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object);
        frames3 = new List<Frame>();
        frames3.Add(part3InitialFrame);
        frames3.Add(part3InitialFrame);
        float part3YPos = part3InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part3YPos += 0.6f / 20f;
            frames3.Add(new Frame(new Vector3(part3Object.transform.position.x, part3YPos, part3Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        for (int i = 0; i < 20; i++) {
            part3YPos += 0.39f / 20f;
            frames3.Add(new Frame(new Vector3(part3Object.transform.position.x, part3YPos, part3Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        for (int i = 0; i < 80; i++) {
            frames3.Add(new Frame(new Vector3(part3Object.transform.position.x, part3YPos, part3Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        Frame part4InitialFrame = new Frame(new Vector3(part4Object.transform.position.x, -1.78f, part4Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object);
        frames4 = new List<Frame>();
        frames4.Add(part4InitialFrame);
        frames4.Add(part4InitialFrame);
        float part4YPos = part4InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part4YPos += 0.6f / 20f;
            frames4.Add(new Frame(new Vector3(part4Object.transform.position.x, part4YPos, part4Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        for (int i = 0; i < 40; i++) {
            part4YPos += 0.39f / 20f;
            frames4.Add(new Frame(new Vector3(part4Object.transform.position.x, part4YPos, part4Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        for (int i = 0; i < 60; i++) {
            frames4.Add(new Frame(new Vector3(part4Object.transform.position.x, part4YPos, part4Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        Frame part5InitialFrame = new Frame(new Vector3(part5Object.transform.position.x, -2.17f, part5Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object);
        frames5 = new List<Frame>();
        frames5.Add(part5InitialFrame);
        frames5.Add(part5InitialFrame);
        float part5YPos = part5InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part5YPos += 0.6f / 20f;
            frames5.Add(new Frame(new Vector3(part5Object.transform.position.x, part5YPos, part5Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        for (int i = 0; i < 60; i++) {
            part5YPos += 0.39f / 20f;
            frames5.Add(new Frame(new Vector3(part5Object.transform.position.x, part5YPos, part5Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        for (int i = 0; i < 40; i++) {
            frames5.Add(new Frame(new Vector3(part5Object.transform.position.x, part5YPos, part5Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        animator = new FrameAnim(frames);
        animator2 = new FrameAnim(frames2);
        animator3 = new FrameAnim(frames3);
        animator4 = new FrameAnim(frames4);
        animator5 = new FrameAnim(frames5);
    }

    public void ResetAnimation(Vector3 newPos) {
        Transform stemTransform = gameObject.transform.Find("DeceBalus_Tree_Stem");
        Transform part2Transform = gameObject.transform.Find("DeceBalus_Tree_Part2");
        Transform part3Transform = gameObject.transform.Find("DeceBalus_Tree_Part3");
        Transform part4Transform = gameObject.transform.Find("DeceBalus_Tree_Part4");
        Transform part5Transform = gameObject.transform.Find("DeceBalus_Tree_Part5");
        GameObject stemObject = stemTransform.gameObject;
        GameObject part2Object = part2Transform.gameObject;
        GameObject part3Object = part3Transform.gameObject;
        GameObject part4Object = part4Transform.gameObject;
        GameObject part5Object = part5Transform.gameObject;
        Frame stemInitialFrame = new Frame(new Vector3(newPos.x, -0.6f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject);
        frames = new List<Frame>();
        frames.Add(stemInitialFrame);
        frames.Add(stemInitialFrame);
        float YPos = stemInitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            YPos += 0.6f / 20f;
            frames.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
        }
        for (int i = 0; i < 100; i++) {
            frames.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
        }
        Frame part2InitialFrame = new Frame(new Vector3(newPos.x, -1f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object);
        frames2 = new List<Frame>();
        frames2.Add(part2InitialFrame);
        frames2.Add(part2InitialFrame);
        float part2YPos = part2InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part2YPos += 0.6f / 20f;
            frames2.Add(new Frame(new Vector3(newPos.x, part2YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        for (int i = 0; i < 100; i++) {
            frames2.Add(new Frame(new Vector3(newPos.x, part2YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        Frame part3InitialFrame = new Frame(new Vector3(newPos.x, -1.39f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object);
        frames3 = new List<Frame>();
        frames3.Add(part3InitialFrame);
        frames3.Add(part3InitialFrame);
        float part3YPos = part3InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part3YPos += 0.6f / 20f;
            frames3.Add(new Frame(new Vector3(newPos.x, part3YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        for (int i = 0; i < 20; i++) {
            part3YPos += 0.39f / 20f;
            frames3.Add(new Frame(new Vector3(newPos.x, part3YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        for (int i = 0; i < 80; i++) {
            frames3.Add(new Frame(new Vector3(newPos.x, part3YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        Frame part4InitialFrame = new Frame(new Vector3(newPos.x, -1.78f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object);
        frames4 = new List<Frame>();
        frames4.Add(part4InitialFrame);
        frames4.Add(part4InitialFrame);
        float part4YPos = part4InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part4YPos += 0.6f / 20f;
            frames4.Add(new Frame(new Vector3(newPos.x, part4YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        for (int i = 0; i < 40; i++) {
            part4YPos += 0.39f / 20f;
            frames4.Add(new Frame(new Vector3(newPos.x, part4YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        for (int i = 0; i < 60; i++) {
            frames4.Add(new Frame(new Vector3(newPos.x, part4YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        Frame part5InitialFrame = new Frame(new Vector3(newPos.x, -2.17f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object);
        frames5 = new List<Frame>();
        frames5.Add(part5InitialFrame);
        frames5.Add(part5InitialFrame);
        float part5YPos = part5InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part5YPos += 0.6f / 20f;
            frames5.Add(new Frame(new Vector3(newPos.x, part5YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        for (int i = 0; i < 60; i++) {
            part5YPos += 0.39f / 20f;
            frames5.Add(new Frame(new Vector3(newPos.x, part5YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        for (int i = 0; i < 40; i++) {
            frames5.Add(new Frame(new Vector3(newPos.x, part5YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        animator = new FrameAnim(frames);
        animator2 = new FrameAnim(frames2);
        animator3 = new FrameAnim(frames3);
        animator4 = new FrameAnim(frames4);
        animator5 = new FrameAnim(frames5);
    }

    // Update is called once per frame
    void Update()
    {
        currentFrame = (int)Math.Floor((balus.transform.position.z - gameObject.transform.position.z + animationOffset) * 13f);
        float curFrame2 = (balus.transform.position.z - gameObject.transform.position.z + animationOffset) * 13f;
        float t = curFrame2 - (int)curFrame2;
        if (currentFrame < 0)
        {
            currentFrame = 0;
            animator.SetFrame(1, 0f);
            animator2.SetFrame(1, 0f);
            animator3.SetFrame(1, 0f);
            animator4.SetFrame(1, 0f);
            animator5.SetFrame(1, 0f);
        }
        else if (currentFrame >= 120)
        {
            animator.SetFrame(120, 0.99f);
            animator2.SetFrame(120, 0.99f);
            animator3.SetFrame(120, 0.99f);
            animator4.SetFrame(120, 0.99f);
            animator5.SetFrame(120, 0.99f);
        }
        else
        {
            animator.SetFrame(currentFrame + 1, t);
            animator2.SetFrame(currentFrame + 1, t);
            animator3.SetFrame(currentFrame + 1, t);
            animator4.SetFrame(currentFrame + 1, t);
            animator5.SetFrame(currentFrame + 1, t);
        }
    }
}
