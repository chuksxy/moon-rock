using System;
using System.Linq;
using UnityEngine;
using us_dead_kids.inventory;

namespace us_dead_kids.character {

      public partial class Character {

            public static class Service {

                  private const string FIND_CHARACTER_BY_ID =
                        "select * " +
                        "from character " +
                        "where character.ID = ?";

                  public static void Init(SimpleSQL.SimpleSQLManager db) { }


                  // Move character specified by [ID] in a direction via the Animator.
                  public static void Move(string characterID, Vector3 direction) {
                        // Have an in memory cache for attributes that don't change too often.
                        var db = UsDeadKids.DB.Get();
                        if (db == null) {
                              Debug.LogWarning($"cannot move character [{characterID}], DB is null.");
                              return;
                        }

                        var characters = db.Query<Character>(FIND_CHARACTER_BY_ID, characterID);
                        if (characters == null || characters.Count == 0) {
                              Debug.LogWarning($"could not find character with ID [{characterID}].");
                              return;
                        }

                        characters.ForEach(c => {
                              var character = Repr.Get(c.ID);
                              var velocity  = direction * c.Speed;
                              character.GetAnimator().SetFloat(Animation.Param.MoveX, velocity.x);
                              character.GetAnimator().SetFloat(Animation.Param.MoveY, velocity.y);
                        });
                  }


                  // Use Primary Weapon equipped.
                  public static void UsePrimaryWeapon(string characterID) {
                        var weaponID = Inventory.Service.GetPrimaryWeaponID(characterID);
                        // Weapon.Fire(weaponID);
                  }

            }

      }

}