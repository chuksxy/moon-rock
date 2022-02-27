using System;
using eden;
using UnityEngine;

namespace moon.rock.character {

      public class Character : MonoBehaviour {

            public static class Animation {

                  public static class Param {

                        public static readonly int Move = Animator.StringToHash("Move");

                  }

            }

            [SerializeField] private string characterID;


            // Move character in a specific direction.
            public void Move(Vector3 direction) {
                  Action<Animator, Vector3, float> moveFunc = (Animator animator, Vector3 direction, float modifier) => {
                        var speed = Mathf.Max(direction.x, direction.y) * modifier;
                        animator.SetFloat(Animation.Param.Move, speed);
                  };
                  Eden.GetService<eden.locomotion.Eden.Locomotion>()
                      .MoveViaAnimator(characterID, this, direction, 1.0f, moveFunc);
            }


            // Jump in a specific direction.
            public void Jump(Vector3 direction) {
                  Eden.GetService<eden.locomotion.Eden.Locomotion>().Jump(characterID, this, direction, 1.0f);
            }


            // Interact with an entity in the world.
            public void Interact() { }


            // Use Melee Attack.
            public void UseMelee() { }


            // Use Charge Attack.
            public void UseChargeAttack() { }


            // Use Primary Weapon.
            public void UsePrimaryWeapon() { }


            // Use Secondary Weapon.
            public void UseSecondaryWeapon() { }


            // Dodge an incoming attack.
            public void Dodge() { }


            // Guard an incoming attack.
            public void Guard(bool guard) { }


            // Take Damage from an entity in the world.
            public void TakeDamage(string culpritID) { }

      }

}