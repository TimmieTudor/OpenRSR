using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnim
{
    public List<Frame> frames;
    public int currentFrame = 0;
    public GameObject name;

    public FrameAnim(List<Frame> frames)
    {
        this.frames = frames;
        this.currentFrame = 0;
        this.name = frames[0].name;
    }

    public void SetFrame(int index)
    {
        this.currentFrame = index;
        name.transform.position = Vector3.Lerp(frames[currentFrame].position, frames[(currentFrame + 1)].position, 0.5f);
        name.transform.localScale = Vector3.Lerp(frames[currentFrame].scale, frames[(currentFrame + 1)].scale, 0.5f);
        name.transform.rotation = Quaternion.Lerp(frames[currentFrame].rotation, frames[(currentFrame + 1)].rotation, 0.5f);
        currentFrame++;
    }
}

public class Frame
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public GameObject name;
    
    public Frame(Vector3 position, Quaternion rotation, Vector3 scale, GameObject name)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.name = name;
    }
}