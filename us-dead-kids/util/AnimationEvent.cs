using System.Collections;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.util {

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
                        var avatar = a.GetComponentInParent<avatar.Avatar>();
                        if (avatar == null) {
                              Debug.LogWarning($"Avatar not assigned to game object [{a.name}]");
                              return;
                        }

                        var state = ReadState(s, i, avatar);
                        state.IsCancelled = false;

                        avatar.InvokeAsync(() => Invoke(s, a, i));
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

                        var state = avatar.AnimState(s.ID);
                        if (state != null) return state;

                        state = s.ToState(i.shortNameHash);
                        avatar.SetSkillState(state);

                        return state;
                  }


                  private IEnumerator Invoke(AnimationStateSo s, Animator a, AnimatorStateInfo i) {
                        var state = ReadState(s, i, a);
                        yield return new WaitUntil(() => state.NormalisedTime >= TimeMarker && state is {IsCancelled: false});

                        Action.StartTrace(s, a);
                        Action.EndTrace(s, a);
                        Action.LockOn(s, a);
                        Action.LockOnMultiple(s, a);
                  }

            }

      }

}