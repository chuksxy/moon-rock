using UnityEngine;

namespace us_dead_kids.character {

      public partial class Character {

            public class Controller : MonoBehaviour {

                  [SerializeField] private string characterID = "";


                  public void Init(string id) {
                        if (string.IsNullOrEmpty(id)) {
                              Debug.LogWarning("character ID cannot be blank");
                              return;
                        }

                        characterID = id;
                  }


                  public void Move(Vector3 direction) {
                        Service.Move(characterID, direction);
                  }


                  // Interact with an entity in the world.
                  // Pick up weapons in the world. E.g Picking up a Great-sword will change your melee move-set.
                  public void Interact() {
                        Service.Interact(characterID);
                  }


                  // Use Melee Attack.
                  public void UseMelee() {
                        Service.UseMeleeWeapon(characterID);
                  }


                  // Use Skill in the specified slot.
                  // 1 - Melee
                  // 2 - Gun Skill
                  // 3 -
                  // 4 - Grenade?
                  public void UseSkill(int slot) {
                        Service.UseSkill(characterID, slot);
                  }


                  public void UsePrimaryWeapon() {
                        Service.UsePrimaryWeapon("player");
                  }


                  // Use Secondary Weapon.
                  public void UseSecondaryWeapon() {
                        Service.UseSecondaryWeapon(characterID);
                  }


                  // Use Item equipped in slot
                  public void UseItem(int x, int y) {
                        Service.UseItem(characterID, x, y);
                  }


                  // Select Item in item slot.
                  public void SelectItem(int x, int y) { }


                  // Dodge an incoming attack.
                  public void Dodge(Vector3 direction) {
                        Service.Dodge(characterID, direction);
                  }


                  // Guard an incoming attack.
                  public void Guard() {
                        Service.Guard(characterID);
                  }


                  // Take Damage from an entity in the world.
                  public void TakeDamage(string culpritID) { }


                  // Set state to Alive or Dead.
                  public void SetState(bool alive) { }

            }


      }

}