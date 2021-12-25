using System;
using System.Collections.Generic;
using System.Linq;
using moon.rock.table;
using UnityEngine;
using UnityEngine.Rendering;

namespace tatarus.graph {

      public class Graph {

            public Node EntryNode { get; set; }
            public Node ExitNode  { get; set; }

            public string ID => EntryNode.ID;


            // Generate ID with the [prefix] supplied.
            public static string GenerateID(string prefix) {
                  var noPrefix = string.IsNullOrEmpty(prefix) || "no.graph.ID".Equals(prefix);
                  var p        = noPrefix ? "default" : prefix;

                  return $"graph_{p}_{Guid.NewGuid().ToString()}";
            }


            // Create a new graph with the [ID] specified.
            public static Graph Create(string graphID) {
                  var node = new Node(graphID) {
                        Connections = new SerializedDictionary<string, Node.Connection>(),
                        Props       = Node.Properties.Empty(),
                        Position    = new Node.Point()
                  };

                  return new Graph {
                        EntryNode = node,
                        ExitNode  = node
                  };
            }


            // Add a graph within this graph.
            public void Add(Graph graph) {
                  graph.ExitNode = EntryNode;
                  graph.EntryNode.Connect(EntryNode, bidirectional: true);
            }


            public int CountConnections() {
                  var entryNodeCount = EntryNode.Connections?.Count ?? 0;
                  var exitNodeCount  = ExitNode.Connections?.Count ?? 0;
                  return EntryNode.Equals(ExitNode) ? entryNodeCount : entryNodeCount + exitNodeCount;
            }


            public int CountNodes() {
                  return EntryNode.CountAll();
            }


            // Node housing various properties for form an entity in the world.
            public class Node {

                  private static readonly Node BLANK = new Node("no.node.ID") {
                        Position    = new Point(),
                        Props       = Properties.Empty(),
                        Connections = new SerializedDictionary<string, Connection>()
                  };

                  public string                                   ID          { get; }
                  public SerializedDictionary<string, Connection> Connections { get; set; }
                  public Properties                               Props       { get; set; }
                  public bool                                     Visited     { get; set; }

                  public Point Position { get; set; }


                  public Node(string nodeID) {
                        ID = nodeID;
                  }


                  public override bool Equals(object obj) {
                        return ((Node) obj)!.ID.Equals(ID);
                  }


                  public override int GetHashCode() {
                        return ID.GetHashCode();
                  }


                  // New Node with [ID] Randomly generated.
                  public static Node New() {
                        return New(GenerateID("default"));
                  }


                  // New Node with [ID] specified.
                  public static Node New(string nodeID) {
                        return new Node(nodeID) {
                              Connections = new Table<string, Connection>(),
                              Props       = Properties.Empty(),
                              Visited     = false,
                              Position    = new Point()
                        };
                  }


                  // Empty Node with all fields initialized.
                  public static Node Blank() {
                        return new Node("no.node.ID") {
                              Position    = new Point(),
                              Props       = Properties.Empty(),
                              Connections = new SerializedDictionary<string, Connection>()
                        };
                  }


                  public bool IsBlank() {
                        return BLANK.Equals(this);
                  }


                  public Node Visit() {
                        Visited = true;
                        return this;
                  }


                  public Node Reset() {
                        Visited = false;
                        return this;
                  }


                  // Generate ID for node using the [prefix] supplied.
                  public static string GenerateID(string prefix) {
                        var noPrefix = prefix == null || "no.prefix".Equals(prefix) || prefix == "";
                        var p        = noPrefix ? "default" : prefix;
                        return $"node_{p}_{Guid.NewGuid().ToString()}";
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
                  public Connection Connect(Node node, float weight = 1.0f, bool bidirectional = false) {
                        return Connection.Create(this, node, weight, bidirectional);
                  }


                  // Add a new connection to another Node. Ignore if the connection is already present.
                  public void Add(Connection connection) {
                        if (Connections.ContainsKey(connection.ID)) return;

                        Connections ??= new SerializedDictionary<string, Connection>();
                        Connections.Add(connection.ID, connection);

                        if (connection.IsBiDirectional) {
                              connection.To.Add(connection);
                        }
                  }


                  // Add a new connection to another Node. Ignore if the connection is already present.
                  public void Add(string connectionID, float weight, Node to, bool bidirectional) {
                        if (Connections.ContainsKey(connectionID)) return;

                        var connection = new Connection {
                              ID              = connectionID,
                              Weight          = weight,
                              To              = to,
                              IsBiDirectional = bidirectional
                        };
                        Connections.Add(connectionID, connection);

                        if (bidirectional) {
                              to.Add(connection);
                        }
                  }


                  public void Remove(string connectionID) {
                        if (!Connections.ContainsKey(connectionID)) return;
                        Connections.Remove(connectionID);
                  }


                  public int CountAll() {
                        if (Visited) {
                              return 0;
                        }

                        if (IsBlank() || Connections == null || Connections.Count == 0) {
                              return 1;
                        }

                        Visited = true;
                        var count = Connections.Values
                                               .Where(c => !c.AlreadyVisited())
                                               .Select(c => c.Visit().To)
                                               .Select(node => node.CountAll())
                                               .Sum();

                        Connections.Values.ToList().ForEach(c => c.VisitCount = 0);
                        Visited = false;

                        return count + 1;
                  }


                  public class Connection {

                        public string ID     { get; set; }
                        public float  Weight { get; set; }

                        public Node From            { get; set; }
                        public Node To              { get; set; }
                        public bool IsBiDirectional { get; set; }
                        public int  VisitCount      { get; set; }


                        // Create Single Connection with empty `to` node.
                        public static Connection CreateSingle(Node from) {
                              return Create(from, Blank());
                        }


                        public static Connection Create(Node from, Node to, float weight = 1.0f, bool bidirectional = false) {
                              var connectionID = $"connect_[{from.ID}]_to_[{to.ID}]";
                              var connection = new Connection {
                                    ID              = connectionID,
                                    Weight          = weight,
                                    From            = from,
                                    To              = to,
                                    IsBiDirectional = bidirectional
                              };

                              from.Add(connection);
                              to.Add(connection);

                              return connection;
                        }


                        public static string GenerateID() {
                              return $"connection_{Guid.NewGuid().ToString()}";
                        }


                        // Record how many times this connection has been visited.
                        public Connection Visit() {
                              VisitCount++;
                              return this;
                        }


                        // Already Visited from both nodes if bidirectional or from the single node if not bidirectional.
                        public bool AlreadyVisited() {
                              return VisitCount >= 1;
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

}