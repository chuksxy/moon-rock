using UnityEngine;

namespace game.world {

      public static partial class World {

            public static Interface Create(Template template) {
                  return Build(template.ToData());
            }


            public static Interface Load(Data data) {
                  return Build(data);
            }


            private static Interface Build(Data data) {
                  return new GameObject().AddComponent<Interface>().Init(data);
            }

      }

}