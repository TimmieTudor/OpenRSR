using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SmallTreeAnim : MonoBehaviour
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
    private List<Frame> frames;
    private List<Frame> frames2;
    private List<Frame> frames3;
    private List<Frame> frames4;
    private List<Frame> frames5;
    private List<Frame> frames6;
    private List<Frame> frames7;
    private int currentFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        Transform stemTransform = gameObject.transform.Find("DeceBalus_Tree_Stem");
        Transform part8Transform = gameObject.transform.Find("DeceBalus_Tree_Part8");
        Transform part9Transform = gameObject.transform.Find("DeceBalus_Tree_Part9");
        Transform part10Transform = gameObject.transform.Find("DeceBalus_Tree_Part10");
        Transform part11Transform = gameObject.transform.Find("DeceBalus_Tree_Part11");
        Transform part12Transform = gameObject.transform.Find("DeceBalus_Tree_Part12");
        Transform part13Transform = gameObject.transform.Find("DeceBalus_Tree_Part13");
        GameObject stemObject = stemTransform.gameObject;
        GameObject part8Object = part8Transform.gameObject;
        GameObject part9Object = part9Transform.gameObject;
        GameObject part10Object = part10Transform.gameObject;
        GameObject part11Object = part11Transform.gameObject;
        GameObject part12Object = part12Transform.gameObject;
        GameObject part13Object = part13Transform.gameObject;
        Frame stemInitialFrame = new Frame(new Vector3(stemObject.transform.position.x, -0.9f, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject);
        frames = new List<Frame>();
        frames.Add(stemInitialFrame);
        frames.Add(stemInitialFrame);
        Frame part8InitialFrame = new Frame(new Vector3(part8Object.transform.position.x, -0.9f, part8Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object);
        frames2 = new List<Frame>();
        frames2.Add(part8InitialFrame);
        frames2.Add(part8InitialFrame);
        Frame part9InitialFrame = new Frame(new Vector3(part9Object.transform.position.x + 0.05f, -0.9f, part9Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object);
        frames3 = new List<Frame>();
        frames3.Add(part9InitialFrame);
        frames3.Add(part9InitialFrame);
        Frame part10InitialFrame = new Frame(new Vector3(part10Object.transform.position.x, -0.95f, part10Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object);
        frames4 = new List<Frame>();
        frames4.Add(part10InitialFrame);
        frames4.Add(part10InitialFrame);
        Frame part11InitialFrame = new Frame(new Vector3(part11Object.transform.position.x - 0.05f, -0.9f, part11Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object);
        frames5 = new List<Frame>();
        frames5.Add(part11InitialFrame);
        frames5.Add(part11InitialFrame);
        Frame part12InitialFrame = new Frame(new Vector3(part12Object.transform.position.x, -0.9f, part12Object.transform.position.z + 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object);
        frames6 = new List<Frame>();
        frames6.Add(part12InitialFrame);
        frames6.Add(part12InitialFrame);
        Frame part13InitialFrame = new Frame(new Vector3(part13Object.transform.position.x, -0.9f, part13Object.transform.position.z - 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object);
        frames7 = new List<Frame>();
        frames7.Add(part13InitialFrame);
        frames7.Add(part13InitialFrame);
        float YPos = stemInitialFrame.position.y;
        for (int i = 0; i < 10; i++) {
            frames.Add(new Frame(new Vector3(stemObject.transform.position.x, YPos, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(part8Object.transform.position.x, YPos, part8Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(part9Object.transform.position.x + 0.05f, YPos, part9Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(part10Object.transform.position.x, YPos - 0.05f, part10Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(part11Object.transform.position.x - 0.05f, YPos, part11Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(part12Object.transform.position.x, YPos, part12Object.transform.position.z + 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(part13Object.transform.position.x, YPos, part13Object.transform.position.z - 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        for (int i = 0; i < 6; i++) {
            YPos += 0.9f / 6f;
            frames.Add(new Frame(new Vector3(stemObject.transform.position.x, YPos, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(part8Object.transform.position.x, YPos, part8Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(part9Object.transform.position.x + 0.05f, YPos, part9Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(part10Object.transform.position.x, YPos - 0.05f, part10Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(part11Object.transform.position.x - 0.05f, YPos, part11Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(part12Object.transform.position.x, YPos, part12Object.transform.position.z + 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(part13Object.transform.position.x, YPos, part13Object.transform.position.z - 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        for (int i = 0; i < 2; i++) {
            frames.Add(new Frame(new Vector3(stemObject.transform.position.x, YPos, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(part8Object.transform.position.x, YPos, part8Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(part9Object.transform.position.x + 0.05f, YPos, part9Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(part10Object.transform.position.x, YPos - 0.05f, part10Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(part11Object.transform.position.x - 0.05f, YPos, part11Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(part12Object.transform.position.x, YPos, part12Object.transform.position.z + 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(part13Object.transform.position.x, YPos, part13Object.transform.position.z - 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        float xzOffset = 0.05f;
        for (int i = 0; i < 2; i++) {
            xzOffset -= 0.05f / 2f;
            frames.Add(new Frame(new Vector3(stemObject.transform.position.x, YPos, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(part8Object.transform.position.x, YPos, part8Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(part9Object.transform.position.x + xzOffset, YPos, part9Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(part10Object.transform.position.x, YPos - xzOffset, part10Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(part11Object.transform.position.x - xzOffset, YPos, part11Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(part12Object.transform.position.x, YPos, part12Object.transform.position.z + xzOffset), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(part13Object.transform.position.x, YPos, part13Object.transform.position.z - xzOffset), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        for (int i = 0; i < 40; i++) {
            frames.Add(new Frame(new Vector3(stemObject.transform.position.x, YPos, stemObject.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(part8Object.transform.position.x, YPos, part8Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(part9Object.transform.position.x, YPos, part9Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(part10Object.transform.position.x, YPos, part10Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(part11Object.transform.position.x, YPos, part11Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(part12Object.transform.position.x, YPos, part12Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(part13Object.transform.position.x, YPos, part13Object.transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        animator = new FrameAnim(frames);
        animator2 = new FrameAnim(frames2);
        animator3 = new FrameAnim(frames3);
        animator4 = new FrameAnim(frames4);
        animator5 = new FrameAnim(frames5);
        animator6 = new FrameAnim(frames6);
        animator7 = new FrameAnim(frames7);
    }

    public void ResetAnimation(Vector3 newPos) {
        animator = null;
        animator2 = null;
        animator3 = null;
        animator4 = null;
        animator5 = null;
        animator6 = null;
        animator7 = null;
        Transform stemTransform = gameObject.transform.Find("DeceBalus_Tree_Stem");
        Transform part8Transform = gameObject.transform.Find("DeceBalus_Tree_Part8");
        Transform part9Transform = gameObject.transform.Find("DeceBalus_Tree_Part9");
        Transform part10Transform = gameObject.transform.Find("DeceBalus_Tree_Part10");
        Transform part11Transform = gameObject.transform.Find("DeceBalus_Tree_Part11");
        Transform part12Transform = gameObject.transform.Find("DeceBalus_Tree_Part12");
        Transform part13Transform = gameObject.transform.Find("DeceBalus_Tree_Part13");
        GameObject stemObject = stemTransform.gameObject;
        GameObject part8Object = part8Transform.gameObject;
        GameObject part9Object = part9Transform.gameObject;
        GameObject part10Object = part10Transform.gameObject;
        GameObject part11Object = part11Transform.gameObject;
        GameObject part12Object = part12Transform.gameObject;
        GameObject part13Object = part13Transform.gameObject;
        Frame stemInitialFrame = new Frame(new Vector3(newPos.x, -0.9f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject);
        frames = new List<Frame>();
        frames.Add(stemInitialFrame);
        frames.Add(stemInitialFrame);
        Frame part8InitialFrame = new Frame(new Vector3(newPos.x, -0.9f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object);
        frames2 = new List<Frame>();
        frames2.Add(part8InitialFrame);
        frames2.Add(part8InitialFrame);
        Frame part9InitialFrame = new Frame(new Vector3(newPos.x + 0.05f, -0.9f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object);
        frames3 = new List<Frame>();
        frames3.Add(part9InitialFrame);
        frames3.Add(part9InitialFrame);
        Frame part10InitialFrame = new Frame(new Vector3(newPos.x, -0.95f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object);
        frames4 = new List<Frame>();
        frames4.Add(part10InitialFrame);
        frames4.Add(part10InitialFrame);
        Frame part11InitialFrame = new Frame(new Vector3(newPos.x - 0.05f, -0.9f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object);
        frames5 = new List<Frame>();
        frames5.Add(part11InitialFrame);
        frames5.Add(part11InitialFrame);
        Frame part12InitialFrame = new Frame(new Vector3(newPos.x, -0.9f, newPos.z + 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object);
        frames6 = new List<Frame>();
        frames6.Add(part12InitialFrame);
        frames6.Add(part12InitialFrame);
        Frame part13InitialFrame = new Frame(new Vector3(newPos.x, -0.9f, newPos.z - 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object);
        frames7 = new List<Frame>();
        frames7.Add(part13InitialFrame);
        frames7.Add(part13InitialFrame);
        float YPos = stemInitialFrame.position.y;
        for (int i = 0; i < 10; i++) {
            frames.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(newPos.x + 0.05f, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(newPos.x, YPos - 0.05f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(newPos.x - 0.05f, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z + 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z - 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        for (int i = 0; i < 6; i++) {
            YPos += 0.9f / 6f;
            frames.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(newPos.x + 0.05f, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(newPos.x, YPos - 0.05f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(newPos.x - 0.05f, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z + 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z - 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        for (int i = 0; i < 2; i++) {
            frames.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(newPos.x + 0.05f, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(newPos.x, YPos - 0.05f, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(newPos.x - 0.05f, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z + 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z - 0.05f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        float xzOffset = 0.05f;
        for (int i = 0; i < 2; i++) {
            xzOffset -= 0.05f / 2f;
            frames.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(newPos.x + xzOffset, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(newPos.x, YPos - xzOffset, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(newPos.x - xzOffset, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z + xzOffset), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z - xzOffset), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        for (int i = 0; i < 40; i++) {
            frames.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), stemObject));
            frames2.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part8Object));
            frames3.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part9Object));
            frames4.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part10Object));
            frames5.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part11Object));
            frames6.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part12Object));
            frames7.Add(new Frame(new Vector3(newPos.x, YPos, newPos.z), Quaternion.Euler(new Vector3(0f, 0f, 0f)), new Vector3(1f, 1f, 1f), part13Object));
        }
        animator = new FrameAnim(frames);
        animator2 = new FrameAnim(frames2);
        animator3 = new FrameAnim(frames3);
        animator4 = new FrameAnim(frames4);
        animator5 = new FrameAnim(frames5);
        animator6 = new FrameAnim(frames6);
        animator7 = new FrameAnim(frames7);
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
        }
        else if (currentFrame >= 60)
        {
            animator.SetFrame(60, 0.99f);
            animator2.SetFrame(60, 0.99f);
            animator3.SetFrame(60, 0.99f);
            animator4.SetFrame(60, 0.99f);
            animator5.SetFrame(60, 0.99f);
            animator6.SetFrame(60, 0.99f);
            animator7.SetFrame(60, 0.99f);
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
        }
    }
}
