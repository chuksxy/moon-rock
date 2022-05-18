using UnityEngine;

namespace us_dead_kids.util {

      public class AnimationEventBehaviour : StateMachineBehaviour {

            public override void OnStateEnter(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) {
                        Debug.LogWarning($"Attempting to access null state assigned to [{a.name}]");
                        return;
                  }

                  state.Invoke(a, i, layer);
            }


            public override void OnStateUpdate(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) {
                        Debug.LogWarning($"Attempting to access null state assigned to [{a.name}]");
                        return;
                  }

                  state.Tick(a, i, layer);
            }


            public override void OnStateExit(Animator a, AnimatorStateInfo i, int layer) {
                  var state = AnimationStateRegistry.Read(i);
                  if (state == null) {
                        Debug.LogWarning($"Attempting to access null state assigned to [{a.name}]");
                        return;
                  }

                  state.Cancel(a, i, layer);
            }

      }

}