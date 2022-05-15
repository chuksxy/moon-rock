using UnityEngine;

namespace us_dead_kids.skill {

      public class SkillBehaviour : StateMachineBehaviour {

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