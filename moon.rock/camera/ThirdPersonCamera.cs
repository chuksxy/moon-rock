using System;
using UnityEngine;
using Avatar = moon.rock.avatar.Avatar;

namespace moon.rock.camera {

      public class ThirdPersonCamera : MonoBehaviour {

            public Camera cam;

            private Tuple<Avatar, Vector3> _last;


            private void Start() {
                  if (cam != null) {
                        return;
                  }

                  Debug.LogWarning("camera is null, re-creating it");

                  cam = new Camera();
            }


            private void LateUpdate() {
                  void TrackTarget() {
                        if (_last == null) {
                              Debug.LogWarning("cannot track target because target is missing");
                              return;
                        }

                        if (cam == null) {
                              Debug.LogWarning("cannot track target because target is missing");
                              return;
                        }

                        var target         = _last.Item1;
                        var directionMoved = _last.Item2;

                        // Stub implementation 
                        cam.transform.LookAt(target.transform.position, directionMoved);
                  }

                  TrackTarget();
            }


            public void Track(Avatar target) {
                  target.Moved += (avatar, direction) => { _last = new Tuple<Avatar, Vector3>(avatar, direction); };
            }

      }

}