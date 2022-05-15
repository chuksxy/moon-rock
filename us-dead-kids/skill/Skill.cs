using System.Collections.Generic;
using UnityEngine;

namespace us_dead_kids.skill {

      public class Skill : ScriptableObject {

            [SerializeField] private string             skillID;
            [SerializeField] private List<SkillTrigger> actions;

            public string ID => skillID;

            // Actions assigned to skill. e.g
            // Lock On, Trace Weapon e.t.c
            public List<SkillTrigger> Actions => actions;


            internal void Invoke(Animator animator, AnimatorStateInfo info, int layer) {
                  actions.ForEach(action => action.Invoke(this, animator, info, layer));
            }


            internal void Cancel() {
                  actions.ForEach(action => action.Cancel());
            }

      }

}