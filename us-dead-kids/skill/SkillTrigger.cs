using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.skill {

      public class SkillTrigger {

            public float       TimeMarker { get; set; }
            public SkillAction Action     { get; set; }


            public void Invoke(Skill s, Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Avatar not assigned to game object [{a.name}]");
                        return;
                  }

                  var state = ReadState(s, i, avatar);
                  state.IsCancelled = false;

                  avatar.InvokeAsync(() => Invoke(s, a, i));
            }


            public static void Cancel(Skill s, Animator a, AnimatorStateInfo i, int layer) {
                  var state = ReadState(s, i, a);
                  if (state == null) return;

                  state.IsCancelled = true;
            }


            private static SkillState ReadState(Skill s, AnimatorStateInfo i, Component c) {
                  var avatar = c.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Avatar not assigned to game object [{c.name}]");
                        return null;
                  }

                  var state = avatar.GetSkillState(s.ID);
                  if (state != null) return state;

                  state = s.ToState(i.shortNameHash);
                  avatar.TrackSkillState(state);

                  return state;
            }


            private IEnumerator Invoke(Skill s, Animator a, AnimatorStateInfo i) {
                  var state = ReadState(s, i, a);
                  yield return new WaitUntil(() => state.NormalisedTime >= TimeMarker && state is {IsCancelled: false});

                  Action.StartTrace(s, a);
                  Action.EndTrace(s, a);
                  Action.LockOn(s, a);
                  Action.LockOnMultiple(s, a);
            }

      }

}