using System;
using System.Collections.Generic;

/*
 * Graph management, utils and behaviour.
 */
namespace tartarus.graph {

      public partial class Graph {

            public Node Entry { get; private set; }
            public Node Exit  { get; private set; }

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


            // TODO:: Implement!
            public HashSet<Node> AllNodes() {
                  return new HashSet<Node>();
            }


            // Add a graph to this graph.
            public void Add(Graph graph, float weight = 1.0f, bool bidirectional = false) {
                  Entry.Connect(graph.Entry, weight, bidirectional);
                  graph.Exit = Entry;
            }


            // Count all nodes up to a specific depth.
            public int CountAllNodes(int depth) {
                  return Entry.CountAll(depth);
            }


            // Find First Node By Tag on match.
            public Graph FindFirstByTag(string tag) {
                  return FindFirstByTags(new[] {tag});
            }


            // Find First Node By Tags on match.
            public Graph FindFirstByTags(string[] tags) {
                  var tree = Entry.FindFirstByTags(tags);
                  return Create(tree.ID, tree.Name);
            }

      }

}