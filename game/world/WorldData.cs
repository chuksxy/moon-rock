using game.world.zone;

namespace game.world {
      public struct WorldData {
            public static readonly WorldData Blank = new WorldData();

            public long       Seed  { get; set; }
            public string     Name  { get; set; }
            public ZoneData[] Zones { get; set; }

            public bool IsBlank() {
                  return Equals(Blank);
            }
      }
}