using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.skill {

      public class SkillAction : ScriptableObject {

            [SerializeField] private string                      actionID;
            [SerializeField] private float                       startTime;
            [SerializeField] private float                       endTime;
            [SerializeField] private UnityEvent<Skill, Animator> startEvent;
            [SerializeField] private UnityEvent<Skill, Animator> endEvent;


            public void Invoke(Skill s, Animator animator, AnimatorStateInfo i, int layer) {
                  var avatar = animator.GetComponent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Avatar not assigned to game object [{animator.name}]");
                        return;
                  }

                  var state = ReadState(s, i, avatar);
                  state.IsCancelled = false;

                  avatar.StartCoroutine(Start(s, animator, i));
                  avatar.StartCoroutine(End(s, animator, i));
            }


            public static void Cancel(Skill s, Animator animator, AnimatorStateInfo i, int layer) {
                  var state = ReadState(s, i, animator);
                  if (state == null) return;

                  state.IsCancelled = true;
            }


            private static SkillState ReadState(Skill s, AnimatorStateInfo i, Component c) {
                  var avatar = c.GetComponent<Avatar>();
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


            private IEnumerator Start(Skill s, Animator a, AnimatorStateInfo i) {
                  var state = ReadState(s, i, a);
                  yield return new WaitUntil(() => i.normalizedTime >= startTime && state is {IsCancelled: false});
                  startEvent?.Invoke(s, a);
            }


            private IEnumerator End(Skill s, Animator a, AnimatorStateInfo i) {
                  var state = ReadState(s, i, a);
                  yield return new WaitUntil(() => i.normalizedTime >= endTime && state is {IsCancelled: false});
                  endEvent?.Invoke(s, a);
            }

      }

}