using System.Collections.Generic;
using System.Linq;
using moon.rock.world.company;
using moon.rock.world.props;
using tartarus.graph;

namespace moon.rock.world.part {

      public static partial class Part {

            public static class Electric {

                  public static class Motor {

                        public static readonly Dictionary<string, Graph.Node> AllMotorsByName =
                              new Dictionary<string, Graph.Node>();


                        static Motor() {
                              AllByOgunMotors().ToList().ForEach(motor => AllMotorsByName.Add(motor.Name, motor));
                              AllByPeterAndLawanson().ToList().ForEach(motor => AllMotorsByName.Add(motor.Name, motor));
                              AllByHouseOfMaalpertuus().ToList().ForEach(motor => AllMotorsByName.Add(motor.Name, motor));
                        }


                        // Find An `Electric Motor` by [name].
                        public static Graph.Node FindByName(string name) {
                              return !AllMotorsByName.ContainsKey(name) ? Graph.Node.Blank() : AllMotorsByName[name].DeepClone();
                        }


                        // Create An Electric Motor.
                        public static Graph.Node Create(
                              Graph.Node company,
                              string     name,
                              float      health,
                              float      load,
                              float      power,
                              float      price,
                              float      weight,
                              float      efficiency,
                              string[]   materials,
                              int        level,
                              string     rarity,
                              int        version = 1
                        ) {
                              var motor = company.TagNew(name)
                                                 .Tag($"level:{level}")
                                                 .Tag($"health:{health}")
                                                 .Tag($"weight:{weight}")
                                                 .Tag($"price:{price}")
                                                 .Tag($"{rarity}")
                                                 .Tag($"load:{load}V")
                                                 .Tag($"power:{power}KW")
                                                 .Tag($"efficiency:{efficiency}%")
                                                 .Tag($"version:{version}");

                              materials.ToList().ForEach(material => motor.AddTag($"material:{material}"));
                              AddProperties(motor, health, load, price, weight, efficiency, level, version);

                              motor.Name = name;
                              return motor;
                        }


                        private static void AddProperties(
                              Graph.Node motor,
                              float      health,
                              float      load,
                              float      price,
                              float      weight,
                              float      efficiency,
                              int        level,
                              int        version
                        ) {
                              motor.Props.Merge(Props.Create.Health(health, health));
                              motor.Props.Merge(Props.Create.MaxLoad(load));
                              motor.Props.Merge(Props.Create.Price(price));
                              motor.Props.Merge(Props.Create.Efficiency(efficiency));
                              motor.Props.Merge(Props.Create.Weight(weight));
                              motor.Props.Merge(Props.Create.Level(level));
                              motor.Props.Merge(Props.Create.Hidden(true));
                              motor.Props.Merge(Props.Create.Version(version));
                        }


                        // All By `Ogun Motors` Electric Company.
                        private static IEnumerable<Graph.Node> AllByOgunMotors() {
                              return new[] {
                                    BlueIronPhaser(Company.OgunMotors()),
                                    CopperRedPhaser(Company.OgunMotors()),
                                    CopperRedPhaserFire(Company.OgunMotors())
                              };
                        }


                        private static Graph.Node BlueIronPhaser(Graph.Node company) {
                              return Create(
                                    company,
                                    "blue-iron-phaser",
                                    60.0f,
                                    60.0f,
                                    2.2f,
                                    110.99f,
                                    8.2f,
                                    43.3333f,
                                    new[] {"copper", "iron", "steel"},
                                    1,
                                    "common"
                              );
                        }


                        private static Graph.Node CopperRedPhaser(Graph.Node company) {
                              return Create(
                                    company,
                                    "copper-red-phaser",
                                    71f,
                                    45.0f,
                                    2.2f,
                                    151f,
                                    6.5f,
                                    52.65f,
                                    new[] {"copper", "aluminium"},
                                    1,
                                    "common"
                              );
                        }


                        private static Graph.Node CopperRedPhaserFire(Graph.Node company) {
                              return Create(
                                    company,
                                    "copper-red-phaser.fire",
                                    86f,
                                    45f,
                                    4.8f,
                                    208f,
                                    5.9f,
                                    58.3333f,
                                    new[] {"copper", "aluminium", "gold"},
                                    2,
                                    "uncommon",
                                    2
                              );
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
                              var bruin              = Bruin(Company.HouseOfMaalpertuus());
                              var reynard_the_first  = ReynardTheFirst(Company.HouseOfMaalpertuus());
                              var reynard_the_second = ReynardTheSecond();
                              var baldwin            = Baldwin();
                              var isengrim           = Isengrim();
                              var noble              = Noble();
                              var tybalt             = Tybalt();
                              var king_noble         = KingNoble();

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
                              return Create(
                                    company,
                                    "bruin",
                                    196f,
                                    50f,
                                    10f,
                                    800f,
                                    4.7f,
                                    58.90f,
                                    new[] {"graphene", "copper"},
                                    1,
                                    "rare"
                              );
                        }


                        private static Graph.Node ReynardTheFirst(Graph.Node company) {
                              return Create(
                                    company,
                                    "reynard.the-first",
                                    220f,
                                    42f,
                                    10f,
                                    1200,
                                    3.8f,
                                    62.0f,
                                    new[] {"graphene", "nanite-mesh", "copper"},
                                    2,
                                    "rare"
                              );
                        }


                        private static Graph.Node ReynardTheSecond() {
                              return Create(
                                    Company.HouseOfMaalpertuus(),
                                    "reynard.the-second",
                                    280f,
                                    38f,
                                    14f,
                                    1428,
                                    3.6f,
                                    68.0f,
                                    new[] {"graphene", "nanite-mesh", "gold", "copper"},
                                    3,
                                    "epic",
                                    2
                              );
                        }


                        private static Graph.Node Baldwin() {
                              return Create(
                                    Company.HouseOfMaalpertuus(),
                                    "baldwin",
                                    380f,
                                    30,
                                    22f,
                                    2800,
                                    1.9f,
                                    73.3333f,
                                    new[] {"graphene", "nanite-mesh", "mercury"},
                                    4,
                                    "legendary"
                              );
                        }


                        private static Graph.Node Isengrim() {
                              return Create(
                                    Company.HouseOfMaalpertuus(),
                                    "isengrim",
                                    480f,
                                    21f,
                                    48f,
                                    6800,
                                    2.32f,
                                    82.65f,
                                    new[] {"graphene", "nanite-mesh", "titan-mesh", "mercury", "sapphire"},
                                    4,
                                    "legendary"
                              );
                        }


                        private static Graph.Node Noble() {
                              return Create(
                                    Company.HouseOfMaalpertuus(),
                                    "noble",
                                    512f,
                                    18f,
                                    48f,
                                    7200,
                                    1.8f,
                                    88f,
                                    new[] {"graphene", "nanite-mesh", "titan-mesh", "mercury", "mythril"},
                                    5,
                                    "mythic"
                              );
                        }


                        // Tybalt Electric Motor crafted from the cells of corrupted celestial tissue using Graphene as a base.
                        private static Graph.Node Tybalt() {
                              return Create(
                                    Company.HouseOfMaalpertuus(),
                                    "tybalt",
                                    1200f,
                                    80f,
                                    120f,
                                    18000,
                                    0.999292f,
                                    93.456f,
                                    new[] {"graphene", "celestial-mesh", "celestial-tissue.impure"},
                                    6,
                                    "exotic"
                              );
                        }


                        // King Noble Electric Motor crafted from the cells of celestial tissue using Graphene as a base.
                        private static Graph.Node KingNoble() {
                              return Create(
                                    Company.HouseOfMaalpertuus(),
                                    "king-noble",
                                    1286f,
                                    120f,
                                    120f,
                                    18500,
                                    0.999292f,
                                    99.9999998f,
                                    new[] {"graphene", "celestial-mesh", "celestial-tissue.pure"},
                                    6,
                                    "exotic"
                              );
                        }

                  }

            }

      }

}