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

                  [SerializeField] private float                actionID;
                  [SerializeField] private float                startTime;
                  [SerializeField] private float                endTime;
                  [SerializeField] private UnityEvent<Animator> startEvent;
                  [SerializeField] private UnityEvent<Animator> endEvent;


                  public void Invoke(Animator animator, AnimatorStateInfo info, int layer) {
                        var avatar = animator.GetComponent<Avatar>();
                        if (avatar == null) {
                              Debug.LogWarning($"Avatar not assigned to game object [{animator.name}]");
                        }

                        avatar.StartCoroutine(Start(animator, info));
                        avatar.StartCoroutine(End(animator, info));
                  }


                  private IEnumerator Start(Animator animator, AnimatorStateInfo i) {
                        yield return new WaitUntil(() => i.normalizedTime >= startTime);
                        startEvent?.Invoke(animator);
                  }


                  private IEnumerator End(Animator animator, AnimatorStateInfo i) {
                        yield return new WaitUntil(() => i.normalizedTime >= endTime);
                        endEvent?.Invoke(animator);
                  }

            }


            private void Invoke(Animator animator, AnimatorStateInfo info, int layer) {
                  actions.ForEach(action => action.Invoke(animator, info, layer));
            }


            public class Behaviour : StateMachineBehaviour {

                  public override void OnStateEnter(Animator animator, AnimatorStateInfo info, int layer) {
                        var skill = SkillRegistry.Read(info);
                        if (skill == null) {
                              Debug.LogWarning($"Attempting to access null skill assigned to [{animator.name}]");
                              return;
                        }

                        skill.Invoke(animator, info, layer);
                  }

            }


      }


}