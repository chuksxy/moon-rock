using game.world.zone;

namespace game.world {

      public static partial class World {

            public const float MAX_WEIGHT         = 100.0f;
            public const float MAX_MOVEMENT_SPEED = 160.0f;
            public const float MAX_JUMP_SPEED     = 18.0f;

            public struct Data {

                  public static readonly Data Blank = new Data();

                  public long       Seed  { get; set; }
                  public string     Name  { get; set; }
                  public ZoneData[] Zones { get; set; }


                  public bool IsBlank() {
                        return Equals(Blank);
                  }

            }

      }

      public struct Health {

            public int      Max       { get; set; }
            public int      Current   { get; set; }
            public string[] Modifiers { get; set; }

      }

      public struct Energy {

            public float    Max       { get; set; }
            public int      Current   { get; set; }
            public string[] Modifiers { get; set; }

      }

      public interface IHaveWeight {

            float Weight { get; }

      }


}