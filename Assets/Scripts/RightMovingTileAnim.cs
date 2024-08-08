using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenRSR.Animation;

public class RightMovingTileAnim : MonoBehaviour
{
    public GameObject balus;
    public float xOffset = 0f;
    public float rightMostXOffset = 0f;
    private GameObject baseObject;
    private GameObject m_Riser;
    private GameObject rotorObject;
    
    // Start is called before the first frame update
    void Start()
    {
        baseObject = gameObject.transform.Find("DeceBalus_Normal_Tile_Base").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float curFrame2 = (balus.transform.position.z - gameObject.transform.position.z) / 2f;
        if (rightMostXOffset == 0f) {
            baseObject.transform.position = new Vector3(Mathf.Sin(curFrame2) * 2f + xOffset, 0f, baseObject.transform.position.z);
        } else if (rightMostXOffset == -1f) {
            baseObject.transform.position = new Vector3(Mathf.Sin(curFrame2 + Mathf.PI / 6f) * 2f + xOffset - 1f, 0f, baseObject.transform.position.z);
        } else if (rightMostXOffset == -2f) {
            baseObject.transform.position = new Vector3(Mathf.Sin(curFrame2 + Mathf.PI / 2f) * 2f + xOffset - 2f, 0f, baseObject.transform.position.z);
        } else if (rightMostXOffset == 2f) {
            baseObject.transform.position = new Vector3(Mathf.Sin(curFrame2 - Mathf.PI / 2f) * 2f + xOffset + 2f, 0f, baseObject.transform.position.z);
        } else if (rightMostXOffset == 1f) {
            baseObject.transform.position = new Vector3(Mathf.Sin(curFrame2 - Mathf.PI / 6f) * 2f + xOffset + 1f, 0f, baseObject.transform.position.z);
        }
        if (m_Riser == null) {
            GameObject[] risers = GameObject.FindGameObjectsWithTag("Riser");
            foreach (GameObject riser in risers) {
                if (riser == null) continue;
                if (riser.transform.position.z == transform.position.z && riser.transform.position.x == transform.position.x) {
                    m_Riser = riser;
                    if (m_Riser.transform.childCount > 2) {
                        rotorObject = m_Riser.transform.GetChild(2).gameObject;
                    }
                    break;
                }
            }
        }
        if (m_Riser != null) {
            if (m_Riser.TryGetComponent<BaseAnim>(out BaseAnim baseAnim)) {
                if (rotorObject != null) {
                    rotorObject.transform.position = new Vector3(baseObject.transform.position.x, rotorObject.transform.position.y, rotorObject.transform.position.z);
                }
                foreach (FrameAnim animator in baseAnim.animators) {
                    foreach (Frame frame in animator.frames) {
                        frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                    }
                }
            } else if (m_Riser.TryGetComponent<SoundPlayer>(out SoundPlayer soundPlayer)) {
                GameObject gemBase = m_Riser.transform.Find("DeceBalus_Gem_Base").gameObject;
                gemBase.transform.position = new Vector3(baseObject.transform.position.x, gemBase.transform.position.y, gemBase.transform.position.z);
            }
        }
    }
}
