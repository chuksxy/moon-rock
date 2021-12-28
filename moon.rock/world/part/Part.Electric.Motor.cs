using System.Collections.Generic;
using System.Linq;
using moon.rock.world.company;
using moon.rock.world.props;
using tartarus.graph;

namespace moon.rock.world.part {

      public static partial class Part {

            public static class Electric {

                  public static class Motor {

                        public static readonly Dictionary<string, Graph.Node> MotorsByName =
                              new Dictionary<string, Graph.Node>();


                        static Motor() {
                              AllByOgunMotors().ToList().ForEach(motor => MotorsByName.Add(motor.Name, motor));
                              AllByPeterAndLawanson().ToList().ForEach(motor => MotorsByName.Add(motor.Name, motor));
                              AllByHouseOfMaalpertuus().ToList().ForEach(motor => MotorsByName.Add(motor.Name, motor));
                        }


                        // Find An `Electric Motor` by [name].
                        public static Graph.Node FindByName(string name) {
                              return !MotorsByName.ContainsKey(name) ? Graph.Node.Blank() : MotorsByName[name].DeepClone();
                        }


                        // All By `Ogun Motors` Electric Company.
                        private static IEnumerable<Graph.Node> AllByOgunMotors() {
                              var ogun_motors = Company.OgunMotors();

                              var blue_iron_phaser     = BlueIronPhaser(ogun_motors);
                              var copper_red_phaser_v1 = CopperRedPhaserV1(ogun_motors);
                              var copper_red_phaser_v2 = CopperRedPhaserV2(ogun_motors);

                              return new[] {
                                    blue_iron_phaser,
                                    copper_red_phaser_v1,
                                    copper_red_phaser_v2
                              };
                        }


                        private static Graph.Node BlueIronPhaser(Graph.Node company) {
                              var blue_iron_phaser = company.TagNew("blue.iron.phaser");
                              blue_iron_phaser.Name = "blue.iron.phaser";
                              blue_iron_phaser.Tag("level:1").Tag("cast-iron").Tag("weight:8.2").Tag("common")
                                              .Tag("load:50V").Tag("power:2.2KW").Tag("efficiency:43.3333%");

                              blue_iron_phaser.Props.Merge(Props.Create.Health(60, 60));
                              blue_iron_phaser.Props.Merge(Props.Create.MaxLoad(60));
                              blue_iron_phaser.Props.Merge(Props.Create.Price(110.99f));
                              blue_iron_phaser.Props.Merge(Props.Create.Efficiency(43.3333f));
                              blue_iron_phaser.Props.Merge(Props.Create.Weight(8.2f));
                              blue_iron_phaser.Props.Merge(Props.Create.Level(1));
                              blue_iron_phaser.Props.Merge(Props.Create.Hidden(true));
                              return blue_iron_phaser;
                        }


                        private static Graph.Node CopperRedPhaserV1(Graph.Node company) {
                              var copper_red_phaser_v1 = company.TagNew("copper.red.phaser");
                              copper_red_phaser_v1.Name = "copper.red.phaser";
                              copper_red_phaser_v1.Tag("level:1").Tag("cast-iron").Tag("weight:6.5").Tag("common")
                                                  .Tag("load:45V").Tag("power:2.2KW").Tag("efficiency:52.65%");
                              return copper_red_phaser_v1;
                        }


                        private static Graph.Node CopperRedPhaserV2(Graph.Node company) {
                              var copper_red_phaser_v2 = company.TagNew("copper.red.phaser").Tag("uncommon");
                              copper_red_phaser_v2.Name = "copper.red.phaser.version:2";
                              copper_red_phaser_v2.Tag("level:2").Tag("cast-iron").Tag("weight:5.9")
                                                  .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:58.3333%");
                              return copper_red_phaser_v2;
                        }


                        // All By `Peter and Lawanson` Inc.
                        private static IEnumerable<Graph.Node> AllByPeterAndLawanson() {
                              var peter_and_lawanson = Company.PeterAndLawansonInc();

                              var lawanson_turbine_17 = peter_and_lawanson.TagNew("lawanson_turbine_17");
                              lawanson_turbine_17.Name = "lawanson_turbine_17";
                              lawanson_turbine_17.Tag("level:1").Tag("aluminium").Tag("weight:6").Tag("common")
                                                 .Tag("load:50V").Tag("power:2.2KW").Tag("efficiency:61.3%");

                              var lawamson_turbine_18 = peter_and_lawanson.TagNew("lawanson_turbine_18");
                              lawamson_turbine_18.Name = "lawanson_turbine_18";
                              lawamson_turbine_18.Tag("level:1").Tag("aluminium").Tag("weight:6").Tag("common")
                                                 .Tag("load:45V").Tag("power:2.2KW").Tag("efficiency:63.65%");

                              var lawanson_turbine_19 = peter_and_lawanson.TagNew("lawanson_turbine_19");
                              lawanson_turbine_19.Name = "lawanson_turbine_19";
                              lawanson_turbine_19.Tag("level:2").Tag("aluminium").Tag("weight:6").Tag("uncommon")
                                                 .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:66.3333%");

                              var lawawnson_titan = peter_and_lawanson.TagNew("lawanson_titan");
                              lawawnson_titan.Name = "lawanson_titan";
                              lawawnson_titan.Tag("level:2").Tag("aluminium").Tag("weight:6").Tag("rare")
                                             .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:68.3333%");

                              var lawanson_x = peter_and_lawanson.TagNew("lawanson_x");
                              lawanson_x.Name = "lawanson_x";
                              lawanson_x.Tag("level:2").Tag("aluminium").Tag("weight:6").Tag("epic")
                                        .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:71.3333%");

                              return new[] {
                                    lawanson_turbine_17,
                                    lawamson_turbine_18,
                                    lawanson_turbine_19,
                                    lawawnson_titan,
                                    lawanson_x
                              };
                        }


                        // All By `Maalpertuus Institure` Electric Motors Company.
                        private static IEnumerable<Graph.Node> AllByHouseOfMaalpertuus() {
                              var house_of_maalpertuus = Company.HouseOfMaalpertuus();

                              var bruin              = Bruin(house_of_maalpertuus);
                              var reynard_the_first  = ReynardTheFirst(house_of_maalpertuus);
                              var reynard_the_second = ReynardTheSecond(house_of_maalpertuus);
                              var baldwin            = Baldwin(house_of_maalpertuus);
                              var isengrim           = Isengrim(house_of_maalpertuus);
                              var noble              = Noble(house_of_maalpertuus);
                              var tybalt             = Tybalt(house_of_maalpertuus);
                              var king_noble         = KingNoble(house_of_maalpertuus);

                              return new[] {
                                    bruin,
                                    reynard_the_first,
                                    reynard_the_second,
                                    baldwin,
                                    isengrim,
                                    noble,
                                    tybalt,
                                    king_noble
                              };
                        }


                        private static Graph.Node Bruin(Graph.Node company) {
                              var bruin = company.TagNew("bruin");
                              bruin.Name = "bruin";
                              bruin.Tag("level:1").Tag("graphene").Tag("weight:4.7").Tag("rare")
                                   .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:58.9%");
                              return bruin;
                        }


                        private static Graph.Node ReynardTheFirst(Graph.Node company) {
                              var reynard_the_first = company.TagNew("reynard.the-first");
                              reynard_the_first.Name = "reynard";
                              reynard_the_first.Tag("level:2").Tag("graphene").Tag("weight:3.8").Tag("rare")
                                               .Tag("load:50V").Tag("power:2.2KW").Tag("efficiency:62%");
                              return reynard_the_first;
                        }


                        private static Graph.Node ReynardTheSecond(Graph.Node company) {
                              var reynard_the_second = company.TagNew("reynard.the-second");
                              reynard_the_second.Name = "reynard";
                              reynard_the_second.Tag("level:3").Tag("graphene").Tag("weight:3.6").Tag("epic")
                                                .Tag("load:50V").Tag("power:2.2KW").Tag("efficiency:68.3%");
                              return reynard_the_second;
                        }


                        private static Graph.Node Baldwin(Graph.Node company) {
                              var baldwin = company.TagNew("baldwin");
                              baldwin.Name = "baldwin";
                              baldwin.Tag("level:4").Tag("graphene").Tag("weight:1.9").Tag("legendary")
                                     .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:73.3333%");
                              return baldwin;
                        }


                        private static Graph.Node Isengrim(Graph.Node company) {
                              var isengrim = company.TagNew("isengrim");
                              isengrim.Name = "isengrim";
                              isengrim.Tag("level:4").Tag("graphene").Tag("weight:2.32").Tag("legendary")
                                      .Tag("load:45V").Tag("power:2.2KW").Tag("efficiency:82.65%");
                              return isengrim;
                        }


                        private static Graph.Node Noble(Graph.Node company) {
                              var noble = company.TagNew("noble");
                              noble.Name = "noble";
                              noble.Tag("level:5").Tag("graphene").Tag("weight:1.8").Tag("mythic")
                                   .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:88%");
                              return noble;
                        }


                        private static Graph.Node Tybalt(Graph.Node company) {
                              var tybalt = company.TagNew("tybalt");
                              tybalt.Name = "tybalt";
                              tybalt.Tag("level:6").Tag("graphene").Tag("weight:0.999292").Tag("exotic")
                                    .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:99.9999998");
                              return tybalt;
                        }


                        private static Graph.Node KingNoble(Graph.Node company) {
                              var king_noble = company.TagNew("king-noble");
                              king_noble.Name = "king-noble";
                              king_noble.Tag("level:6").Tag("graphene").Tag("weight:0.999292").Tag("exotic")
                                        .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:99.9999998");
                              return king_noble;
                        }

                  }

            }

      }

}