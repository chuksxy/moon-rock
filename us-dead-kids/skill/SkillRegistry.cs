using System.Collections.Generic;
using UnityEngine;

namespace us_dead_kids.skill {

      public class SkillRegistry : MonoBehaviour {

            [SerializeField] private List<Skill> allSkills;

            private static readonly Dictionary<int, Skill> Skills = new Dictionary<int, Skill>();


            private void Awake() {
                  if (allSkills != null && allSkills.Count > 0) {
                        allSkills.ForEach(s => {
                              var shortNameHash = Animator.StringToHash(s.ID);
                              if (Skills.ContainsKey(shortNameHash)) {
                                    Debug.LogWarning($"Skill [{s.ID}] has already been registered.");
                                    return;
                              }

                              Skills.Add(shortNameHash, s);
                        });
                  }
            }


            public static Skill Read(AnimatorStateInfo i) {
                  var skillHash = i.shortNameHash;
                  if (Skills.ContainsKey(skillHash)) return Skills[skillHash];
                  Debug.LogWarning($"Attempting to access skill not in registry [{i.ToString()}]");
                  return null;
            }

      }

}