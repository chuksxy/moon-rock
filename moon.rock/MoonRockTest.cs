using System.Linq;
using adam.property;
using moon.rock.world.part;
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
                        Graph.Node.New("main.capacitor").Tag("energy.storage")
                  });

                  var powerBank = Graph.Node.New("power.bank.unit");
                  powerBank.ConnectAll(new[] {
                        Graph.Node.New("main.capacitor").Tag("energy.storage"),
                        Graph.Node.New("main.capacitor").Tag("energy.storage"),
                        Graph.Node.New("main.capacitor").Tag("energy.storage")
                  });

                  Debug.Log($"Found [{powerBank.FindByTag("energy.storage").Count()}] by Tag energy.storage for power Bank.");

                  var processingUnit = circuit.Append(powerBank);

                  // A frame/chassis with armour.
                  var frame = Graph.Create("frame");
                  frame.Entry.Props.Floats.Add(Property.Armour.Current(100.0f));
                  frame.Entry.Props.Floats.Add(Property.Armour.Max(100.0f));
                  frame.Entry.Props.Booleans.Add(Property.Armour.Enabled(true));

                  // Destroying sub components should add a de-buff.
                  var armL = Graph.Create("arm.l");
                  armL.Append(circuit.DeepClone())
                      .Connect(Part.Electric.Motor.FindByName("blue.iron.phaser"));
                  armL.Add(Graph.Node.New("hand.l"));

                  Debug.Log($"Graph Size of Left Arm is {armL.Entry.CountAll(7)}");

                  var armR = Graph.Create("arm.r");
                  armR.Append(circuit.DeepClone()).Connect(Part.Electric.Motor.FindByName("baldwin"));
                  armR.Add(Graph.Node.New("hand.r"));

                  var legL = Graph.Create("leg.l");
                  legL.Add(circuit.DeepClone());
                  legL.Add(Graph.Create("feet.l"));

                  var legR = Graph.Create("leg.R");
                  legR.Add(circuit.DeepClone());
                  legR.Add(Graph.Create("feet.r"));

                  suit.Add(frame);
                  suit.Add(processingUnit);
                  suit.Add(armL);
                  suit.Add(armR);
                  suit.Add(legL);
                  suit.Add(legR);
            }

      }

}