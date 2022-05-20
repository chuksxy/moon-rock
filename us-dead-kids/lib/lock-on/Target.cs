using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationState = us_dead_kids.lib.animation.AnimationState;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.lock_on {

      public static class Target {

            public static IEnumerator ClearDelayed(Avatar caller, AnimationState state) {
                  if (caller != null) {
                        yield return null;
                  }

                  yield return new WaitUntil(() => state.IsCancelled);

                  Clear(caller);
            }


            public static void Clear(Avatar caller) {
                  if (caller == null) return;
                  caller.Targets      = new List<Avatar>();
                  caller.TrackTargets = false;
            }


            // Track targets in real time
            public static IEnumerator Track(Avatar a) {
                  while (a.TrackTargets) {
                        Populate(a);
                        yield return null;
                  }
            }


            private static void Populate(Avatar caller) {
                  if (caller != null) {
                        // find targets in weapon radius.
                        // the lock on to them
                  }
            }


            public static Avatar Closest(List<Avatar> targets) {
                  return null;
            }

      }

}