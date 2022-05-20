using UnityEngine;

namespace us_dead_kids.lib {

      public class AnimationEventBehaviour : StateMachineBehaviour {

            private class Params {

                  public static readonly int NormalisedTime = Animator.StringToHash("Normalised Time");
                  public static readonly int Transition     = Animator.StringToHash("Transition");

            }


            public override void OnStateEnter(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) {
                        Debug.LogWarning($"Attempting to access null state assigned to [{a.name}]");
                        return;
                  }

                  a.SetBool(Params.Transition, false);
                  a.SetFloat(Params.NormalisedTime, 0.0f);
                  state.Invoke(a, i, layer);
            }


            public override void OnStateUpdate(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) {
                        Debug.LogWarning($"Attempting to access null state assigned to [{a.name}]");
                        return;
                  }

                  a.SetFloat(Params.NormalisedTime, i.normalizedTime);
                  if (i.normalizedTime >= state.ExitTime()) {
                        a.SetBool(Params.Transition, true);
                  }

                  state.Tick(a, i, layer);
            }


            public override void OnStateExit(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) {
                        Debug.LogWarning($"Attempting to access null state assigned to [{a.name}]");
                        return;
                  }

                  a.SetFloat(Params.NormalisedTime, 0.0f);
                  a.SetBool(Params.Transition, true);
                  
                  state.Cancel(a, i, layer);
            }

      }

}