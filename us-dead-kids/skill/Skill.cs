using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.skill {

      public class Skill : ScriptableObject {

            [SerializeField] private string       skillID;
            [SerializeField] private List<Action> actions;

            public string       ID      => skillID;
            public List<Action> Actions => actions;

            public class Action : ScriptableObject {

                  [SerializeField] private float                       actionID;
                  [SerializeField] private float                       startTime;
                  [SerializeField] private float                       endTime;
                  [SerializeField] private UnityEvent<Skill, Animator> startEvent;
                  [SerializeField] private UnityEvent<Skill, Animator> endEvent;


                  public void Invoke(Skill s, Animator animator, AnimatorStateInfo info, int layer) {
                        var avatar = animator.GetComponent<Avatar>();
                        if (avatar == null) {
                              Debug.LogWarning($"Avatar not assigned to game object [{animator.name}]");
                        }

                        avatar.StartCoroutine(Start(s, animator, info));
                        avatar.StartCoroutine(End(s, animator, info));
                  }


                  private IEnumerator Start(Skill s, Animator animator, AnimatorStateInfo i) {
                        yield return new WaitUntil(() => i.normalizedTime >= startTime);
                        startEvent?.Invoke(s, animator);
                  }


                  private IEnumerator End(Skill s, Animator animator, AnimatorStateInfo i) {
                        yield return new WaitUntil(() => i.normalizedTime >= endTime);
                        endEvent?.Invoke(s, animator);
                  }

            }


            internal void Invoke(Animator animator, AnimatorStateInfo info, int layer) {
                  actions.ForEach(action => action.Invoke(this, animator, info, layer));
            }

      }

}