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
                              string     rarity
                        ) {
                              var electricMotor = company.TagNew(name)
                                                         .Tag($"level:{level}")
                                                         .Tag($"health:{health}")
                                                         .Tag($"weight:{weight}")
                                                         .Tag($"price:{price}")
                                                         .Tag($"{rarity}")
                                                         .Tag($"load:{load}V")
                                                         .Tag($"power:{power}KW")
                                                         .Tag($"efficiency:{efficiency}%");

                              materials.ToList().ForEach(material => electricMotor.AddTag($"material:{material}"));
                              electricMotor.Name = name;

                              AddProperties(health, load, price, weight, efficiency, level, electricMotor);

                              return electricMotor;
                        }


                        private static void AddProperties(
                              float      health,
                              float      load,
                              float      price,
                              float      weight,
                              float      efficiency,
                              int        level,
                              Graph.Node electricMotor
                        ) {
                              electricMotor.Props.Merge(Props.Create.Health(health, health));
                              electricMotor.Props.Merge(Props.Create.MaxLoad(load));
                              electricMotor.Props.Merge(Props.Create.Price(price));
                              electricMotor.Props.Merge(Props.Create.Efficiency(efficiency));
                              electricMotor.Props.Merge(Props.Create.Weight(weight));
                              electricMotor.Props.Merge(Props.Create.Level(level));
                              electricMotor.Props.Merge(Props.Create.Hidden(true));
                        }


                        // All By `Ogun Motors` Electric Company.
                        private static IEnumerable<Graph.Node> AllByOgunMotors() {
                              return new[] {
                                    BlueIronPhaser(Company.OgunMotors()),
                                    CopperRedPhaserV1(Company.OgunMotors()),
                                    CopperRedPhaserV2(Company.OgunMotors())
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


                        private static Graph.Node CopperRedPhaserV1(Graph.Node company) {
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


                        private static Graph.Node CopperRedPhaserV2(Graph.Node company) {
                              return Create(
                                    company,
                                    "copper-red-phaser.2",
                                    86f,
                                    45f,
                                    4.8f,
                                    208f,
                                    5.9f,
                                    58.3333f,
                                    new[] {"copper", "aluminium", "gold"},
                                    2,
                                    "uncommon"
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


                        private static Graph.Node ReynardTheSecond(Graph.Node company) {
                              return Create(
                                    company,
                                    "reynard.the-second",
                                    280f,
                                    38f,
                                    14f,
                                    1428,
                                    3.6f,
                                    68.0f,
                                    new[] {"graphene", "nanite-mesh", "gold", "copper"},
                                    3,
                                    "epic"
                              );
                        }


                        private static Graph.Node Baldwin(Graph.Node company) {
                              return Create(
                                    company,
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


                        private static Graph.Node Isengrim(Graph.Node company) {
                              return Create(
                                    company,
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


                        private static Graph.Node Noble(Graph.Node company) {
                              return Create(
                                    company,
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


                        private static Graph.Node Tybalt(Graph.Node company) {
                              return Create(
                                    company,
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


                        private static Graph.Node KingNoble(Graph.Node company) {
                              var king_noble = company.TagNew("king-noble");
                              king_noble.Name = "king-noble";
                              king_noble.Tag("level:6").Tag("graphene").Tag("weight:0.999292").Tag("exotic")
                                        .Tag("load:45V").Tag("power:4.8KW").Tag("efficiency:99.9999998");

                              return Create(
                                    company,
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