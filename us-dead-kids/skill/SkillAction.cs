using UnityEngine;

namespace us_dead_kids.skill {

      [System.Serializable]
      public enum SkillAction {

            NoAction,
            StartTrace,
            EndTrace,
            LockOn,
            LockOnMultiple,

      }

      public static class SkillActionExtension {


            // Get Weapon, then trace
            public static void StartTrace(this SkillAction a, Skill s, Animator animator) {
                  if (SkillAction.StartTrace == a) {
                        Debug.Log($"start tracing weapon for [{animator.name}] via skill [{s.ID}]");
                  }
            }


            // Get Weapon, then trace
            public static void EndTrace(this SkillAction a, Skill s, Animator animator) {
                  if (SkillAction.EndTrace == a) {
                        Debug.Log($"end tracing weapon for [{animator.name}] via skill [{s.ID}]");
                  }
            }


            // Lock on to single target
            public static void LockOn(this SkillAction a, Skill s, Animator animator) {
                  if (SkillAction.LockOn == a) {
                        Debug.Log($"lock unto to single target for [{animator.name}] via skill [{s.ID}]");
                  }
            }


            // Lock on to multiple targets/
            // Pull lock on radius from skill metadata
            public static void LockOnMultiple(this SkillAction a, Skill s, Animator animator) {
                  if (SkillAction.LockOnMultiple == a) {
                        Debug.Log($"lock on to multiple targets for [{animator.name}] via skill [{s.ID}]");
                  }
            }

      }

}