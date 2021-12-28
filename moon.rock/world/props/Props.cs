/*
 * Property behaviours and management.
 */

using tartarus.graph;

namespace moon.rock.world.props {

      public static partial class Props {

            public static class Add {

                  public static Graph.Props.Ref Health(float current, float max, bool enabled = true) {
                        return Graph.Props.Builder.NewGroup("health")
                                    .AddProperty("current", current)
                                    .AddProperty("max", max)
                                    .AddProperty("enabled", enabled)
                                    .Next();
                  }


                  // Energy - Objects with [energy], consume it!
                  public static Graph.Props.Ref Energy(int current, int max, bool enabled) {
                        return Graph.Props.Builder.NewGroup("energy")
                                    .AddProperty("current", current)
                                    .AddProperty("max", max)
                                    .AddProperty("enabled", enabled)
                                    .Next();
                  }


                  public static Graph.Props.Ref Armour(int current, int max) {
                        return Graph.Props.Builder.NewGroup("armour")
                                    .AddProperty("current", current)
                                    .AddProperty("max", max)
                                    .Next();
                  }


                  public static Graph.Props.Ref Efficiency(float value) {
                        return Graph.Props.Builder.NewProperty("efficiency", value);
                  }


                  public static Graph.Props.Ref Power(float value) {
                        return Graph.Props.Builder.NewProperty("power", value);
                  }


                  public static Graph.Props.Ref MaxLoad(float max) {
                        return Graph.Props.Builder.NewProperty("load.max", max);
                  }


                  public static Graph.Props.Ref Level(int value) {
                        return Graph.Props.Builder.NewProperty("level", value);
                  }


                  public static Graph.Props.Ref Weight(float value) {
                        return Graph.Props.Builder.NewProperty("weight", value);
                  }


                  public static Graph.Props.Ref Rarity(string value) {
                        return Graph.Props.Builder.NewProperty("rarity", value);
                  }


                  public static Graph.Props.Ref Price(float value) {
                        return Graph.Props.Builder.NewProperty("price", value);
                  }


                  public static Graph.Props.Ref Hidden(bool value) {
                        return Graph.Props.Builder.NewProperty("hidden", value);
                  }

            }

      }

}