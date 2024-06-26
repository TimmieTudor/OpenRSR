using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OpenRSR {
    namespace Gameplay {
        public class Ball : MonoBehaviour {
            public float speed = 6.0f;
            public Rigidbody rb;
            public SphereMovement sphm;

            void Start() {
                rb = GetComponent<Rigidbody>();
                sphm = GetComponent<SphereMovement>();
                speed = sphm.speed;
            }

            public void Jump(float distance, Vector3 startPosition) {
                sphm.Jump(distance, startPosition);
            }
        }
    }
}