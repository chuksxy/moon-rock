using us_dead_kids.character;
using us_dead_kids.world;

namespace us_dead_kids {

      public class UsDeadKids {

            // Start a new session.
            public void Start() {
                  var world       = World.Generator.Generate("", System.DateTime.Now.Ticks);
                  var protagonist = Character.Generator.Generate("player");
            }

      }

}