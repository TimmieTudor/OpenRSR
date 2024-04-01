using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LargeTreeAnim : MonoBehaviour
{
    public GameObject balus;
    public float animationOffset = 13f;
    public FrameAnim animator;
    public FrameAnim animator2;
    public FrameAnim animator3;
    public FrameAnim animator4;
    public FrameAnim animator5;
    public FrameAnim animator6;
    public FrameAnim animator7;
    public FrameAnim animator8;
    private List<Frame> frames;
    private List<Frame> frames2;
    private List<Frame> frames3;
    private List<Frame> frames4;
    private List<Frame> frames5;
    private List<Frame> frames6;
    private List<Frame> frames7;
    private List<Frame> frames8;
    private int currentFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        Transform stemTransform = gameObject.transform.Find("DeceBalus_Tree_Stem");
        Transform part1Transform = gameObject.transform.Find("DeceBalus_Tree_Part1");
        Transform part2Transform = gameObject.transform.Find("DeceBalus_Tree_Part2");
        Transform part3Transform = gameObject.transform.Find("DeceBalus_Tree_Part3");
        Transform part4Transform = gameObject.transform.Find("DeceBalus_Tree_Part4");
        Transform part5Transform = gameObject.transform.Find("DeceBalus_Tree_Part5");
        Transform part6Transform = gameObject.transform.Find("DeceBalus_Tree_Part6");
        Transform part7Transform = gameObject.transform.Find("DeceBalus_Tree_Part7");
        GameObject stemObject = stemTransform.gameObject;
        GameObject part1Object = part1Transform.gameObject;
        GameObject part2Object = part2Transform.gameObject;
        GameObject part3Object = part3Transform.gameObject;
        GameObject part4Object = part4Transform.gameObject;
        GameObject part5Object = part5Transform.gameObject;
        GameObject part6Object = part6Transform.gameObject;
        GameObject part7Object = part7Transform.gameObject;
        Frame stemInitialFrame = new Frame(new Vector3(stemObject.transform.position.x, -0.6f, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject);
        frames = new List<Frame>();
        frames.Add(stemInitialFrame);
        frames.Add(stemInitialFrame);
        float YPos = stemInitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            YPos += 0.6f / 20f;
            frames.Add(new Frame(new Vector3(stemObject.transform.position.x, YPos, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
        }
        for (int i = 0; i < 160; i++) {
            frames.Add(new Frame(new Vector3(stemObject.transform.position.x, YPos, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
        }
        Frame part1InitialFrame = new Frame(new Vector3(part1Object.transform.position.x, -0.6f, part1Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part1Object);
        frames2 = new List<Frame>();
        frames2.Add(part1InitialFrame);
        frames2.Add(part1InitialFrame);
        float part1YPos = part1InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part1YPos += 0.6f / 20f;
            frames2.Add(new Frame(new Vector3(part1Object.transform.position.x, part1YPos, part1Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part1Object));
        }
        for (int i = 0; i < 160; i++) {
            frames2.Add(new Frame(new Vector3(part1Object.transform.position.x, part1YPos, part1Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part1Object));
        }
        Frame part2InitialFrame = new Frame(new Vector3(part2Object.transform.position.x, -0.99f, part2Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object);
        frames3 = new List<Frame>();
        frames3.Add(part2InitialFrame);
        frames3.Add(part2InitialFrame);
        float part2YPos = part2InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part2YPos += 0.6f / 20f;
            frames3.Add(new Frame(new Vector3(part2Object.transform.position.x, part2YPos, part2Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        for (int i = 0; i < 20; i++) {
            part2YPos += 0.39f / 20f;
            frames3.Add(new Frame(new Vector3(part2Object.transform.position.x, part2YPos, part2Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        for (int i = 0; i < 140; i++) {
            frames3.Add(new Frame(new Vector3(part2Object.transform.position.x, part2YPos, part2Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        Frame part3InitialFrame = new Frame(new Vector3(part3Object.transform.position.x, -1.38f, part3Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object);
        frames4 = new List<Frame>();
        frames4.Add(part3InitialFrame);
        frames4.Add(part3InitialFrame);
        float part3YPos = part3InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part3YPos += 0.6f / 20f;
            frames4.Add(new Frame(new Vector3(part3Object.transform.position.x, part3YPos, part3Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        for (int i = 0; i < 40; i++) {
            part3YPos += 0.39f / 20f;
            frames4.Add(new Frame(new Vector3(part3Object.transform.position.x, part3YPos, part3Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        for (int i = 0; i < 120; i++) {
            frames4.Add(new Frame(new Vector3(part3Object.transform.position.x, part3YPos, part3Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        Frame part4InitialFrame = new Frame(new Vector3(part4Object.transform.position.x, -1.77f, part4Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object);
        frames5 = new List<Frame>();
        frames5.Add(part4InitialFrame);
        frames5.Add(part4InitialFrame);
        float part4YPos = part4InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part4YPos += 0.6f / 20f;
            frames5.Add(new Frame(new Vector3(part4Object.transform.position.x, part4YPos, part4Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        for (int i = 0; i < 60; i++) {
            part4YPos += 0.39f / 20f;
            frames5.Add(new Frame(new Vector3(part4Object.transform.position.x, part4YPos, part4Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        for (int i = 0; i < 100; i++) {
            frames5.Add(new Frame(new Vector3(part4Object.transform.position.x, part4YPos, part4Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        Frame part5InitialFrame = new Frame(new Vector3(part5Object.transform.position.x, -2.16f, part5Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object);
        frames6 = new List<Frame>();
        frames6.Add(part5InitialFrame);
        frames6.Add(part5InitialFrame);
        float part5YPos = part5InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part5YPos += 0.6f / 20f;
            frames6.Add(new Frame(new Vector3(part5Object.transform.position.x, part5YPos, part5Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        for (int i = 0; i < 80; i++) {
            part5YPos += 0.39f / 20f;
            frames6.Add(new Frame(new Vector3(part5Object.transform.position.x, part5YPos, part5Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        for (int i = 0; i < 80; i++) {
            frames6.Add(new Frame(new Vector3(part5Object.transform.position.x, part5YPos, part5Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        Frame part6InitialFrame = new Frame(new Vector3(part6Object.transform.position.x, -2.55f, part6Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part6Object);
        frames7 = new List<Frame>();
        frames7.Add(part6InitialFrame);
        frames7.Add(part6InitialFrame);
        float part6YPos = part6InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part6YPos += 0.6f / 20f;
            frames7.Add(new Frame(new Vector3(part6Object.transform.position.x, part6YPos, part6Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part6Object));
        }
        for (int i = 0; i < 100; i++) {
            part6YPos += 0.39f / 20f;
            frames7.Add(new Frame(new Vector3(part6Object.transform.position.x, part6YPos, part6Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part6Object));
        }
        for (int i = 0; i < 60; i++) {
            frames7.Add(new Frame(new Vector3(part6Object.transform.position.x, part6YPos, part6Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part6Object));
        }
        Frame part7InitialFrame = new Frame(new Vector3(part7Object.transform.position.x, -2.94f, part7Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part7Object);
        frames8 = new List<Frame>();
        frames8.Add(part7InitialFrame);
        frames8.Add(part7InitialFrame);
        float part7YPos = part7InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part7YPos += 0.6f / 20f;
            frames8.Add(new Frame(new Vector3(part7Object.transform.position.x, part7YPos, part7Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part7Object));
        }
        for (int i = 0; i < 120; i++) {
            part7YPos += 0.39f / 20f;
            frames8.Add(new Frame(new Vector3(part7Object.transform.position.x, part7YPos, part7Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part7Object));
        }
        for (int i = 0; i < 40; i++) {
            frames8.Add(new Frame(new Vector3(part7Object.transform.position.x, part7YPos, part7Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part7Object));
        }
        animator = new FrameAnim(frames);
        animator2 = new FrameAnim(frames2);
        animator3 = new FrameAnim(frames3);
        animator4 = new FrameAnim(frames4);
        animator5 = new FrameAnim(frames5);
        animator6 = new FrameAnim(frames6);
        animator7 = new FrameAnim(frames7);
        animator8 = new FrameAnim(frames8);
    }

    public void ResetAnimation(Vector3 newPos) {
        Transform stemTransform = gameObject.transform.Find("DeceBalus_Tree_Stem");
        Transform part1Transform = gameObject.transform.Find("DeceBalus_Tree_Part1");
        Transform part2Transform = gameObject.transform.Find("DeceBalus_Tree_Part2");
        Transform part3Transform = gameObject.transform.Find("DeceBalus_Tree_Part3");
        Transform part4Transform = gameObject.transform.Find("DeceBalus_Tree_Part4");
        Transform part5Transform = gameObject.transform.Find("DeceBalus_Tree_Part5");
        Transform part6Transform = gameObject.transform.Find("DeceBalus_Tree_Part6");
        Transform part7Transform = gameObject.transform.Find("DeceBalus_Tree_Part7");
        GameObject stemObject = stemTransform.gameObject;
        GameObject part1Object = part1Transform.gameObject;
        GameObject part2Object = part2Transform.gameObject;
        GameObject part3Object = part3Transform.gameObject;
        GameObject part4Object = part4Transform.gameObject;
        GameObject part5Object = part5Transform.gameObject;
        GameObject part6Object = part6Transform.gameObject;
        GameObject part7Object = part7Transform.gameObject;
        Frame stemInitialFrame = new Frame(new Vector3(newPos.x, -0.6f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject);
        frames = new List<Frame>();
        frames.Add(stemInitialFrame);
        frames.Add(stemInitialFrame);
        float YPos = stemInitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            YPos += 0.6f / 20f;
            frames.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
        }
        for (int i = 0; i < 160; i++) {
            frames.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
        }
        Frame part1InitialFrame = new Frame(new Vector3(newPos.x, -0.6f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part1Object);
        frames2 = new List<Frame>();
        frames2.Add(part1InitialFrame);
        frames2.Add(part1InitialFrame);
        float part1YPos = part1InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part1YPos += 0.6f / 20f;
            frames2.Add(new Frame(new Vector3(newPos.x, part1YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part1Object));
        }
        for (int i = 0; i < 160; i++) {
            frames2.Add(new Frame(new Vector3(newPos.x, part1YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part1Object));
        }
        Frame part2InitialFrame = new Frame(new Vector3(newPos.x, -0.99f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object);
        frames3 = new List<Frame>();
        frames3.Add(part2InitialFrame);
        frames3.Add(part2InitialFrame);
        float part2YPos = part2InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part2YPos += 0.6f / 20f;
            frames3.Add(new Frame(new Vector3(newPos.x, part2YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        for (int i = 0; i < 20; i++) {
            part2YPos += 0.39f / 20f;
            frames3.Add(new Frame(new Vector3(newPos.x, part2YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        for (int i = 0; i < 140; i++) {
            frames3.Add(new Frame(new Vector3(newPos.x, part2YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part2Object));
        }
        Frame part3InitialFrame = new Frame(new Vector3(newPos.x, -1.38f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object);
        frames4 = new List<Frame>();
        frames4.Add(part3InitialFrame);
        frames4.Add(part3InitialFrame);
        float part3YPos = part3InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part3YPos += 0.6f / 20f;
            frames4.Add(new Frame(new Vector3(newPos.x, part3YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        for (int i = 0; i < 40; i++) {
            part3YPos += 0.39f / 20f;
            frames4.Add(new Frame(new Vector3(newPos.x, part3YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        for (int i = 0; i < 120; i++) {
            frames4.Add(new Frame(new Vector3(newPos.x, part3YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part3Object));
        }
        Frame part4InitialFrame = new Frame(new Vector3(newPos.x, -1.77f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object);
        frames5 = new List<Frame>();
        frames5.Add(part4InitialFrame);
        frames5.Add(part4InitialFrame);
        float part4YPos = part4InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part4YPos += 0.6f / 20f;
            frames5.Add(new Frame(new Vector3(newPos.x, part4YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        for (int i = 0; i < 60; i++) {
            part4YPos += 0.39f / 20f;
            frames5.Add(new Frame(new Vector3(newPos.x, part4YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        for (int i = 0; i < 100; i++) {
            frames5.Add(new Frame(new Vector3(newPos.x, part4YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part4Object));
        }
        Frame part5InitialFrame = new Frame(new Vector3(newPos.x, -2.16f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object);
        frames6 = new List<Frame>();
        frames6.Add(part5InitialFrame);
        frames6.Add(part5InitialFrame);
        float part5YPos = part5InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part5YPos += 0.6f / 20f;
            frames6.Add(new Frame(new Vector3(newPos.x, part5YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        for (int i = 0; i < 80; i++) {
            part5YPos += 0.39f / 20f;
            frames6.Add(new Frame(new Vector3(newPos.x, part5YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        for (int i = 0; i < 80; i++) {
            frames6.Add(new Frame(new Vector3(newPos.x, part5YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part5Object));
        }
        Frame part6InitialFrame = new Frame(new Vector3(newPos.x, -2.55f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part6Object);
        frames7 = new List<Frame>();
        frames7.Add(part6InitialFrame);
        frames7.Add(part6InitialFrame);
        float part6YPos = part6InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part6YPos += 0.6f / 20f;
            frames7.Add(new Frame(new Vector3(newPos.x, part6YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part6Object));
        }
        for (int i = 0; i < 100; i++) {
            part6YPos += 0.39f / 20f;
            frames7.Add(new Frame(new Vector3(newPos.x, part6YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part6Object));
        }
        for (int i = 0; i < 60; i++) {
            frames7.Add(new Frame(new Vector3(newPos.x, part6YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part6Object));
        }
        Frame part7InitialFrame = new Frame(new Vector3(newPos.x, -2.94f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part7Object);
        frames8 = new List<Frame>();
        frames8.Add(part7InitialFrame);
        frames8.Add(part7InitialFrame);
        float part7YPos = part7InitialFrame.position.y;
        for (int i = 0; i < 20; i++) {
            part7YPos += 0.6f / 20f;
            frames8.Add(new Frame(new Vector3(newPos.x, part7YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part7Object));
        }
        for (int i = 0; i < 120; i++) {
            part7YPos += 0.39f / 20f;
            frames8.Add(new Frame(new Vector3(newPos.x, part7YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part7Object));
        }
        for (int i = 0; i < 40; i++) {
            frames8.Add(new Frame(new Vector3(newPos.x, part7YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part7Object));
        }
        animator = new FrameAnim(frames);
        animator2 = new FrameAnim(frames2);
        animator3 = new FrameAnim(frames3);
        animator4 = new FrameAnim(frames4);
        animator5 = new FrameAnim(frames5);
        animator6 = new FrameAnim(frames6);
        animator7 = new FrameAnim(frames7);
        animator8 = new FrameAnim(frames8);
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
            animator6.SetFrame(1, 0f);
            animator7.SetFrame(1, 0f);
            animator8.SetFrame(1, 0f);
        }
        else if (currentFrame >= 180)
        {
            animator.SetFrame(180, 0.99f);
            animator2.SetFrame(180, 0.99f);
            animator3.SetFrame(180, 0.99f);
            animator4.SetFrame(180, 0.99f);
            animator5.SetFrame(180, 0.99f);
            animator6.SetFrame(180, 0.99f);
            animator7.SetFrame(180, 0.99f);
            animator8.SetFrame(180, 0.99f);
        }
        else
        {
            animator.SetFrame(currentFrame + 1, t);
            animator2.SetFrame(currentFrame + 1, t);
            animator3.SetFrame(currentFrame + 1, t);
            animator4.SetFrame(currentFrame + 1, t);
            animator5.SetFrame(currentFrame + 1, t);
            animator6.SetFrame(currentFrame + 1, t);
            animator7.SetFrame(currentFrame + 1, t);
            animator8.SetFrame(currentFrame + 1, t);
        }
    }
}
