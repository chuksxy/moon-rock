using System.Collections.Generic;
using UnityEngine;

namespace us_dead_kids.lib.animation {

      /// <summary>
      /// Read externally configured animation states;
      /// </summary>
      public class AnimationStateRegistry : MonoBehaviour {

            [SerializeField] private List<AnimationStateSo> animStates;

            private static readonly Dictionary<int, AnimationStateSo> Animations = new Dictionary<int, AnimationStateSo>();


            private void Awake() {
                  if (animStates != null && animStates.Count > 0) {
                        animStates.ForEach(s => {
                              var shortNameHash = Animator.StringToHash(s.Name);
                              if (Animations.ContainsKey(shortNameHash)) {
                                    Debug.LogWarning($"Animation [{s.Name}] has already been registered.");
                                    return;
                              }

                              Animations.Add(shortNameHash, s);
                        });
                  }
            }


            public static AnimationStateSo Read(AnimatorStateInfo i) {
                  var skillHash = i.shortNameHash;
                  return Animations.ContainsKey(skillHash) ? Animations[skillHash] : null;
            }

      }

}