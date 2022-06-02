using UnityEngine;
using us_dead_kids.lib.lock_on;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.animation {

      [System.Serializable]
      public enum AnimationAction {

            NoAction,

            // Trace Melee Weapons 
            BeginTrace,
            EndTrace,

            // Track Targets in range
            LockOnBegin,
            LockOnEnd,

            // Enable Head shots
            HeadShotWindowBegin,
            HeadShotWindowEnd,

            // Fire Weapon
            Fire,
            ContinuousFireBegin,
            ContinuousFireEnd,

            // Reload weapon
            Reload,


      }

      public static class AnimationActionExtension {


            // Get Weapon, then trace
            public static void BeginTrace(this AnimationAction a, AnimationState s, Animator animator) {
                  if (AnimationAction.BeginTrace == a) {
                        Debug.Log($"start tracing weapon for [{animator.name}] via skill [{s.Name}]");
                  }
            }


            // Get Weapon, then trace
            public static void EndTrace(this AnimationAction a, AnimationState s, Animator animator) {
                  if (AnimationAction.EndTrace == a) {
                        Debug.Log($"end tracing weapon for [{animator.name}] for anim [{s.Name}]");
                  }
            }


            public static void BeginLockOn(this AnimationAction action, AnimationState state, Animator animator) {
                  if (AnimationAction.LockOnBegin != action) return;

                  Debug.Log($"lock unto to single target for [{animator.name}] for anim [{state.Name}]");

                  var avatar = animator.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"No avatar assigned to animator [{animator.name}]");
                        return;
                  }

                  Target.Clear(avatar);

                  avatar.TrackTargets = true;
                  avatar.InvokeCoroutine(() => Target.Track(avatar));
                  // Clear target when we exit the animation state either way.
                  avatar.InvokeCoroutine(() => Target.ClearDelayed(avatar, state));
            }


            public static void EndLockOn(this AnimationAction action, AnimationState s, Animator animator) {
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
            public static void BeginHeadShot(this AnimationAction a, AnimationState s, Animator animator) {
                  if (AnimationAction.HeadShotWindowBegin == a) {
                        Debug.Log("Head shot window start");
                  }
            }


            // Listen for another button press at just the right moment to trigger a head shot.
            public static void EndHeadShot(this AnimationAction a, AnimationState s, Animator animator) {
                  if (AnimationAction.HeadShotWindowEnd == a) {
                        Debug.Log("Head shot end");
                  }
            }


      }

}