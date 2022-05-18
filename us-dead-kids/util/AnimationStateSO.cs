using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.util {

      [CreateAssetMenu(fileName = "Skill", menuName = "Us-Dead-Kids/Skills/Skill", order = 1)]
      public class AnimationStateSo : ScriptableObject {

            [SerializeField] private string               stateName;
            [SerializeField] private List<AnimationEvent> events;

            public string ID => stateName;


            private List<AnimationEvent.Trigger> _triggers;


            private List<AnimationEvent.Trigger> Triggers() {
                  return _triggers ??= events.Select(a => a.ToTrigger()).ToList();
            }


            internal void Invoke(Animator a, AnimatorStateInfo i, int layer) {
                  Triggers().ForEach(trigger => trigger.Invoke(this, a, i, layer));
            }


            internal void Tick(Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Attempting to access null avatar on [{a.name}].");
                        return;
                  }

                  var state = avatar.AnimState(stateName);
                  if (state == null) return;

                  state.NormalisedTime = i.normalizedTime;
            }


            internal void Cancel(Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Attempting to access null avatar on [{a.name}].");
                        return;
                  }

                  var state = avatar.AnimState(stateName);
                  if (state == null) return;

                  state.NormalisedTime = 0;
                  Triggers().ForEach(action => AnimationEvent.Trigger.Cancel(this, a, i, layer));
            }


            public AnimationState ToState(int skillNameHash) {
                  return new AnimationState(stateName, skillNameHash);
            }

      }

}