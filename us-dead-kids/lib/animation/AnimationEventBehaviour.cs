using System.Collections.Generic;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.animation {

      public class AnimationEventBehaviour : StateMachineBehaviour {

            private static class Params {

                  public static readonly int NormalisedTime = Animator.StringToHash("NormalisedTime");
                  public static readonly int Exit           = Animator.StringToHash("Exit");

            }


            public override void OnStateEnter(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) return;

                  // Cannot transition immediately out of a state
                  a.SetBool(Params.Exit, false);
                  a.SetFloat(Params.NormalisedTime, i.normalizedTime);

                  state.Invoke(a, i, layer);
            }


            public override void OnStateUpdate(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) return;

                  a.SetFloat(Params.NormalisedTime, i.normalizedTime);

                  if (i.normalizedTime >= state.ExitTime()) {
                        a.SetBool(Params.Exit, true);
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


                  a.SetBool(Params.Exit, true);
                  a.SetLayerWeight(layer, 0.0f);

                  state.Cancel(a, i, layer);
            }

      }

}