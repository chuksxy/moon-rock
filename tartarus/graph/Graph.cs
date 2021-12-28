using System;

/*
 * Graph management, utils and behaviour.
 */
namespace tartarus.graph {

      public partial class Graph {

            public Node Entry { get; set; }
            public Node Exit  { get; set; }

            public string ID => Entry.NodeID;


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
            private static Graph Create(string graphID, string name) {
                  var node = Node.New(graphID, name);

                  return new Graph {
                        Entry = node, Exit = node
                  };
            }


            // Append an existing node to the graph, then return it.
            public Node Append(Node node, float weight = 1.0f, bool bidirectional = false) {
                  Add(node, weight, bidirectional);
                  return node;
            }


            // Add an existing node to this graph.
            public void Add(Node node, float weight = 1.0f, bool bidirectional = false) {
                  Entry.Connect(node, weight, bidirectional);
            }


            // Add a graph to this graph.
            public void Add(Graph graph, float weight = 1.0f, bool bidirectional = false) {
                  Entry.Connect(graph.Entry, weight, bidirectional);
                  graph.Exit = Entry;
            }


            public int EdgeCount() {
                  var entryNodeCount = Entry.Edges?.Count ?? 0;
                  var exitNodeCount  = Exit.Edges?.Count ?? 0;
                  return Entry.Equals(Exit) ? entryNodeCount : entryNodeCount + exitNodeCount;
            }


            public int NodeCount(int depth) {
                  return Entry.CountAll(depth);
            }

      }

}