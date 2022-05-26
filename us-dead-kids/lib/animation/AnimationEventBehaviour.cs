using System.Collections.Generic;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.animation {

      public class AnimationEventBehaviour : StateMachineBehaviour {

            private class Params {

                  public static readonly int NormalisedTime = Animator.StringToHash("Normalised Time");
                  public static readonly int Transition     = Animator.StringToHash("Transition");

            }


            public override void OnStateEnter(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) return;

                  // Cannot transition immediately out of a state
                  a.SetBool(Params.Transition, false);
                  a.SetFloat(Params.NormalisedTime, i.normalizedTime);

                  state.Invoke(a, i, layer);
            }


            public override void OnStateUpdate(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) return;

                  a.SetFloat(Params.NormalisedTime, i.normalizedTime);
                  if (i.normalizedTime >= state.ExitTime()) {
                        a.SetBool(Params.Transition, true);
                  }

                  state.Tick(a, i, layer);
            }


            public override void OnStateExit(Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Avatar not assigned to animator [{a.name}]");
                        return;
                  }

                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) return;

                  a.SetFloat(Params.NormalisedTime, i.normalizedTime);
                  a.SetBool(Params.Transition, true);

                  state.Cancel(a, i, layer);
            }

      }

}