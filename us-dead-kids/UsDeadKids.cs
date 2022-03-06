using SimpleSQL;
using UnityEngine;
using us_dead_kids.character;
using us_dead_kids.world;

namespace us_dead_kids {

      public class UsDeadKids : MonoBehaviour {

            public void Awake() {
                  var world       = World.Generator.Generate("", System.DateTime.Now.Ticks);
                  var protagonist = Character.Generator.Generate("player");
            }


            public static class DB {

                  public static SimpleSQLManager Manager() {
                        return null;
                  }

            }

      }

}