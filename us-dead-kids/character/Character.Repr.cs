using System;
using eden;
using UnityEngine;
using us_dead_kids.character.equipment.shoe;
using us_dead_kids.equipment;
using us_dead_kids.locomotion;

namespace us_dead_kids.character {

      public partial class Character {

            public class Repr : MonoBehaviour {

                  [SerializeField] private string characterID;

                  private Animator _animator;


                  // Get the Unity's representation of a Character by it's ID.
                  public static Repr Get(string characterID) {
                        throw new NotImplementedException();
                  }


                  private void Awake() {
                        _animator = GetComponentInChildren<Animator>();
                  }


                  // Move character in a specific direction.
                  public void Move(Vector3 direction) {
                        void MoveViaAnimator(Animator animator, Vector3 moveDirection, float modifier) {
                              var speed = Mathf.Max(moveDirection.x, moveDirection.y) * modifier;
                              animator.SetFloat(Animation.Param.MoveX, speed);
                        }

                        var speedModifier = Eden.GetService<Equipment>()
                                                .Tables()
                                                .GetTable<Shoe.Table, Shoe>()
                                                .Read(characterID)
                                                .SpeedModifier;

                        Eden.GetService<Locomotion>()
                            .MoveViaAnimator(characterID, _animator, direction, speedModifier, MoveViaAnimator);
                  }


                  // Interact with an entity in the world.
                  // Pick up weapons in the world. E.g Picking up a Great-sword will change your melee move-set.
                  public void Interact() { }


                  // Use Melee Attack.
                  public void UseMelee() { }


                  // Use Skill in the specified slot.
                  // 1 - Melee
                  // 2 - Gun Skill
                  // 3 -
                  // 4 - Grenade?
                  public void UseSkill(int slot) { }


                  // Use Primary Weapon.
                  public void UsePrimaryWeapon() { }


                  // Use Secondary Weapon.
                  public void UseSecondaryWeapon() { }


                  // Use Item equipped in slot
                  public void UseItem() { }


                  // Select Item in item slot.
                  public void SelectItem(int x, int y) { }


                  // Dodge an incoming attack.
                  public void Dodge() { }


                  // Guard an incoming attack.
                  public void Guard(bool guard) { }


                  // Take Damage from an entity in the world.
                  public void TakeDamage(string culpritID) { }


                  // Set state to Alive or Dead.
                  public void SetState(bool alive) { }


                  internal Animator GetAnimator() {
                        return _animator;
                  }

            }

      }

}