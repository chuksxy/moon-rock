using UnityEngine;

namespace us_dead_kids.character {

      public partial class Character {

            public static class Animation {

                  public static class Param {

                        public static readonly int MoveX = Animator.StringToHash("MoveX");
                        public static readonly int MoveY = Animator.StringToHash("MoveY");
                        public static readonly int Dodge = Animator.StringToHash("Dodge");

                  }

            }

      }

}