using UnityEngine;

namespace tartarus.graph {

      public partial class Graph {

            public partial class Node {

                  public struct Point {

                        public Vector3 World { get; set; }

                        public Vector3 Local { get; set; }


                        // Clone Point.
                        public Point Clone() {
                              return new Point {
                                    World = new Vector3 {
                                          x = World.x, y = World.y, z = World.z
                                    },
                                    Local = new Vector3 {
                                          x = Local.x, y = Local.y, z = Local.z
                                    }
                              };
                        }

                  }

            }

      }

}