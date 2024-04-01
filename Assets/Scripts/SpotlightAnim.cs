using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpotlightAnim : MonoBehaviour
{
    public GameObject balus;
    public float animationOffset = 11f;
    public FrameAnim animator;
    private List<Frame> frames;
    private int currentFrame = 0;
    private float RotationFunction(float x) {
        return Mathf.Cos(Mathf.PI * x) * 30f;
    }
    // Start is called before the first frame update
    void Start()
    {
        Transform laserTransform = gameObject.transform.Find("DeceBalus_Spotlight_Laser");
        GameObject laserPart = laserTransform.gameObject;
        laserPart.transform.position = new Vector3(laserPart.transform.position.x, 0f, laserPart.transform.position.z);
        frames = new List<Frame>();
        Frame initialLaserFrame = new Frame(new Vector3(laserPart.transform.position.x, 0f, laserPart.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 30f)), new Vector3(1f, 1f, 1f), laserTransform.gameObject);
        frames.Add(initialLaserFrame);
        frames.Add(initialLaserFrame);
        float y = initialLaserFrame.position.y;
        float rotZ = initialLaserFrame.rotation.eulerAngles.z;
        for (int i = 0; i < 120; i++) {
            rotZ = RotationFunction((float)i / 60f);
            frames.Add(new Frame(new Vector3(laserPart.transform.position.x, y, laserPart.transform.position.z), Quaternion.Euler(0f, 0f, rotZ), new Vector3(1f, 1f, 1f), laserTransform.gameObject));
        }
        animator = new FrameAnim(frames);
    }

    public void ResetAnimation(Vector3 newPos) {
        Transform laserTransform = gameObject.transform.Find("DeceBalus_Spotlight_Laser");
        GameObject laserPart = laserTransform.gameObject;
        laserPart.transform.position = new Vector3(newPos.x, 0f, newPos.z);
        frames = new List<Frame>();
        Frame initialLaserFrame = new Frame(new Vector3(newPos.x, 0f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 30f)), new Vector3(1f, 1f, 1f), laserTransform.gameObject);
        frames.Add(initialLaserFrame);
        frames.Add(initialLaserFrame);
        float y = initialLaserFrame.position.y;
        float rotZ = initialLaserFrame.rotation.eulerAngles.z;
        for (int i = 0; i < 120; i++) {
            rotZ = RotationFunction((float)i / 60f);
            frames.Add(new Frame(new Vector3(newPos.x, y, newPos.z), Quaternion.Euler(0f, 0f, rotZ), new Vector3(1f, 1f, 1f), laserTransform.gameObject));
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
