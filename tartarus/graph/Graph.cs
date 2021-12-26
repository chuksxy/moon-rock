using System;
using System.Collections.Generic;
using System.Linq;
using tartarus.props;
using UnityEngine;
using UnityEngine.Rendering;

/*
 * Graph management, utils and behaviour.
 */
namespace tartarus.graph {

      public class Graph {

            public Node Entry { get; set; }
            public Node Exit  { get; set; }

            public string ID => Entry.ID;


            // Generate ID with the [prefix] supplied.
            public static string GenerateID(string prefix) {
                  var noPrefix = string.IsNullOrEmpty(prefix) || "no.graph.ID".Equals(prefix);
                  var p        = noPrefix ? "default" : prefix;

                  return $"graph|{p}|{Guid.NewGuid().ToString()}";
            }


            // Create a new graph and generate and assign it a random [ID].
            public static Graph Create(string name) {
                  return Create(GenerateID(""), name);
            }


            // Create a new graph with the [ID] specified.
            internal static Graph Create(string graphID, string name) {
                  var node = new Node(graphID, name) {
                        Edges    = new Table<string, Node.Edge>(),
                        Props    = Props.Empty(),
                        Position = new Node.Point()
                  };

                  return new Graph {
                        Entry = node, Exit = node
                  };
            }


            // Add a graph within this graph.
            public void Add(Graph graph) {
                  Entry.Connect(graph.Entry, bidirectional: true);
                  graph.Exit = Entry;
            }


            // Add an existing node to the graph, then return it.
            public Node Add(Node node, float weight = 1.0f, bool bidirectional = false) {
                  Entry.Connect(node, weight, bidirectional);
                  return node;
            }


            public int EdgeCount() {
                  var entryNodeCount = Entry.Edges?.Count ?? 0;
                  var exitNodeCount  = Exit.Edges?.Count ?? 0;
                  return Entry.Equals(Exit) ? entryNodeCount : entryNodeCount + exitNodeCount;
            }


            public int NodeCount(int depth) {
                  return Entry.CountAll(depth);
            }


            // Node housing various properties for an entity in the world.
            public class Node {

                  private static readonly Node Empty = new Node("no.node.ID", "") {
                        Position = new Point(),
                        Props    = Props.Empty(),
                        Edges    = new Table<string, Edge>()
                  };


                  public Node(string nodeID, string name) {
                        ID   = nodeID;
                        Name = name;
                  }


                  public string              ID    { get; }
                  public string              Name  { get; }
                  public Table<string, Edge> Edges { get; set; }
                  public Props               Props { get; set; }

                  public Point Position { get; set; }


                  // Equals another node if the [ID]s are the same.
                  public override bool Equals(object obj) {
                        return ((Node) obj)!.ID.Equals(ID);
                  }


                  public override int GetHashCode() {
                        return ID.GetHashCode();
                  }


                  // New Node with [ID] Randomly generated.
                  public static Node New(string name) {
                        return New(GenerateID("default"), name);
                  }


                  // New Node with [ID] specified.
                  public static Node New(string nodeID, string name) {
                        return new Node(nodeID, name) {
                              Edges    = new Table<string, Edge>(),
                              Props    = Props.Empty(),
                              Position = new Point()
                        };
                  }


                  // Deep Clone a Node and all nodes connected to it. Cloning a large graph could be costly.
                  public Node DeepClone() {
                        return DeepClone(new Dictionary<string, Node>(), new Table<string, Edge>());
                  }


                  // Deep Clone a Node and all nodes connected to it. Cloning a large graph could be costly.
                  private Node DeepClone(Dictionary<string, Node> nodesVisited, Table<string, Edge> edgesVisited) {
                        if (nodesVisited.ContainsKey(ID)) return nodesVisited[ID];

                        var clone = new Node(GenerateID("clone"), Name) {
                              Edges    = edgesVisited,
                              Props    = Props.Clone(),
                              Position = Position.Clone()
                        };
                        nodesVisited.Add(ID, clone);

                        var edges = Edges.Values
                                         .Where(edge => !edgesVisited.ContainsKey(edge.ID))
                                         .Select(edge => edge.DeepClone(nodesVisited, edgesVisited))
                                         .ToDictionary(edgeClone => edgeClone.ID);

                        return clone;
                  }


                  // Empty Node with all fields initialized.
                  public static Node Blank() {
                        return new Node("no.node.ID", "") {
                              Position = new Point(),
                              Props    = Props.Empty(),
                              Edges    = new Table<string, Edge>()
                        };
                  }


                  // Is Blank with all fields initialised and set to default values..
                  public bool IsBlank() {
                        return Empty.Equals(this);
                  }


                  // Generate ID for node using the [prefix] supplied.
                  public static string GenerateID(string prefix) {
                        var noPrefix = prefix == null || "no.prefix".Equals(prefix) || prefix == "";
                        var p        = noPrefix ? "default" : prefix;
                        return $"node|{p}|{Guid.NewGuid().ToString()}";
                  }


                  // Connect Chain from one node to the other node and then return the other one.
                  public Node ConnectChain(Node from, float weight = 1.0f, bool bidirectional = false) {
                        return Connect(from, weight, bidirectional).To;
                  }


                  // Connect All nodes to the node supplied.
                  public void ConnectAll(IEnumerable<Node> nodes, float weight = 1.0f, bool bidirectional = false) {
                        nodes.ToList().ForEach(node => Connect(node, weight, bidirectional));
                  }


                  // Connect this node to another.
                  public Edge Connect(Node node, float weight = 1.0f, bool bidirectional = false) {
                        return Edge.Create(this, node, weight, bidirectional);
                  }


                  // Add a new edge to another Node. Ignore if the edge is already present.
                  public void Add(Edge edge) {
                        if (Edges.ContainsKey(edge.ID)) return;

                        Edges ??= new Table<string, Edge>();
                        Edges.Add(edge.ID, edge);

                        if (edge.BiDirectional) edge.To.Add(edge);
                  }


                  // Add a new edge to another Node. Ignore if the edge is already present.
                  public void Add(string edgeID, float weight, Node to, bool bidirectional) {
                        if (Edges.ContainsKey(edgeID)) return;

                        var edge = new Edge {
                              ID            = edgeID,
                              Weight        = weight,
                              To            = to,
                              BiDirectional = bidirectional
                        };
                        Edges.Add(edgeID, edge);

                        if (bidirectional) to.Add(edge);
                  }


                  // Remove an existing edge.
                  public void Remove(string edgeID) {
                        if (!Edges.ContainsKey(edgeID)) return;
                        Edges.Remove(edgeID);
                  }


                  // Count All nodes up to a specific depth.
                  public int CountAll(int depth) {
                        return CountAll(depth, new HashSet<string>(), new HashSet<string>());
                  }


                  internal int CountAll(int depth, HashSet<string> nodesVisited, HashSet<string> edgesVisited) {
                        if (depth <= 0 || nodesVisited.Contains(ID)) return 0;

                        nodesVisited.Add(ID);
                        var count = Edges.Values
                                         .Where(edge => !edgesVisited.Contains(edge.ID))
                                         .Select(edge => (edgesVisited.Add(edge.ID), edge.To))
                                         .Select(_ => _.To.CountAll(--depth, nodesVisited, edgesVisited))
                                         .Sum();
                        return 1 + count;
                  }


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

      public class Table<TK, TV> : SerializedDictionary<TK, TV> {

            public static Table<TK, TV> From(Dictionary<TK, TV> dictionary) {
                  var table = new Table<TK, TV>();
                  dictionary.ToList().ForEach(entry => table.Add(entry.Key, entry.Value));
                  return table;
            }

      }

}