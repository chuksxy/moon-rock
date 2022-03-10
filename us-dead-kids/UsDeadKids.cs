using System;
using SimpleSQL;
using UnityEngine;
using us_dead_kids.character;
using us_dead_kids.weapon;
using us_dead_kids.world;

namespace us_dead_kids {

      [RequireComponent(typeof(SimpleSQLManager))]
      public class UsDeadKids : MonoBehaviour {

            private static SimpleSQLManager _manager;


            private void Awake() {
                  _manager = GetComponent<SimpleSQLManager>();

                  if (IsNewGame()) {
                        Character.Service.Setup();
                        Weapon.Service.Init();
                  }
            }


            private bool IsNewGame() {
                  throw new NotImplementedException();
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