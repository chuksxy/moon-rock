using System;
using System.Linq;
using UnityEngine;

namespace us_dead_kids.character {

      public partial class Character {

            public static class Service {

                  public static void Init(SimpleSQL.SimpleSQLManager db) { }


                  // Move character specified by [ID] in a direction via the Animator.
                  public static void Move(string characterID, Vector3 direction) {
                        var db = UsDeadKids.DB.Get();
                        if (db == null) {
                              Debug.LogWarning($"Cannot move character [{characterID}], it is null.");
                              return;
                        }

                        var charactersIDs = db.Query<Character>(SQL.ReadCharacter());
                        if (charactersIDs == null || charactersIDs.Count == 0) {
                              Debug.LogWarning($"Could not find character with ID [{characterID}].");
                              return;
                        }

                        charactersIDs.ForEach(c => {
                              var character = Repr.Get(c.ID);
                              var velocity  = direction * c.Speed;
                              character.GetAnimator().SetFloat(Animation.Param.MoveX, velocity.x);
                              character.GetAnimator().SetFloat(Animation.Param.MoveY, velocity.y);
                        });
                  }

            }

      }