using System.Collections.Generic;
using System.Linq;
using tartarus.graph;

namespace moon.rock.world.part {

      public static class Part {


            public static class Electric {

                  public static class Motor {

                        public static readonly Dictionary<string, Graph.Node> MotorsByName =
                              new Dictionary<string, Graph.Node>();


                        static Motor() {
                              All().ToList().ForEach(motor => MotorsByName.Add(motor.Name, motor));
                        }


                        // Find By Name an electric motor.
                        public static Graph.Node FindByName(string name) {
                              return !MotorsByName.ContainsKey(name) ? Graph.Node.Blank() : MotorsByName[name].DeepClone();
                        }


                        // All Electric Motors in the World.
                        private static IEnumerable<Graph.Node> All() {
                              // All Motors Manufactured by the `House Of Ogun Company`.
                              var house_of_ogun_motors =
                                    Graph.Node.New("house.of.ogun.unassigned").Tag("electric").Tag("motor")
                                         .Tag("hardware").Tag("uncommon");

                              var blue_iron_phaser = house_of_ogun_motors.TagNew("blue.iron.phaser");
                              blue_iron_phaser.Name = "blue.iron.phaser";
                              blue_iron_phaser.Tag("level:1").Tag("cast-iron").Tag("weight:2.5")
                                              .Tag("load:50V").Tag("power:2.2KW").Tag("efficiency:68.3%");

                              var copper_red_phaser_v1 = house_of_ogun_motors.TagNew("copper.red.phaser");
                              copper_red_phaser_v1.Name = "copper.red.phaser";
                              copper_red_phaser_v1.Tag("level:1").Tag("cast-iron").Tag("weight:2.5")
                                                  .Tag("load:45V").Tag("power:2.2KW").Tag("efficiency:72.65%");

                              var copper_red_phaser_v2 = house_of_ogun_motors.TagNew("copper.red.phaser");
                              copper_red_phaser_v2.Name = "copper.red.phaser.version:2";
                              copper_red_phaser_v2.Tag("level:2").Tag("cast-iron").Tag("weight:1.9")
                                                  .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:66.3333%");

                              return new[] {
                                    blue_iron_phaser,
                                    copper_red_phaser_v2
                              };
                        }

                  }


            }


      }

}