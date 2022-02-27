using System.Collections.Generic;
using System.Linq;
using eden;
using moon.rock.world;
using UnityEngine;

namespace moon.rock {

      public class MoonRock : MonoBehaviour {


            private void Awake() {
                  Eden.Configure.New()
                      .Register(eden.locomotion.Eden.Locomotion.New())
                      .Init();
            }

      }

}