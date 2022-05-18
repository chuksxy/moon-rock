using UnityEngine;

namespace us_dead_kids.util {

      [System.Serializable]
      public enum AnimationAction {

            NoAction,
            StartTrace,
            EndTrace,
            LockOn,
            LockOnMultiple,

      }

      public static class AnimationActionExtension {


            // Get Weapon, then trace
            public static void StartTrace(this AnimationAction a, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.StartTrace == a) {
                        Debug.Log($"start tracing weapon for [{animator.name}] via skill [{s.ID}]");
                  }
            }


            // Get Weapon, then trace
            public static void EndTrace(this AnimationAction a, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.EndTrace == a) {
                        Debug.Log($"end tracing weapon for [{animator.name}] via skill [{s.ID}]");
                  }
            }


            // Lock on to single target
            public static void LockOn(this AnimationAction a, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.LockOn == a) {
                        Debug.Log($"lock unto to single target for [{animator.name}] via skill [{s.ID}]");
                  }
            }


            // Lock on to multiple targets/
            // Pull lock on radius from skill metadata
            public static void LockOnMultiple(this AnimationAction a, AnimationStateSo s, Animator animator) {
                  if (AnimationAction.LockOnMultiple == a) {
                        Debug.Log($"lock on to multiple targets for [{animator.name}] via skill [{s.ID}]");
                  }
            }

      }

}