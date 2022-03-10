using UnityEngine;

namespace us_dead_kids.character {

      public partial class Character {

            public static class Generator {

                  public static Controller Generate(string name, int priority) {
                        var g = new GameObject();
                        if (!SetupAnimator(g)) return null;
                        SetupRigidBody(g);
                        return SetupController(name, priority, g);
                  }


                  private static Controller SetupController(string name, int priority, GameObject gameObject) {
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


                  // Setup Character's animation controller at runtime.
                  private static bool SetupAnimator(GameObject g) {
                        var animatorController =
                              Resources.Load("master_controller.controller");
                        if (animatorController == null) {
                              Debug.LogWarning("animation controller not found when generating character.");
                              return false;
                        }

                        var animator = g.AddComponent<Animator>();
                        animator.runtimeAnimatorController = animatorController as RuntimeAnimatorController;

                        return true;
                  }


                  // Setup character's rigid body as a child game object.
                  private static void SetupRigidBody(GameObject parent) {
                        var g = new GameObject();
                        g.transform.SetParent(parent.transform);
                        g.AddComponent<Rigidbody>();
                  }


            }

      }

}