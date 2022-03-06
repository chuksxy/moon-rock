using System.Collections.Generic;
using System.Linq;
using eden;
using moon.rock.world;
using UnityEngine;
using us_dead_kids.locomotion;

namespace moon.rock {

      public class MoonRock : MonoBehaviour {


            private void Awake() {
                  Eden.Configure.New()
                      .Register(Locomotion.New())
                      .Init();
            }

      }

}