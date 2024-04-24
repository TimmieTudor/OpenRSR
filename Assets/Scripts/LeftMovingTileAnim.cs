using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMovingTileAnim : MonoBehaviour
{
    public GameObject balus;
    public float xOffset = 0f;
    private GameObject baseObject;
    private GameObject m_Riser;
    
    // Start is called before the first frame update
    void Start()
    {
        baseObject = gameObject.transform.Find("DeceBalus_Normal_Tile_Base").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float curFrame2 = (balus.transform.position.z - gameObject.transform.position.z) / 2f;
        baseObject.transform.position = new Vector3(-Mathf.Sin(curFrame2) * 2f + xOffset, 0f, baseObject.transform.position.z);
        if (m_Riser == null) {
            GameObject[] risers = GameObject.FindGameObjectsWithTag("Riser");
            foreach (GameObject riser in risers) {
                if (riser == null) continue;
                if (riser.transform.position.z == transform.position.z && riser.transform.position.x == transform.position.x) {
                    m_Riser = riser;
                    break;
                }
            }
        }
        if (m_Riser != null) {
            if (m_Riser.TryGetComponent<RiserAnim>(out RiserAnim riserAnim)) {
                foreach (Frame frame in riserAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in riserAnim.animator2.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<CrusherAnim>(out CrusherAnim crusherAnim)) {
                foreach (Frame frame in crusherAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in crusherAnim.animator2.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<LeftHammerAnim>(out LeftHammerAnim hammerAnim)) {
                foreach (Frame frame in hammerAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in hammerAnim.animator2.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<RightHammerAnim>(out RightHammerAnim hammerAnim2)) {
                foreach (Frame frame in hammerAnim2.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in hammerAnim2.animator2.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<LeftHammerLargeAnim>(out LeftHammerLargeAnim hammerLargeAnim)) {
                foreach (Frame frame in hammerLargeAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<RightHammerLargeAnim>(out RightHammerLargeAnim hammerLargeAnim2)) {
                foreach (Frame frame in hammerLargeAnim2.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<LargeTreeAnim>(out LargeTreeAnim largeTreeAnim)) {
                foreach (Frame frame in largeTreeAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in largeTreeAnim.animator2.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in largeTreeAnim.animator3.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in largeTreeAnim.animator4.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in largeTreeAnim.animator5.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in largeTreeAnim.animator6.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in largeTreeAnim.animator7.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in largeTreeAnim.animator8.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<MediumTreeAnim>(out MediumTreeAnim mediumTreeAnim)) {
                foreach (Frame frame in mediumTreeAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in mediumTreeAnim.animator2.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in mediumTreeAnim.animator3.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in mediumTreeAnim.animator4.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in mediumTreeAnim.animator5.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<SmallTreeAnim>(out SmallTreeAnim smallTreeAnim)) {
                foreach (Frame frame in smallTreeAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in smallTreeAnim.animator2.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in smallTreeAnim.animator3.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in smallTreeAnim.animator4.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in smallTreeAnim.animator5.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in smallTreeAnim.animator6.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in smallTreeAnim.animator7.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<LaserAnim>(out LaserAnim laserAnim)) {
                foreach (Frame frame in laserAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in laserAnim.animator2.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in laserAnim.animator3.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                GameObject laserBaseObject = m_Riser.transform.Find("DeceBalus_Laser_Base").gameObject;
                laserBaseObject.transform.position = new Vector3(baseObject.transform.position.x, laserBaseObject.transform.position.y, baseObject.transform.position.z);
            } else if (m_Riser.TryGetComponent<FloaterAnim>(out FloaterAnim floaterAnim)) {
                foreach (Frame frame in floaterAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                foreach (Frame frame in floaterAnim.animator2.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
            } else if (m_Riser.TryGetComponent<SpotlightAnim>(out SpotlightAnim spotlightAnim)) {
                foreach (Frame frame in spotlightAnim.animator.frames) {
                    frame.position = new Vector3(baseObject.transform.position.x, frame.position.y, frame.position.z);
                }
                m_Riser.transform.position = new Vector3(baseObject.transform.position.x, m_Riser.transform.position.y, baseObject.transform.position.z);
            } else if (m_Riser.TryGetComponent<SoundPlayer>(out SoundPlayer soundPlayer)) {
                GameObject gem1stChild = m_Riser.transform.GetChild(0).gameObject;
                gem1stChild.transform.position = new Vector3(baseObject.transform.position.x, gem1stChild.transform.position.y, baseObject.transform.position.z);
            }
        }
    }
}
