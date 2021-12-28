/*
 * Property behaviours and management.
 */

using tartarus.graph;

namespace moon.rock.world.props {

      public static class Props {


            public static Graph.Props.Ref Health(int current, int max) {
                  return Graph.Props.Builder.NewGroup("health")
                              .AddProperty("current", current)
                              .AddProperty("max", max)
                              .Build();
            }


            public static Graph.Props.Ref Energy(int current, int max) {
                  return Graph.Props.Builder.NewGroup("energy")
                              .AddProperty("current", current)
                              .AddProperty("max", max)
                              .Build();
            }


            public static Graph.Props.Ref Armour(int current, int max) {
                  return Graph.Props.Builder.NewGroup("armour")
                              .AddProperty("current", current)
                              .AddProperty("max", max)
                              .Build();
            }


            public static Graph.Props.Ref Efficiency(int value) {
                  return Graph.Props.Builder.NewProperty("efficiency", value);
            }


            public static Graph.Props.Ref Power(float value) {
                  return Graph.Props.Builder.NewProperty("power", value);
            }


            public static Graph.Props.Ref Load(float value) {
                  return Graph.Props.Builder.NewProperty("load", value);
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

      }

}