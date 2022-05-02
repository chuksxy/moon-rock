using System;
using Cinemachine;
using UnityEngine;
using Avatar = moon.rock.avatar.Avatar;

namespace moon.rock.camera {

      public class ThirdPersonCamera : MonoBehaviour {

            public CinemachineFreeLook thirdPersonCam;

            // Last recorded target and the direction it moved in.
            private Tuple<Avatar, Vector3> _last;


            private void Start() {
                  if (thirdPersonCam != null) {
                        return;
                  }

                  Debug.LogWarning("camera is null, re-creating it");

                  thirdPersonCam = new GameObject().AddComponent<CinemachineFreeLook>();
            }


            private void LateUpdate() {
                  // Track Target at the event of the frame update cycle
                  void TrackTarget() {
                        if (_last == null) {
                              Debug.LogWarning("cannot track target because target is missing");
                              return;
                        }

                        if (thirdPersonCam == null) {
                              Debug.LogWarning("cannot track target because camera is missing");
                              return;
                        }

                        var target         = _last.Item1;
                        var directionMoved = _last.Item2;

                        // Stub implementation 
                        thirdPersonCam.transform.LookAt(target.transform.position, directionMoved);
                  }

                  TrackTarget();
            }


            public void Track(Avatar target) {
                  var old = _last.Item1.name;
                  if (_last != null && old != target.name) {
                        Debug.LogWarning($"swapping camera target from {old} to {target.name}");
                  }

                  target.MovedEvents += (avatar, direction) => { _last = new Tuple<Avatar, Vector3>(avatar, direction); };
            }

      }

}