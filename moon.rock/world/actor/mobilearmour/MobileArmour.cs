using System.Linq;
using moon.rock.world.part;
using moon.rock.world.props;
using tartarus.graph;
using UnityEngine;

namespace moon.rock.world.actor.mobilearmour {


      public partial class MobileArmour {


            // Create Mobile Armour from graph.
            public static Interface Create(Graph graph) {
                  var frame = graph.FindFirstByTag("frame");
                  var head  = graph.FindFirstByTag("head");
                  var torso = graph.FindFirstByTag("torso");
                  var armL  = graph.FindFirstByTags(new[] {"arm", "left"});
                  var armR  = graph.FindFirstByTags(new[] {"arm", "right"});
                  var legs  = graph.FindFirstByTag("legs");

                  CreateHead(head);
                  CreateTorso(torso);

                  return new GameObject().AddComponent<Interface>();
            }


            // TODO:: Implement!
            public static void CreateHead(Graph graph) {
                  // For each node, look up creator.
                  graph.AllNodes()
                       .Where(node => !Props.Helpers.IsHidden(node) && !node.Name.Equals(graph.Entry.Name))
                       .Select(node => {
                             var creator = Part.Creator.GetByType("head");
                             return (node, creator);
                       })
                       .ToList()
                       .ForEach(_ => _.creator.Apply(_.node));
            }


            public static void CreateTorso(Graph graph) {
                  // For each node, look up creator.
                  graph.AllNodes()
                       .Where(node => !Props.Helpers.IsHidden(node) && !node.Name.Equals(graph.Entry.Name))
                       .Select(node => {
                             var creator = Part.Creator.GetByType("torso");
                             return (node, creator);
                       })
                       .ToList()
                       .ForEach(_ => _.creator.Apply(_.node));
            }


            public class Interface : MonoBehaviour { }

      }


}