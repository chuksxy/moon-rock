using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.animation {

      [CreateAssetMenu(fileName = "State", menuName = "Us-Dead-Kids/Animation/State", order = 1)]
      public class AnimationStateSO : ScriptableObject {

            private const float DEFAULT_EXIT_TIME = 0.5f;

            [SerializeField] private bool                 mirror;
            [SerializeField] private string               stateName;
            [SerializeField] private List<AnimationEvent> events;
            [SerializeField] private float                exitTime = -1;

            private List<AnimationEvent.Trigger> _triggers;
            public  string                       Name => stateName;


            public float ExitTime() {
                  return exitTime <= 0 ? DEFAULT_EXIT_TIME : exitTime;
            }


            public AnimationState ToState(int skillNameHash) {
                  return new AnimationState(stateName, skillNameHash);
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


            private List<AnimationEvent.Trigger> Triggers() {
                  return _triggers ??= events.Select(a => a.ToTrigger()).ToList();
            }


            public bool Mirror() {
                  return mirror;
            }

      }

}