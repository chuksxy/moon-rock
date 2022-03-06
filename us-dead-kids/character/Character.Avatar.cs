using System;
using UnityEngine;

namespace us_dead_kids.character {

      public partial class Character {

            public class Avatar : MonoBehaviour {

                  [SerializeField] private string characterID;

                  private Animator _animator;


                  // Get the Unity's representation of a Character by it's ID.
                  public static Avatar Get(string characterID) {
                        throw new NotImplementedException();
                  }


                  private void Awake() {
                        _animator = GetComponentInChildren<Animator>();
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