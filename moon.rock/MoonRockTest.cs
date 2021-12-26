using System;
using moon.rock.world.property;
using tartarus.graph;
using UnityEngine;

namespace moon.rock {

      public class MoonRockTest : MonoBehaviour {

            private void Start() {
                  var suit = Graph.Create("red.flesh.exo.suit");

                  // A frame/chassis with health.
                  var frame = Graph.Create("frame");
                  frame.Entry.Props.Floats.Add(Property.HealthPoint.Current(100.0f));
                  frame.Entry.Props.Floats.Add(Property.HealthPoint.Max(100.0f));
                  frame.Entry.Props.Booleans.Add(Property.HealthPoint.Enabled(true));

                  var armL = Graph.Create("arm.l");
                  armL.Add(Graph.Node.New("main.processing.unit"))
                      .ConnectAll(new[] {
                            Graph.Node.New("aux.oscillator"),
                            Graph.Node.New("main.switch"),
                            Graph.Node.New("main.capacitor")
                      });
                  armL.Add(Graph.Node.New("hand.l"));

                  Debug.Log($"Graph Size of Left Arm is {armL.Entry.CountAll(7)}");

                  var armR = Graph.Create("arm.r");
                  armR.Add(Graph.Node.New("hand.r"));

                  var legL = Graph.Create("leg.l");
                  legL.Add(Graph.Create("feet.l"));

                  var legR = Graph.Create("leg.R");
                  legR.Add(Graph.Create("feet.r"));

                  suit.Add(frame);
                  suit.Add(armL);
                  suit.Add(armR);
                  suit.Add(legL);
                  suit.Add(legR);

                  Debug.Log($"Graph Size of Mecha Suit is {suit.Entry.CountAll(15)}");
            }

      }

}