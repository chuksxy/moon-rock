using System;
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
                  var world       = World.Generator.Generate("brave.new.world", System.DateTime.Now.Ticks);
                  var protagonist = Character.Generator.Generate("player");
            }


            public static class DB {

                  public static SimpleSQLManager Get() {
                        if (_manager == null) {
                              Debug.LogWarning("Attempting to get DB manager which is currently null. Assign it in the editor.");
                        }

                        return _manager;
                  }


                  public static void Exec(Action<SimpleSQLManager> action) {
                        if (Get() != null) {
                              action.Invoke(_manager);
                        }
                  }


                  public static R Exec<R>(Func<SimpleSQLManager, R> func) {
                        return Get() != null ? func.Invoke(_manager) : default;
                  }

            }

      }

}