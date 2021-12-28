using System.Collections;
using moon.rock.world.frame;
using tartarus.graph;
using UnityEngine;

namespace moon.rock.world.actor.mobilearmour {


      public partial class MobileArmour {

            public class Interface : MonoBehaviour { }


            // Create Mobile Armour from graph.
            public static Interface Create(Graph graph) {
                  var frame = graph.SubGraphByTag("frame");
                  var head  = graph.SubGraphByTag("head");
                  var torso = graph.SubGraphByTag("torso");
                  var armL  = graph.SubGraphByTags(new string[] {"arm", "left"});
                  var armR  = graph.SubGraphByTags(new string[] {"arm", "right"});
                  var legs  = graph.SubGraphByTag("legs");

                  return new GameObject().AddComponent<Interface>();
            }

      }


}