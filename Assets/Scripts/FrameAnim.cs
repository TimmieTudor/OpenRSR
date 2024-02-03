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

    public void SetFrame(int index, float t)
    {
        this.currentFrame = index;
        name.transform.position = Vector3.Lerp(frames[currentFrame - 1].position, frames[currentFrame].position, t);
        name.transform.localScale = Vector3.Lerp(frames[currentFrame - 1].scale, frames[currentFrame].scale, t);
        name.transform.rotation = Quaternion.Lerp(frames[currentFrame - 1].rotation, frames[currentFrame].rotation, t);
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