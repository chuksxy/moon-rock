using game.world.zone;
using UnityEngine;

namespace game.world {

      public static partial class World {

            public static Interface Create(TWorld template) {
                  return Build(ToData(template));
            }


            public static Interface Load(Data data) {
                  return Build(data);
            }


            private static Interface Build(Data data) {
                  return new GameObject().AddComponent<Interface>().Init(data);
            }


            private static Data ToData(TWorld template) {
                  return new Data();
            }

      }

}