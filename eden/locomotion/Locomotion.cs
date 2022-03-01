using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace eden.locomotion {

      public class Locomotion : Eden.IService {

            private static readonly Eden.ServiceUnit<Locomotion> ActiveUnit = Eden.ServiceUnit<Locomotion>.New();

            private readonly SerializedDictionary<string, Vector3> _entityPositions;


            // Hide our constructor.
            private Locomotion(SerializedDictionary<string, Vector3> entityPositions) {
                  _entityPositions = entityPositions;
            }


            // New Locomotion service.
            public static Eden.IService New() {
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


            // Jump Via Animator in the direction specified and apply modifier.
            public void JumpViaAnimator(string entityID, Animator animator, Action<Animator> jumpFunc) {
                  if (!_entityPositions.ContainsKey(entityID)) {
                        Debug.LogWarning($"Entity [{entityID}] cannot jump. It is not registered.");
                        return;
                  }

                  if (animator == null) {
                        Debug.LogWarning($"Entity [{entityID}] cannot jump. It does not have an animator present.");
                        return;
                  }

                  jumpFunc.Invoke(animator);
            }


            public Eden.ServiceUnit<Locomotion> Register() {
                  return ActiveUnit.Init(this);
            }

      }

}