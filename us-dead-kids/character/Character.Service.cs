using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using us_dead_kids.inventory;

namespace us_dead_kids.character {

      public partial class Character {

            public static class Service {

                  private static readonly Dictionary<string, Character> CharacterCache = new Dictionary<string, Character>();

                  private const string FIND_CHARACTER_BY_ID =
                        "select * " +
                        "from character " +
                        "where character.ID = ?";

                  public static void Init(SimpleSQL.SimpleSQLManager db) { }


                  private static void Cache(Character c, bool evict = false) {
                        if (!CharacterCache.ContainsKey(c.ID) || evict) {
                              CharacterCache.Add(c.ID, c);
                        }
                  }


                  private static void Exec(string characterID, Action<Character> action, bool force = false) {
                        if (CharacterCache.ContainsKey(characterID)) {
                              var character = CharacterCache[characterID];
                              var execute   = force || character.IsAlive();

                              if (execute) {
                                    action.Invoke(character);
                                    return;
                              }
                        }

                        var db = UsDeadKids.DB.Get();
                        if (db == null) {
                              Debug.LogWarning($"cannot exec action for character [{characterID}], DB is null.");
                              return;
                        }

                        var characters = db.Query<Character>(FIND_CHARACTER_BY_ID, characterID);
                        if (characters == null || characters.Count == 0) {
                              Debug.LogWarning($"could not find character with ID [{characterID}].");
                              return;
                        }


                        characters.ForEach(c => {
                              if (!force && !c.IsAlive()) return;
                              
                              action.Invoke(c);
                              Cache(c);
                        });
                  }


                  // Move character specified by [ID] in a direction via the Animator.
                  public static void Move(string characterID, Vector3 direction) {
                        void MoveByAnimator(Character c) {
                              var avatar   = Avatar.Get(c.ID);
                              var velocity = direction * c.Speed;
                              avatar.GetAnimator().SetFloat(Animation.Param.MoveX, velocity.x);
                              avatar.GetAnimator().SetFloat(Animation.Param.MoveY, velocity.y);
                        }

                        Exec(characterID, MoveByAnimator);
                  }


                  public static void Dodge(string characterID, Vector3 direction) {
                        Exec(characterID, c => {
                              var avatar = Avatar.Get(c.ID);
                              avatar.GetAnimator().SetFloat(Animation.Param.MoveX, direction.x);
                              avatar.GetAnimator().SetFloat(Animation.Param.MoveY, direction.y);
                              avatar.GetAnimator().SetTrigger(Animation.Param.Dodge);
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