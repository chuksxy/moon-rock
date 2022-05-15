using UnityEngine;

namespace us_dead_kids.skill {

      public class SkillBehaviour : StateMachineBehaviour {

            public override void OnStateEnter(Animator animator, AnimatorStateInfo i, int layer) {
                  var skill = SkillRegistry.Read(i);
                  if (skill == null) {
                        Debug.LogWarning($"Attempting to access null skill assigned to [{animator.name}]");
                        return;
                  }

                  skill.Invoke(animator, i, layer);
            }


            public override void OnStateExit(Animator animator, AnimatorStateInfo i, int layer) {
                  var skill = SkillRegistry.Read(i);
                  if (skill == null) {
                        Debug.LogWarning($"Attempting to access null skill assigned to [{animator.name}]");
                        return;
                  }

                  skill.Cancel();
            }

      }

}