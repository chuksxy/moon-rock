using UnityEngine;

namespace us_dead_kids.lib.animation {

      public static class AnimationExtension {

            public static int GetDominantLayer(this Animator animator) {
                  var dominant_index  = 0;
                  var dominant_weight = 0f;
                  
                  for (var index = 0; index < animator.layerCount; index++) {
                        var weight = animator.GetLayerWeight(index);
                        if (!(weight > dominant_weight)) continue;
                        dominant_weight = weight;
                        dominant_index  = index;
                  }

                  return dominant_index;
            }

      }

}