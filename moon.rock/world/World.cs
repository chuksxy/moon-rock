using System;
using UnityEngine;

namespace moon.rock.world {

      public static partial class World {

            // Generate ID to represent an object in the world.
            public static string GenerateID() {
                  return $"world_{Guid.NewGuid().ToString().ToLower()}";
            }


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