using System.Collections.Generic;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.animation {

      public static class AnimationBehaviourParams {

            public static readonly int NormalisedTime = Animator.StringToHash("NormalisedTime");


      }

      public class AnimationEventBehaviour : StateMachineBehaviour {


            public override void OnStateEnter(Animator a, AnimatorStateInfo i, int layer) {
                  a.SetFloat(AnimationBehaviourParams.NormalisedTime, i.normalizedTime);

                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Avatar not assigned to animator [{a.name}]");
                        return;
                  }

                  avatar.ToggleLock(layer, true);

                  var r = AnimationStateRegistry.Read(i);
                  if (r == null) return;

                  r.Invoke(a, i, layer);
            }


            public override void OnStateUpdate(Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Attempting to access null avatar on [{a.name}].");
                        return;
                  }


                  var state = avatar.AnimState(i.fullPathHash);
                  if (state == null) {
                        state = new AnimationState("", i.fullPathHash);
                        avatar.SetAnimState(state);
                  }

                  a.SetFloat(AnimationBehaviourParams.NormalisedTime, i.normalizedTime);
                  state.NormalisedTime = i.normalizedTime;
            }


            public override void OnStateExit(Animator a, AnimatorStateInfo i, int layer) {
                  var r = AnimationStateRegistry.Read(i);
                  if (r == null) return;
                  r.Cancel(a, i, layer);
            }

      }

}