using System;
using System.Collections.Generic;

namespace tartarus.graph {

      public partial class Graph {

            public partial class Node {

                  public class Edge {

                        public string ID     { get; set; }
                        public float  Weight { get; set; }

                        public Node From          { get; set; }
                        public Node To            { get; set; }
                        public bool BiDirectional { get; set; }


                        // Create Single Connection with empty `to` node.
                        public static Edge CreateSingle(Node from) {
                              return Create(from, Blank());
                        }


                        public static Edge Create(Node from, Node to, float weight = 1.0f, bool bidirectional = false) {
                              var edgeID = $"connect|[{from.ID}]|to|[{to.ID}]";
                              var edge = new Edge {
                                    ID            = edgeID,
                                    Weight        = weight,
                                    From          = from,
                                    To            = to,
                                    BiDirectional = bidirectional
                              };

                              from.Add(edge);
                              to.Add(edge);

                              return edge;
                        }


                        public static string GenerateID() {
                              return $"edge|{Guid.NewGuid().ToString()}";
                        }


                        internal Edge DeepClone(Dictionary<string, Node> nodesVisited, Table<string, Edge> edgesVisited) {
                              if (edgesVisited.ContainsKey(ID)) return edgesVisited[ID];

                              var clone = new Edge {
                                    ID            = GenerateID(),
                                    Weight        = Weight,
                                    From          = From.DeepClone(nodesVisited, edgesVisited),
                                    To            = To.DeepClone(nodesVisited, edgesVisited),
                                    BiDirectional = BiDirectional
                              };
                              if (!edgesVisited.ContainsKey(ID)) {
                                    edgesVisited.Add(ID, clone);
                              }

                              return clone;
                        }

                  }

            }


      }

}