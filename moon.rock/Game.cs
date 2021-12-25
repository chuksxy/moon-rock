using System.Collections.Generic;
using System.Linq;
using moon.rock.world;
using UnityEngine;

namespace moon.rock {

      public class Game : MonoBehaviour {

            public List<World.Template> templates;


            private void Awake() {
                  var worlds = templates.Select(World.Create);
                  var first  = worlds.First();

                  if (first != null) {
                        first.Begin();
                  }
            }

      }

}