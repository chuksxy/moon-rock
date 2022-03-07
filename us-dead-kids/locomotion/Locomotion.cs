using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace us_dead_kids.locomotion {

      public class Locomotion {

            private readonly SerializedDictionary<string, Vector3> _entityPositions;


            // Hide our constructor.
            private Locomotion(SerializedDictionary<string, Vector3> entityPositions) {
                  _entityPositions = entityPositions;
            }


            // New Locomotion service.
            public static Locomotion New() {
                  return new Locomotion(new SerializedDictionary<string, Vector3>());
            }


            // Move Via Animator in the direction specified and apply modifiers.
            public void MoveViaAnimator(
                  string                           entityID,
                  Animator                         animator,
                  Vector3                          direction,
                  float                            modifier,
                  Action<Animator, Vector3, float> moveFunc) {
                  if (!_entityPositions.ContainsKey(entityID)) {
                        Debug.LogWarning($"Cannot move entity [{entityID}]. It is not registered.");
                        return;
                  }

                  if (animator == null) {
                        Debug.LogWarning($"Cannot move entity [{entityID}]. It does not have an animator present.");
                        return;
                  }

                  moveFunc.Invoke(animator, direction, modifier);
            }


      }

}