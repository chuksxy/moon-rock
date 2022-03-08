using us_dead_kids.attribute.health;
using us_dead_kids.attribute.speed;

namespace us_dead_kids.character {

      public partial class Character {

            public string ID       { get; set; }
            public string Name     { get; set; }
            public int    Priority { get; set; }


            public Health Health() {
                  return us_dead_kids.attribute.health.Health.Get(ID);
            }


            // Speed of this character in miles per hour.
            public Speed Speed() {
                  return us_dead_kids.attribute.speed.Speed.Get(ID);
            }


            public bool IsAlive() {
                  return Health().Current > 0;
            }

      }

}