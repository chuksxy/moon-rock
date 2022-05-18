using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.skill {

      [CreateAssetMenu(fileName = "Skill", menuName = "Us-Dead-Kids/Skills/Skill", order = 1)]
      public class Skill : ScriptableObject {

            [SerializeField] private string           skillID;
            [SerializeField] private List<SkillEvent> events;

            public string ID => skillID;


            private List<SkillTrigger> _triggers;


            private List<SkillTrigger> Triggers() {
                  return _triggers ??= events.Select(a => a.ToTrigger()).ToList();
            }


            internal void Invoke(Animator a, AnimatorStateInfo i, int layer) {
                  Triggers().ForEach(trigger => trigger.Invoke(this, a, i, layer));
            }


            internal void Tick(Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"skill [{skillID}] attempting to access null avatar on [{a.name}].");
                        return;
                  }

                  var state = avatar.GetSkillState(skillID);
                  if (state == null) return;

                  state.NormalisedTime = i.normalizedTime;
            }


            internal void Cancel(Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"skill [{skillID}] attempting to access null avatar on [{a.name}].");
                        return;
                  }

                  var state = avatar.GetSkillState(skillID);
                  if (state == null) return;

                  state.NormalisedTime = 0;
                  Triggers().ForEach(action => SkillTrigger.Cancel(this, a, i, layer));
            }


            public SkillState ToState(int skillNameHash) {
                  return new SkillState(skillID, skillNameHash);
            }

      }

}