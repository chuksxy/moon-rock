using UnityEngine;

namespace us_dead_kids.character {

      public partial class Character {

            public static class Generator {

                  public static Controller Generate(string name, int priority) {
                        var gameObject = new GameObject();

                        // Setup Character's animation controller at runtime.
                        bool SetupAnimator() {
                              var animatorController =
                                    Resources.Load("master_controller.controller");
                              if (animatorController == null) {
                                    Debug.LogWarning("animation controller not found when generating character.");
                                    return false;
                              }

                              var animator = gameObject.AddComponent<Animator>();
                              animator.runtimeAnimatorController = animatorController as RuntimeAnimatorController;

                              return true;
                        }

                        if (!SetupAnimator()) return null;

                        // Setup character's rigid body as a child game object.
                        void SetupRigidBody() {
                              var g = new GameObject();
                              g.transform.SetParent(gameObject.transform);
                              g.AddComponent<Rigidbody>();
                        }

                        SetupRigidBody();


                        var controller  = gameObject.AddComponent<Controller>();
                        var characterID = $"character_{System.Guid.NewGuid().ToString()}";
                        var character = new Character() {
                              ID       = characterID,
                              Name     = name,
                              Priority = priority
                        };
                        controller.Init(character);

                        return controller;
                  }

            }

      }

}