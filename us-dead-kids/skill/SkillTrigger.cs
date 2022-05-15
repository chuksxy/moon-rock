using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.skill {

      public class SkillTrigger : ScriptableObject {

            [SerializeField] private float                       actionID;
            [SerializeField] private float                       startTime;
            [SerializeField] private float                       endTime;
            [SerializeField] private UnityEvent<Skill, Animator> startEvent;
            [SerializeField] private UnityEvent<Skill, Animator> endEvent;

            private bool IsCancelled { get; set; }


            public void Invoke(Skill s, Animator animator, AnimatorStateInfo info, int layer) {
                  var avatar = animator.GetComponent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Avatar not assigned to game object [{animator.name}]");
                  }

                  IsCancelled = false;

                  avatar.StartCoroutine(Start(s, animator, info));
                  avatar.StartCoroutine(End(s, animator, info));
            }


            public void Cancel() {
                  IsCancelled = true;
            }


            private IEnumerator Start(Skill s, Animator animator, AnimatorStateInfo i) {
                  yield return new WaitUntil(() => i.normalizedTime >= startTime && IsCancelled);
                  startEvent?.Invoke(s, animator);
            }


            private IEnumerator End(Skill s, Animator animator, AnimatorStateInfo i) {
                  yield return new WaitUntil(() => i.normalizedTime >= endTime && !IsCancelled);
                  endEvent?.Invoke(s, animator);
            }

      }

}