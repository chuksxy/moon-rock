/*
 * Property behaviours and management.
 */

using tartarus.graph;

namespace moon.rock.world.props {

      public static partial class Props {

            public static class Create {

                  public static Graph.Props Health(float current, float max, bool enabled = true) {
                        return Graph.Props.Builder.New()
                                    .NewGroup("health")
                                    .AddProperty("current", current)
                                    .AddProperty("max", max)
                                    .AddProperty("enabled", enabled)
                                    .Next()
                                    .Build();
                  }


                  // Energy - Objects with [energy], consume it!
                  public static Graph.Props Energy(int current, int max, bool enabled) {
                        return Graph.Props.Builder.New()
                                    .NewGroup("energy")
                                    .AddProperty("current", current)
                                    .AddProperty("max", max)
                                    .AddProperty("enabled", enabled)
                                    .Next()
                                    .Build();
                  }


                  public static Graph.Props Armour(int current, int max) {
                        return Graph.Props.Builder.New()
                                    .NewGroup("armour")
                                    .AddProperty("current", current)
                                    .AddProperty("max", max)
                                    .Next()
                                    .Build();
                  }


                  public static Graph.Props Efficiency(float value) {
                        return Graph.Props.Builder.New().NewProperty("efficiency", value).Build();
                  }


                  public static Graph.Props Power(float value) {
                        return Graph.Props.Builder.New().NewProperty("power", value).Build();
                  }


                  public static Graph.Props MaxLoad(float max) {
                        return Graph.Props.Builder.New().NewProperty("load.max", max).Build();
                  }


                  public static Graph.Props Level(int value) {
                        return Graph.Props.Builder.New().NewProperty("level", value).Build();
                  }


                  public static Graph.Props Weight(float value) {
                        return Graph.Props.Builder.New().NewProperty("weight", value).Build();
                  }


                  public static Graph.Props Rarity(string value) {
                        return Graph.Props.Builder.New().NewProperty("rarity", value).Build();
                  }


                  public static Graph.Props Price(float value) {
                        return Graph.Props.Builder.New().NewProperty("price", value).Build();
                  }


                  public static Graph.Props Hidden(bool value) {
                        return Graph.Props.Builder.New().NewProperty("hidden", value).Build();
                  }


                  public static Graph.Props Version(int value) {
                        return Graph.Props.Builder.New().NewProperty("version", value).Build();
                  }

            }

      }

}