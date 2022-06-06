using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using us_dead_kids.environment;
using Environment = us_dead_kids.environment.Environment;

namespace us_dead_kids.lib.animation {

      /// <summary>
      /// Read externally configured animation states;
      /// </summary>
      public class AnimationRegistry : MonoBehaviour {

            [SerializeField] private List<AnimationStateSO> states;

            private static readonly Dictionary<int, AnimationStateSO> Animations = new();


            public void LoadAll() {
                  states ??= new List<AnimationStateSO>();
                  states.AddRange(Resources.LoadAll<AnimationStateSO>($"{Environment.Path()}/animations"));
            }


            public void Init() {
                  if (states is {Count: > 0}) {
                        states.ForEach(s => {
                              if (s.Mirror()) {
                                    Animations.Add(Animator.StringToHash($"{s.Name}_l"), s);
                                    Animations.Add(Animator.StringToHash($"{s.name}_r"), s);
                                    return;
                              }

                              Animations.Add(Animator.StringToHash(s.name), s);
                        });
                  }
            }


            public static AnimationStateSO Read(AnimatorStateInfo i) {
                  return Animations.ContainsKey(i.shortNameHash) ? Animations[i.shortNameHash] : null;
            }

      }

}