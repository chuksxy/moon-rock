using eden;
using eden.equipment;
using moon.rock.character.equipment.shoe;
using UnityEngine;

namespace moon.rock.character {

      public partial class Character : MonoBehaviour {

            [SerializeField] private string characterID;

            private Animator _animator;


            private void Awake() {
                  _animator = GetComponentInChildren<Animator>();
            }


            // Move character in a specific direction.
            public void Move(Vector3 direction) {
                  void MoveViaAnimator(Animator animator, Vector3 moveDirection, float modifier) {
                        var speed = Mathf.Max(moveDirection.x, moveDirection.y) * modifier;
                        animator.SetFloat(Animation.Param.Move, speed);
                  }

                  var speedModifier = Eden.GetService<Equipment>()
                                          .GetFactory()
                                          .Get<Shoe.Factory>()
                                          .GetEquipment<Shoe>(characterID)
                                          .SpeedModifier;

                  Eden.GetService<eden.locomotion.Locomotion>()
                      .MoveViaAnimator(characterID, _animator, direction, 1.0f, MoveViaAnimator);
            }


            // Jump in a specific direction.
            public void Jump(Vector3 direction) {
                  void JumpViaAnimator(Animator animator) {
                        animator.SetTrigger(Animation.Param.Jump);
                  }

                  Eden.GetService<eden.locomotion.Locomotion>().JumpViaAnimator(characterID, _animator, JumpViaAnimator);
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