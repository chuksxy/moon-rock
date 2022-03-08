using System;
using System.Collections.Generic;
using UnityEngine;
using us_dead_kids.weapon;

namespace us_dead_kids.character {

      public partial class Character {

            public static class Service {


                  private const string GET_CHARACTER_BY_ID =
                        "select * " +
                        "from character c " +
                        "where c.character_id = ?";


                  public static void Init() {
                        UsDeadKids.DB.Exec(db => {
                              const string sql =
                                    @"create table characters (
                                              id string not null,
                                              name string not null,
                                              priority int,
                                              primary key (id)
                                    )";
                              db.Execute(sql);
                        });
                  }


                  private static void Invoke(string characterID, Action<Character> action, bool force = false) {
                        if (Cache.Get().ContainsKey(characterID)) {
                              var character = Cache.Get()[characterID];
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
                              Cache.Put(c);
                        });
                  }


                  public static void Interact(string characterID) {
                        throw new NotImplementedException();
                  }


                  public static void Rotate(string characterID, Vector3 direction, float speed = 1.0f, bool interpolate = false) {
                        Character.Avatar.Invoke(characterID, a => {
                              var rotation = Quaternion.LookRotation(direction);
                              a.transform.rotation = interpolate
                                    ? Quaternion.Lerp(a.transform.rotation, rotation, Time.deltaTime * speed)
                                    : rotation;
                        });
                  }


                  // Move character specified by [ID] in a direction via the Animator.
                  public static void Move(string characterID, Vector3 direction) {
                        void MoveByAnimator(Character c) {
                              var velocity = direction * c.Speed().Current;
                              Avatar.Invoke(c.ID, avatar => {
                                    avatar.GetAnimator().SetFloat(Animation.Param.MoveX, velocity.x);
                                    avatar.GetAnimator().SetFloat(Animation.Param.MoveY, velocity.y);
                              });
                        }

                        Invoke(characterID, MoveByAnimator);
                  }


                  public static void Dodge(string characterID, Vector3 direction) {
                        Invoke(characterID, c => {
                              Avatar.Invoke(c.ID, avatar => {
                                    avatar.GetAnimator().SetFloat(Animation.Param.MoveX, direction.x);
                                    avatar.GetAnimator().SetFloat(Animation.Param.MoveY, direction.y);
                                    avatar.GetAnimator().SetTrigger(Animation.Param.Dodge);
                              });
                        });
                  }


                  public static void Guard(string characterID) {
                        Invoke(characterID, c => {
                              Avatar.Invoke(c.ID,
                                    avatar => { avatar.GetAnimator().SetTrigger(Animation.Param.Guard); });
                        });
                  }


                  public static void UseSkill(string characterID, int slot) {
                        throw new NotImplementedException();
                  }


                  // Use Primary Weapon equipped.
                  public static void UsePrimaryWeapon(string characterID) {
                        Weapon.UsePrimary(characterID);
                  }


                  // Use Secondary Weapon equipped.
                  public static void UseSecondaryWeapon(string characterID) {
                        Weapon.UseSecondary(characterID);
                  }


                  public static void UseMeleeWeapon(string characterID) {
                        Weapon.UseMelee(characterID);
                  }


                  public static void UseItem(string characterID, int x, int y) {
                        throw new NotImplementedException();
                  }

            }

      }

}