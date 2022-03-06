using System;
using System.Linq;
using UnityEngine;

namespace us_dead_kids.character {

      public partial class Character {

            public static class Service {

                  private const float DEFAULT_SPEED = 12.0f;

                  public static void Init(SimpleSQL.SimpleSQLManager db) { }


                  // Move character specified by [ID] in a direction via the Animator.
                  public static void Move(string characterID, Vector3 direction) {
                        var db         = UsDeadKids.DB.Get();
                        var characters = db.Query<Character>(SQL.ReadCharacter());
                        var speed      = characters.First() != null ? characters.First().Speed : DEFAULT_SPEED;
                        var velocity   = direction * speed;
                        var character  = Repr.Get(characterID);

                        character.GetAnimator().SetFloat(Animation.Param.MoveX, velocity.x);
                        character.GetAnimator().SetFloat(Animation.Param.MoveY, velocity.y);
                  }

            }

      }

}