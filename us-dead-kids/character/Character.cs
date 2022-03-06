using SimpleSQL;

namespace us_dead_kids.character {

      public partial class Character {

            [PrimaryKey] public string ID { get; set; }

            public string Name     { get; set; }
            public int    Health   { get; set; }
            public float  Speed    { get; set; }
            public int    Priority { get; set; }


            public bool IsAlive() {
                  return Health > 0;
            }

      }

}