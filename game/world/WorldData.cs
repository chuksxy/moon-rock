using UnityEngine;

namespace game.world {

      public static partial class World {

            public const float MAX_WEIGHT                         = 84.0f;
            public const float MAX_MOVEMENT_SPEED                 = 142.0f;
            public const float MAX_MOVEMENT_SPEED_AFTER_MODIFIERS = 220.0f;
            public const float MAX_JUMP_SPEED                     = 18.0f;
            public const float MAX_JUMP_SPEED_AFTER_MODIFIERS     = 24.0f;

            public class Template : ScriptableObject {

                  [SerializeField] private long   seed;
                  [SerializeField] private string worldName;


                  public Data ToData() {
                        return new Data {Name = worldName, Seed = seed};
                  }

            }

            public struct Data {

                  public static readonly Data Blank = new Data();

                  public long   Seed { get; set; }
                  public string Name { get; set; }


                  public bool IsBlank() {
                        return Equals(Blank);
                  }

            }

      }

}