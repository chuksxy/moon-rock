using System.Collections;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.animation {

      [System.Serializable]
      public struct AnimationEvent {

            public float           invokeTime;
            public AnimationAction action;


            public Trigger ToTrigger() {
                  return new Trigger {
                        TimeMarker = invokeTime,
                        Action     = action,
                  };
            }


            public class Trigger {

                  public float           TimeMarker { get; set; }
                  public AnimationAction Action     { get; set; }


                  public void Invoke(AnimationStateSo s, Animator a, AnimatorStateInfo i, int layer) {
                        var avatar = a.GetComponentInParent<Avatar>();
                        if (avatar == null) {
                              Debug.LogWarning($"Avatar not assigned to game object [{a.name}]");
                              return;
                        }

                        var state = ReadState(s, i, avatar);
                        state.IsCancelled = false;

                        avatar.InvokeCouroutine(() => InvokeTrigger(state, a, i));
                  }


                  public static void Cancel(AnimationStateSo s, Animator a, AnimatorStateInfo i, int layer) {
                        var state = ReadState(s, i, a);
                        if (state == null) return;

                        state.IsCancelled = true;
                  }


                  private static AnimationState ReadState(AnimationStateSo s, AnimatorStateInfo i, Component c) {
                        var avatar = c.GetComponentInParent<Avatar>();
                        if (avatar == null) {
                              Debug.LogWarning($"Avatar not assigned to game object [{c.name}]");
                              return null;
                        }

                        var state = avatar.AnimState(s.Name);
                        if (state != null) return state;

                        state = s.ToState(i.shortNameHash);
                        avatar.SetAnimState(state);

                        return state;
                  }


                  private IEnumerator InvokeTrigger(AnimationState state, Animator a, AnimatorStateInfo i) {
                        yield return new WaitUntil(() => state.NormalisedTime >= TimeMarker && state is {IsCancelled: false});

                        Action.BeginHeadShot(state, a);
                        Action.BeginTrace(state, a);
                        Action.BeginLockOn(state, a);
                        Action.EndLockOn(state, a);
                        Action.EndTrace(state, a);
                        Action.EndHeadShot(state, a);
                  }

            }

      }

}