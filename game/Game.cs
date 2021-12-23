using System.Collections.Generic;
using System.Linq;
using game.world;
using UnityEngine;

namespace game {

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