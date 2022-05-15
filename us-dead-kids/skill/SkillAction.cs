using UnityEngine;

namespace us_dead_kids.skill {

      public class SkillAction {

            // Get Weapon, then trace
            public void StartTrace(Skill s, Animator animator) { }


            // Get Weapon, then trace
            public void EndTrace(Skill s, Animator animator) { }


            // Lock on to single target
            public void LockOn(Skill s, Animator animator) { }


            // Lock on to multiple targets/
            // Pull lock on radius from skill metadata
            public void LockOnMultiple(Skill s, Animator animator) { }

      }

}