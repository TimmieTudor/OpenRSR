using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FloaterAnim : MonoBehaviour
{
    public GameObject balus;
    public float animationOffset = 13.9f;
    public FrameAnim animator;
    public FrameAnim animator2;
    private List<Frame> frames;
    private List<Frame> frames2;
    public GameObject part1Object;
    public GameObject part2Object;
    public GameObject part3Object;
    private int currentFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        Transform part1Transform = gameObject.transform.Find("DeceBalus_Floater_Part1");
        ResetAnimation(part1Transform.position);
    }

    public void ResetAnimation(Vector3 newPos) {
        Transform part1Transform = gameObject.transform.Find("DeceBalus_Floater_Part1");
        Transform part2Transform = gameObject.transform.Find("DeceBalus_Floater_Part2");
        Transform part3Transform = gameObject.transform.Find("DeceBalus_Floater_Part3");
        part1Object = part1Transform.gameObject;
        part2Object = part2Transform.gameObject;
        part3Object = part3Transform.gameObject;
        
        Frame part1InitialFrame = new Frame(new Vector3(newPos.x, 0f, newPos.z), Quaternion.identity, new Vector3(1f, 1f, 1f), part1Object);
        Frame part2InitialFrame = new Frame(new Vector3(newPos.x, 0f, newPos.z), Quaternion.identity, new Vector3(1f, 1f, 1f), part2Object);
        frames = new List<Frame>();
        frames.Add(part1InitialFrame);
        frames.Add(part1InitialFrame);
        frames2 = new List<Frame>();
        frames2.Add(part2InitialFrame);
        frames2.Add(part2InitialFrame);
        float rotx = 0f;
        float posY = 0f;
        for (int i = 0; i < 80; i++) {
            rotx += 180f / 80f;
            posY += 2.7f / 80f;
            frames.Add(new Frame(new Vector3(newPos.x, posY, newPos.z), Quaternion.Euler(rotx, 0f, 0f), new Vector3(1f, 1f, 1f), part1Object));
            frames2.Add(new Frame(new Vector3(newPos.x, posY, newPos.z), Quaternion.Euler(rotx, 0f, 0f), new Vector3(1f, 1f, 1f), part2Object));
        }
        for (int i = 0; i < 100; i++) {
            frames.Add(new Frame(new Vector3(newPos.x, posY, newPos.z), Quaternion.Euler(rotx, 0f, 0f), new Vector3(1f, 1f, 1f), part1Object));
            frames2.Add(new Frame(new Vector3(newPos.x, posY, newPos.z), Quaternion.Euler(rotx, 0f, 0f), new Vector3(1f, 1f, 1f), part2Object));
        }
        animator = new FrameAnim(frames);
        animator2 = new FrameAnim(frames2);
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
            part1Object.SetActive(true);
            part2Object.SetActive(false);
            part3Object.SetActive(false);
        }
        else if (currentFrame >= 90 && currentFrame < 180) {
            animator.SetFrame(currentFrame + 1, t);
            animator2.SetFrame(currentFrame + 1, t);
            part1Object.SetActive(false);
            part2Object.SetActive(true);
            part3Object.SetActive(true);
        }
        else if (currentFrame >= 180)
        {
            animator.SetFrame(1, 0f);
            animator2.SetFrame(1, 0f);
            part1Object.SetActive(true);
            part2Object.SetActive(false);
            part3Object.SetActive(false);
        }
        else
        {
            animator.SetFrame(currentFrame + 1, t);
            animator2.SetFrame(currentFrame + 1, t);
        }
    }
}
