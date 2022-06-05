using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.animation {

      [CreateAssetMenu(fileName = "State", menuName = "Us-Dead-Kids/Animation/State", order = 1)]
      public class AnimationStateSO : ScriptableObject {

            [SerializeField] private bool                       mirror;
            [SerializeField] private string                     stateName;
            [SerializeField] private List<AnimationIntParamSO>  intParameters;
            [SerializeField] private List<AnimationBoolParamSO> boolParameters;
            [SerializeField] private List<AnimationEvent>       events;
            [SerializeField] private float                      exitTime = -1;

            private List<AnimationEvent.Trigger> _triggers;
            public  string                       Name => stateName;


            internal AnimationState ToState(int shortNameHash) {
                  return new AnimationState(stateName, shortNameHash) {
                        ExitTime = exitTime
                  };
            }


            internal void Invoke(Animator a, AnimatorStateInfo i, int layer) {
                  Triggers().ForEach(trigger => trigger.Invoke(this, a, i, layer));
            }


            internal void Cancel(Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Attempting to access null avatar on [{a.name}].");
                        return;
                  }

                  var state = avatar.AnimState(i);
                  if (state == null) return;

                  state.NormalisedTime = 0;
                  Triggers().ForEach(action => AnimationEvent.Trigger.Cancel(this, a, i, layer));
            }


            internal void OverrideParameters(AnimationEventBehaviour b, Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Attempting to access null avatar on [{a.name}].");
                        return;
                  }

                  intParameters.ForEach(param => {
                        Debug.Log($"override parameter [{param.Name()}] for [{a.name}] with value [{param.Value()}]");
                        a.SetInteger(param.Name(), param.Value());
                  });

                  boolParameters.ForEach(param => {
                        Debug.Log($"override parameter [{param.Name()}] for [{a.name}] with value [{param.Value()}]");
                        a.SetBool(param.Name(), param.Value());
                  });
            }


            internal void ResetParameters(Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Attempting to access null avatar on [{a.name}].");
                        return;
                  }

                  intParameters.ForEach(param => {
                        if (!param.Reset()) return;
                        Debug.Log($"resetting parameter [{param.Name()}] for [{a.name}] with value 0");
                        a.SetInteger(param.Name(), 0);
                  });

                  boolParameters.ForEach(param => {
                        if (!param.Reset()) return;
                        Debug.Log($"resetting parameter [{param.Name()}] for [{a.name}].");
                        a.SetBool(param.Name(), !param.Value());
                  });
            }


            private List<AnimationEvent.Trigger> Triggers() {
                  return _triggers ??= events.Select(a => a.ToTrigger()).ToList();
            }


            public bool Mirror() {
                  return mirror;
            }

      }

}