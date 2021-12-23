using game.world.zone;

namespace game.world {

      public static partial class World {

            public const float MAX_WEIGHT                         = 84.0f;
            public const float MAX_MOVEMENT_SPEED                 = 142.0f;
            public const float MAX_MOVEMENT_SPEED_AFTER_MODIFIERS = 220.0f;
            public const float MAX_JUMP_SPEED                     = 18.0f;
            public const float MAX_JUMP_SPEED_AFTER_MODIFIERS     = 24.0f;

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
            public float    Current   { get; set; }
            public string[] Modifiers { get; set; }

      }

      public interface IHaveWeight {

            float Weight { get; }

      }

      public interface ICanStack {

            bool CanStack { get; }

      }

}