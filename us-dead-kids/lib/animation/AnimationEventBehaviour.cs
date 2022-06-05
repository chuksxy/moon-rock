using System;
using System.Collections.Generic;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.lib.animation {

      public static class AnimationBehaviourParams {

            public static readonly int NormalisedTime = Animator.StringToHash("NormalisedTime");
            public static readonly int Exit           = Animator.StringToHash("Exit");


      }

      public class AnimationEventBehaviour : StateMachineBehaviour {

            public delegate void CallBack();

            public CallBack OnEnter;
            public CallBack OnExit;


            public override void OnStateEnter(Animator a, AnimatorStateInfo i, int layer) {
                  a.SetFloat(AnimationBehaviourParams.NormalisedTime, 0.0f);
                  a.SetBool(AnimationBehaviourParams.Exit, false);

                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Avatar not assigned to animator [{a.name}]");
                        return;
                  }

                  var state = avatar.AnimState(i);
                  state.NormalisedTime = 0.0f;

                  var animationStateSo = AnimationRegistry.Read(i);
                  if (animationStateSo == null) return;

                  animationStateSo.Invoke(a, i, layer);
                  animationStateSo.OverrideParameters(this, a, i, layer);
            }


            public override void OnStateUpdate(Animator a, AnimatorStateInfo i, int layer) {
                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Attempting to access null avatar on [{a.name}].");
                        return;
                  }

                  var state = avatar.AnimState(i);
                  state.NormalisedTime = i.normalizedTime;

                  a.SetBool(AnimationBehaviourParams.Exit, state.Exit());
                  a.SetFloat(AnimationBehaviourParams.NormalisedTime, i.normalizedTime);
            }


            public override void OnStateExit(Animator a, AnimatorStateInfo i, int layer) {
                  a.SetFloat(AnimationBehaviourParams.NormalisedTime, 1.0f);
                  a.SetBool(AnimationBehaviourParams.Exit, true);

                  var avatar = a.GetComponentInParent<Avatar>();
                  if (avatar == null) {
                        Debug.LogWarning($"Avatar not assigned to animator [{a.name}]");
                        return;
                  }

                  var state = avatar.AnimState(i);
                  state.NormalisedTime = 1.0f;

                  var animationStateSo = AnimationRegistry.Read(i);
                  if (animationStateSo == null) return;
                  
                  animationStateSo.Cancel(a, i, layer);
                  animationStateSo.ResetParameters(a, i, layer);
            }

      }

}