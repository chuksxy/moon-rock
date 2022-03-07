using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using us_dead_kids.inventory;
using us_dead_kids.weapon;

namespace us_dead_kids.character {

      public partial class Character {

            public static class Service {

                  private static readonly Dictionary<string, Character> CharacterCache = new Dictionary<string, Character>();

                  private const string GET_CHARACTER_BY_ID =
                        "select * " +
                        "from character c " +
                        "where c.character_id = ?";


                  public static void Init() {
                        UsDeadKids.DB.Exec(db => {
                              const string sql =
                                    @" create table characters (
                                              id string not null,
                                              name string not null,
                                              priority int,
                                              primary key (id)
                                    )";
                              db.Execute(sql);
                        });
                  }


                  private static void Cache(Character character, bool evict = false) {
                        if (CharacterCache.ContainsKey(character.ID) && evict) {
                              CharacterCache.Remove(character.ID);
                        }

                        if (!CharacterCache.ContainsKey(character.ID)) {
                              CharacterCache.Add(character.ID, character);
                        }
                  }


                  private static void InvokeOnCharacter(string characterID, Action<Character> action, bool force = false) {
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

                        var characters = db.Query<Character>(GET_CHARACTER_BY_ID, characterID);
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


                  private static void InvokeOnAvatar(string characterID, Action<Avatar> action) {
                        var avatar = Avatar.Get(characterID);
                        if (avatar == null) {
                              Debug.LogWarning($"avatar with ID [{characterID}] not found.");
                              return;
                        }

                        action.Invoke(avatar);
                  }


                  // Move character specified by [ID] in a direction via the Animator.
                  public static void Move(string characterID, Vector3 direction) {
                        void MoveByAnimator(Character c) {
                              var velocity = direction * c.Speed().Current;
                              InvokeOnAvatar(c.ID, avatar => {
                                    avatar.GetAnimator().SetFloat(Animation.Param.MoveX, velocity.x);
                                    avatar.GetAnimator().SetFloat(Animation.Param.MoveY, velocity.y);
                              });
                        }

                        InvokeOnCharacter(characterID, MoveByAnimator);
                  }


                  public static void Dodge(string characterID, Vector3 direction) {
                        InvokeOnCharacter(characterID, c => {
                              InvokeOnAvatar(c.ID, avatar => {
                                    avatar.GetAnimator().SetFloat(Animation.Param.MoveX, direction.x);
                                    avatar.GetAnimator().SetFloat(Animation.Param.MoveY, direction.y);
                                    avatar.GetAnimator().SetTrigger(Animation.Param.Dodge);
                              });
                        });
                  }


                  public static void Guard(string characterID) {
                        InvokeOnCharacter(characterID, c => {
                              InvokeOnAvatar(c.ID,
                                    avatar => { avatar.GetAnimator().SetTrigger(Animation.Param.Guard); });
                        });
                  }


                  // Use Primary Weapon equipped.
                  public static void UsePrimaryWeapon(string characterID) {
                        var inventories = Inventory.Service.Get(characterID);
                        inventories.ForEach(inventory => { Weapon.Use(inventory.PrimaryWeaponID); });
                  }


                  // Use Secondary Weapon equipped.
                  public static void UseSecondaryWeapon(string characterID) {
                        var inventories = Inventory.Service.Get(characterID);
                        inventories.ForEach(inventory => { Weapon.Use(inventory.SecondaryWeaponID); });
                  }


                  public static void UseMeleeWeapon(string characterID) {
                        Weapon.UseMeleeWeapon(characterID);
                  }

            }

      }

}