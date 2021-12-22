using game.world.zone;
using UnityEngine;

namespace game.world {

      public static class World {

            public static WorldInterface Create(TWorld template) {
                  return Build(ToData(template));
            }


            public static WorldInterface Load(WorldData data) {
                  return Build(data);
            }


            private static WorldInterface Build(WorldData data) {
                  return new GameObject().AddComponent<WorldInterface>().Init(data);
            }


            private static WorldData ToData(TWorld template) {
                  return new WorldData();
            }

      }

}