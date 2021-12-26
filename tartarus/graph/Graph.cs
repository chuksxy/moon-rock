using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

/*
 * Graph management, utils and behaviour.
 */
namespace tartarus.graph {

      public class Graph {

            public Node EntryNode { get; set; }
            public Node ExitNode  { get; set; }

            public string ID => EntryNode.ID;


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
                        Props    = Node.Properties.Empty(),
                        Position = new Node.Point()
                  };

                  return new Graph {
                        EntryNode = node, ExitNode = node
                  };
            }


            // Add a graph within this graph.
            public void Add(Graph graph) {
                  graph.ExitNode = EntryNode;
                  graph.EntryNode.Connect(EntryNode, bidirectional: true);
            }


            public int CountConnections() {
                  var entryNodeCount = EntryNode.Edges?.Count ?? 0;
                  var exitNodeCount  = ExitNode.Edges?.Count ?? 0;
                  return EntryNode.Equals(ExitNode) ? entryNodeCount : entryNodeCount + exitNodeCount;
            }


            public int CountNodes(int depth) {
                  return EntryNode.CountAll(depth);
            }


            // Node housing various properties for an entity in the world.
            public class Node {

                  private static readonly Node Empty = new Node("no.node.ID", "") {
                        Position = new Point(),
                        Props    = Properties.Empty(),
                        Edges    = new Table<string, Edge>()
                  };


                  public Node(string nodeID, string name) {
                        ID   = nodeID;
                        Name = name;
                  }


                  public string              ID    { get; }
                  public string              Name  { get; }
                  public Table<string, Edge> Edges { get; set; }
                  public Properties          Props { get; set; }

                  public Point Position { get; set; }


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
                              Props    = Properties.Empty(),
                              Position = new Point()
                        };
                  }


                  // Empty Node with all fields initialized.
                  public static Node Blank() {
                        return new Node("no.node.ID", "") {
                              Position = new Point(),
                              Props    = Properties.Empty(),
                              Edges    = new Table<string, Edge>()
                        };
                  }


                  public bool IsBlank() {
                        return Empty.Equals(this);
                  }


                  // Generate ID for node using the [prefix] supplied.
                  public static string GenerateID(string prefix) {
                        var noPrefix = prefix == null || "no.prefix".Equals(prefix) || prefix == "";
                        var p        = noPrefix ? "default" : prefix;
                        return $"node|{p}|{Guid.NewGuid().ToString()}";
                  }


                  // Connect Chain to node and return the node `connected to`.
                  public Node ConnectChain(Node node, float weight = 1.0f, bool bidirectional = false) {
                        return Connect(node, weight, bidirectional).To;
                  }


                  // Chain All nodes node to current node.
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

                        if (edge.IsBiDirectional) edge.To.Add(edge);
                  }


                  // Add a new edge to another Node. Ignore if the edge is already present.
                  public void Add(string edgeID, float weight, Node to, bool bidirectional) {
                        if (Edges.ContainsKey(edgeID)) return;

                        var edge = new Edge {
                              ID              = edgeID,
                              Weight          = weight,
                              To              = to,
                              IsBiDirectional = bidirectional
                        };
                        Edges.Add(edgeID, edge);

                        if (bidirectional) to.Add(edge);
                  }


                  public void Remove(string edgeID) {
                        if (!Edges.ContainsKey(edgeID)) return;
                        Edges.Remove(edgeID);
                  }


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

                        public Node From            { get; set; }
                        public Node To              { get; set; }
                        public bool IsBiDirectional { get; set; }


                        // Create Single Connection with empty `to` node.
                        public static Edge CreateSingle(Node from) {
                              return Create(from, Blank());
                        }


                        public static Edge Create(Node from, Node to, float weight = 1.0f, bool bidirectional = false) {
                              var edgeID = $"connect|[{from.ID}]|to|[{to.ID}]";
                              var edge = new Edge {
                                    ID              = edgeID,
                                    Weight          = weight,
                                    From            = from,
                                    To              = to,
                                    IsBiDirectional = bidirectional
                              };

                              from.Add(edge);
                              to.Add(edge);

                              return edge;
                        }


                        public static string GenerateID() {
                              return $"edge|{Guid.NewGuid().ToString()}";
                        }


                  }

                  public struct Properties {


                        public List<Float>   Floats   { get; set; }
                        public List<Boolean> Booleans { get; set; }
                        public List<Int>     Ints     { get; set; }
                        public List<String>  Strings  { get; set; }


                        public static Properties Empty() {
                              return new Properties {
                                    Floats   = new List<Float>(),
                                    Booleans = new List<Boolean>(),
                                    Ints     = new List<Int>(),
                                    Strings  = new List<String>()
                              };
                        }


                        public struct Boolean {

                              public string ID    { get; set; }
                              public bool   Value { get; set; }

                        }

                        public struct Int {

                              public string ID    { get; set; }
                              public bool   Value { get; set; }

                        }

                        public struct Float {

                              public string ID    { get; set; }
                              public bool   Value { get; set; }

                        }

                        public struct String {

                              public string ID    { get; set; }
                              public bool   Value { get; set; }

                        }

                  }

                  public struct Point {

                        public Vector3 World { get; set; }

                        public Vector3 Local { get; set; }

                  }

            }

      }

      public class Table<TK, TV> : SerializedDictionary<TK, TV> { }

}