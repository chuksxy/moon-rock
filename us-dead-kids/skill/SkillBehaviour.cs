using UnityEngine;

namespace us_dead_kids.skill {

      public class SkillBehaviour : StateMachineBehaviour {

            public override void OnStateEnter(Animator a, AnimatorStateInfo i, int layer) {
                  var skill = SkillRegistry.Read(i);
                  if (skill == null) {
                        Debug.LogWarning($"Attempting to access null skill assigned to [{a.name}]");
                        return;
                  }

                  skill.Invoke(a, i, layer);
            }


            public override void OnStateUpdate(Animator a, AnimatorStateInfo i, int layer) {
                  var skill = SkillRegistry.Read(i);
                  if (skill == null) {
                        Debug.LogWarning($"Attempting to access null skill assigned to [{a.name}]");
                        return;
                  }

                  skill.Tick(a, i, layer);
            }


            public override void OnStateExit(Animator a, AnimatorStateInfo i, int layer) {
                  var skill = SkillRegistry.Read(i);
                  if (skill == null) {
                        Debug.LogWarning($"Attempting to access null skill assigned to [{a.name}]");
                        return;
                  }

                  skill.Cancel(a, i, layer);
            }

      }

}