using SimpleSQL;
using UnityEngine;
using us_dead_kids.character;
using us_dead_kids.world;

namespace us_dead_kids {

      [RequireComponent(typeof(SimpleSQLManager))]
      public class UsDeadKids : MonoBehaviour {

            private static SimpleSQLManager _manager;


            public void Awake() {
                  _manager = GetComponent<SimpleSQLManager>();
                  var world       = World.Generator.Generate("", System.DateTime.Now.Ticks);
                  var protagonist = Character.Generator.Generate("player");
            }


            public static class DB {

                  public static SimpleSQLManager Get() {
                        return _manager;
                  }

            }

      }

}