using tartarus.graph;

namespace moon.rock.world.props {

      public partial class Props {

            public static class Helpers {

                  public static bool IsHidden(Graph.Node node) {
                        return node.Props.Get("hidden").GetV alue<bool>();
                  }

            }

      }

}