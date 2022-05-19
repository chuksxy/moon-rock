using System.Collections.Generic;
using UnityEngine;

namespace us_dead_kids.lib {

      public class AnimationStateRegistry : MonoBehaviour {

            [SerializeField] private List<AnimationStateSo> animStates;

            private static readonly Dictionary<int, AnimationStateSo> Animations = new Dictionary<int, AnimationStateSo>();


            private void Awake() {
                  if (animStates != null && animStates.Count > 0) {
                        animStates.ForEach(s => {
                              var shortNameHash = Animator.StringToHash(s.ID);
                              if (Animations.ContainsKey(shortNameHash)) {
                                    Debug.LogWarning($"Skill [{s.ID}] has already been registered.");
                                    return;
                              }

                              Animations.Add(shortNameHash, s);
                        });
                  }
            }


            public static AnimationStateSo Read(AnimatorStateInfo i) {
                  var skillHash = i.shortNameHash;
                  if (Animations.ContainsKey(skillHash)) return Animations[skillHash];
                  Debug.LogWarning($"Attempting to access skill not in registry [{i.ToString()}]");
                  return null;
            }

      }

}