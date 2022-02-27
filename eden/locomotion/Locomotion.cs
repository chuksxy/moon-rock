using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace eden.locomotion {

      public static partial class Eden {

            public class Locomotion : eden.Eden.IService {

                  private readonly SerializedDictionary<string, Vector3> _entityPositions;


                  // Hide our constructor.
                  private Locomotion(SerializedDictionary<string, Vector3> entityPositions) {
                        _entityPositions = entityPositions;
                  }


                  // New Locomotion service.
                  public static eden.Eden.IService New() {
                        return new Locomotion(new SerializedDictionary<string, Vector3>());
                  }


                  // Move Via CharacterController in the direction specified and apply modifiers.
                  public void MoveViaCharacterController(
                        string entityID, Component character, Vector3 direction, float modifier) {
                        if (!_entityPositions.ContainsKey(entityID)) {
                              Debug.LogWarning($"Cannot move entity [{entityID}], it is not registered.");
                              return;
                        }

                        var controller = character.GetComponentInChildren<CharacterController>();
                        // do some stuff.
                  }


                  // Move Via Animator in the direction specified and apply modifiers.
                  public void MoveViaAnimator(
                        string                           entityID,
                        Component                        character,
                        Vector3                          direction,
                        float                            modifier,
                        Action<Animator, Vector3, float> moveFunc) {
                        
                        if (!_entityPositions.ContainsKey(entityID)) {
                              Debug.LogWarning($"Cannot move entity [{entityID}], it is not registered.");
                              return;
                        }

                        var animator = character.GetComponentInChildren<Animator>();
                        if (animator == null) {
                              Debug.LogWarning($"Cannot move entity [{entityID}], it does not have an animator present.");
                              return;
                        }

                        moveFunc.Invoke(animator, direction, modifier);
                  }


                  // Jump in direction specified and apply modifier.
                  public void Jump(string entityID, Component character, Vector3 direction, float modifier) { }


            }

      }

}