using UnityEngine;
using us_dead_kids.lib.lock_on;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.animation {

      [System.Serializable]
      public enum AnimationAction {

            NoAction,
            BeginTrace,
            EndTrace,
            LockOn,
            LockOnEnd,
            HeadShotWindowBegin,
            HeadShotWindowEnd,

      }

      public static class AnimationActionExtension {


            // Get Weapon, then trace
            public static void StartTrace(this AnimationAction a, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.BeginTrace == a) {
                        Debug.Log($"start tracing weapon for [{animator.name}] via skill [{s.Name}]");
                  }
            }


            // Get Weapon, then trace
            public static void EndTrace(this AnimationAction a, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.EndTrace == a) {
                        Debug.Log($"end tracing weapon for [{animator.name}] via skill [{s.Name}]");
                  }
            }


            // Lock on to single target
            public static void LockOnBegin(this AnimationAction action, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.LockOn != action) return;

                  Debug.Log($"lock unto to single target for [{animator.name}] via skill [{s.Name}]");

                  var avatar = animator.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"No avatar assigned to animator [{animator.name}]");
                        return;
                  }

                  Target.Populate(avatar);
            }


            // Lock on to multiple targets/
            // Pull lock on radius from skill metadata
            public static void LockOnEnd(this AnimationAction action, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.LockOnEnd != action) return;

                  Debug.Log($"lock on to multiple targets for [{animator.name}] via skill [{s.Name}]");

                  var avatar = animator.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"No avatar assigned to animator [{animator.name}]");
                        return;
                  }

                  Target.Clear(avatar);
            }


            // Listen for another button press at just the right moment to trigger a head shot.
            public static void HeadShotWindowBegin(this AnimationAction a, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.HeadShotWindowBegin == a) {
                        Debug.Log("Head shot window start");
                  }
            }


            // Listen for another button press at just the right moment to trigger a head shot.
            public static void HeadShotWindowEnd(this AnimationAction a, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.HeadShotWindowEnd == a) {
                        Debug.Log("Head shot end");
                  }
            }

      }

}