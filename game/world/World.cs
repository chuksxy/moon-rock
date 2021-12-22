using game.world.zone;
using UnityEngine;

namespace game.world {

      public static partial class World {

            public static WorldInterface Create(TWorld template) {
                  return Build(ToData(template));
            }


            public static WorldInterface Load(Data data) {
                  return Build(data);
            }


            private static WorldInterface Build(Data data) {
                  return new GameObject().AddComponent<WorldInterface>().Init(data);
            }


            private static Data ToData(TWorld template) {
                  return new Data();
            }

      }

}