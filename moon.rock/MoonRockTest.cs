using System;
using moon.rock.world.property;
using tartarus.graph;
using UnityEngine;

namespace moon.rock {

      public class MoonRockTest : MonoBehaviour {

            private void Start() {
                  var suit = Graph.Create("red.flesh.exo.suit");

                  // Parts.
                  var circuit = Graph.Node.New("main.processing.unit");
                  circuit.ConnectAll(new[] {
                        Graph.Node.New("aux.oscillator"),
                        Graph.Node.New("main.switch"),
                        Graph.Node.New("main.capacitor")
                  });

                  Debug.Log($"Original Size [{circuit.CountAll(5)}]");
                  Debug.Log($"Clone Size [{circuit.DeepClone().CountAll(5)}]");

                  Debug.Log($"Original ID [{circuit.ID}]");
                  Debug.Log($"Clone ID [{circuit.DeepClone().ID}]");

                  Debug.Log($"Original Name [{circuit.Name}]");
                  Debug.Log($"Clone Name [{circuit.DeepClone().Name}]");

                  // A frame/chassis with armour.
                  var frame = Graph.Create("frame");
                  frame.Entry.Props.Floats.Add(Property.Armour.Current(100.0f));
                  frame.Entry.Props.Floats.Add(Property.Armour.Max(100.0f));
                  frame.Entry.Props.Booleans.Add(Property.Armour.Enabled(true));

                  // Destroying sub components should add a de-buff.
                  var armL = Graph.Create("arm.l");
                  armL.Add(circuit.DeepClone());
                  armL.Add(Graph.Node.New("hand.l"));

                  Debug.Log($"Graph Size of Left Arm is {armL.Entry.CountAll(7)}");

                  var armR = Graph.Create("arm.r");
                  armR.Add(circuit.DeepClone());
                  armR.Add(Graph.Node.New("hand.r"));

                  var legL = Graph.Create("leg.l");
                  legL.Add(circuit.DeepClone());
                  legL.Add(Graph.Create("feet.l"));

                  var legR = Graph.Create("leg.R");
                  legR.Add(circuit.DeepClone());
                  legR.Add(Graph.Create("feet.r"));

                  suit.Add(frame);
                  suit.Add(armL);
                  suit.Add(armR);
                  suit.Add(legL);
                  suit.Add(legR);

                  Debug.Log($"Graph Size of Mecha Suit is {suit.Entry.CountAll(100)}");
            }

      }

}