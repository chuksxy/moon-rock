using eden;
using UnityEngine;

namespace moon.rock.character {

      public class Character {

            private readonly string _characterID;


            private Character(string characterID) {
                  _characterID = characterID;
            }


            public static Character New(string characterID) {
                  return new Character(characterID);
            }


            // Move character in a specific direction.
            public void Move(Vector3 direction) {
                  Eden.GetService<eden.locomotion.Eden.Locomotion>().Move(_characterID, direction, 1.0f);
            }


            // Jump in a specific direction.
            public void Jump(Vector3 direction) {
                  Eden.GetService<eden.locomotion.Eden.Locomotion>().Jump(_characterID, direction, 1.0f);
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