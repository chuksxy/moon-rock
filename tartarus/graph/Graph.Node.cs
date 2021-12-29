using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Graph Node management and behaviour.
 */
namespace tartarus.graph {

      public partial class Graph {

            // Node housing various properties for an entity in the world.
            public partial class Node {

                  private static readonly Node Empty = new Node(
                        "no.node.ID",
                        "",
                        new HashSet<string>(),
                        new Table<string, Edge>(),
                        Props.Empty(),
                        new Point()
                  );


                  // Private Constructor. Do Not Expose!
                  private Node(
                        string              nodeID,
                        string              name,
                        HashSet<string>     tags,
                        Table<string, Edge> edges,
                        Props               props,
                        Point               position) {
                        Tags     = tags;
                        ID       = nodeID;
                        Name     = name;
                        Edges    = edges;
                        Props    = props;
                        Position = position;
                  }


                  public HashSet<string>     Tags     { get; set; }
                  public string              ID       { get; }
                  public string              Name     { get; set; }
                  public Table<string, Edge> Edges    { get; set; }
                  public Props               Props    { get; set; }
                  public Point               Position { get; set; }


                  // New Node with [ID] Randomly generated.
                  public static Node New(string name) {
                        return New(GenerateID("default"), name);
                  }


                  // New Node with [ID] specified.
                  public static Node New(string nodeID, string name) {
                        return new Node(
                              nodeID,
                              name,
                              new HashSet<string>(),
                              new Table<string, Edge>(),
                              Props.Empty(),
                              new Point());
                  }


                  // Empty Node with all fields initialized.
                  public static Node Blank() {
                        return new Node(
                              "no.node.ID",
                              "",
                              new HashSet<string>(),
                              new Table<string, Edge>(),
                              Props.Empty(),
                              new Point());
                  }


                  // Generate ID for node using the [prefix] supplied.
                  public static string GenerateID(string prefix) {
                        var noPrefix = prefix == null || "no.prefix".Equals(prefix) || prefix == "";
                        var p        = noPrefix ? "default" : prefix;
                        return $"node|{p}|{Guid.NewGuid().ToString()}";
                  }


                  // Is Blank with all fields initialised and set to default values.
                  public bool IsBlank() {
                        return Empty.Equals(this);
                  }


                  // Equals another node if the [ID]s are the same.
                  public override bool Equals(object obj) {
                        return ((Node) obj)!.ID.Equals(ID);
                  }


                  public override int GetHashCode() {
                        return ID.GetHashCode();
                  }


                  // Swap Place by transferring all edges from old to new node.
                  public Dictionary<string, Edge> SwapInPlace(Node oldNode, Node newNode, bool keepCurrentEdges = true) {
                        newNode.Edges = keepCurrentEdges
                              ? Table<string, Edge>.MergeRight(newNode.Edges, oldNode.Edges)
                              : oldNode.Edges;
                        oldNode.Edges = new Table<string, Edge>();

                        return new Dictionary<string, Edge>(newNode.Edges);
                  }


                  // Deep Clone a node and all nodes connected to it. Cloning a large graph could be costly!
                  public Node DeepClone() {
                        return DeepClone(new Dictionary<string, Node>(), new Table<string, Edge>());
                  }


                  // Deep Clone a Node and all nodes connected to it. Cloning a large graph could be costly.
                  private Node DeepClone(Dictionary<string, Node> nodesVisited, Table<string, Edge> edgesVisited) {
                        if (nodesVisited.ContainsKey(ID)) return nodesVisited[ID];

                        var clone = new Node(
                              GenerateID("clone"),
                              Name,
                              new HashSet<string>(Tags),
                              edgesVisited,
                              Props.DeepClone(),
                              Position.Clone()
                        );

                        nodesVisited.Add(ID, clone);

                        Edges.Values
                             .Where(edge => !edgesVisited.ContainsKey(edge.ID))
                             .ToList()
                             .ForEach(edge => edge.DeepClone(nodesVisited, edgesVisited));

                        return clone;
                  }


                  // Append a node then return it.
                  public Node Append(Node from, float weight = 1.0f, bool bidirectional = false) {
                        return Connect(from, weight, bidirectional).To;
                  }


                  // Connect All [nodes] to the current node.
                  public void ConnectAll(IEnumerable<Node> nodes, float weight = 1.0f, bool bidirectional = false) {
                        nodes.ToList().ForEach(node => Connect(node, weight, bidirectional));
                  }


                  // Connect [node] to current node.
                  public Edge Connect(Node node, float weight = 1.0f, bool bidirectional = false) {
                        return Edge.Create(this, node, weight, bidirectional);
                  }


                  // Add a new [edge] that connects to another Node. Ignore if the [edge] is a duplicate.
                  public void Add(Edge edge) {
                        if (Edges.ContainsKey(edge.ID)) return;

                        Edges ??= new Table<string, Edge>();
                        Edges.Add(edge.ID, edge);

                        if (edge.BiDirectional) edge.To.Add(edge);
                  }


                  // Add a new edge to that connects to another Node. Ignore if the edge is a duplicate.
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


                  // Remove an existing edge by it's ID.
                  public void Remove(string edgeID) {
                        if (!Edges.ContainsKey(edgeID)) return;
                        Edges.Remove(edgeID);
                  }


                  // Count All nodes up to a specific depth.
                  public int CountAll(int depth) {
                        return CountAll(depth, new HashSet<string>(), new HashSet<string>());
                  }


                  private int CountAll(int depth, ISet<string> nodesVisited, ISet<string> edgesVisited) {
                        if (depth <= 0 || nodesVisited.Contains(ID)) return 0;

                        nodesVisited.Add(ID);
                        var count = Edges.Values
                                         .Where(edge => !edgesVisited.Contains(edge.ID))
                                         .Select(edge => (edgesVisited.Add(edge.ID), edge.To))
                                         .Select(_ => _.To.CountAll(--depth, nodesVisited, edgesVisited))
                                         .Sum();
                        return 1 + count;
                  }


                  // Add a Tag to a Node so it can be queried later.
                  public void AddTag(string name) {
                        Tags ??= new HashSet<string>();

                        if (Tags.Contains(name)) return;
                        Tags.Add(name);
                  }


                  // Tag Node with a [name] so it can be queried later and return said node.
                  public Node Tag(string name) {
                        Tags ??= new HashSet<string>();

                        if (Tags.Contains(name)) return this;
                        Tags.Add(name);

                        return this;
                  }


                  // Tag Node with a [name] so it can be queried later and return said node.
                  public Node TagNew(string name) {
                        Tags ??= new HashSet<string>();

                        var copy = DeepClone();
                        return Tags.Contains(name) ? copy : copy.Tag(name);
                  }


                  // Find First Node By Tag.
                  public Node FindFirstByTag(string tag) {
                        return FindByTag(tag).First();
                  }


                  // Find First Node By Tags.
                  public Node FindFirstByTags(IEnumerable<string> tags) {
                        return FindByTags(tags).First();
                  }


                  // Find By Tag all nodes connected to this one.
                  public IEnumerable<Node> FindByTag(string tag) {
                        return FindByTags(new[] {tag}, new Dictionary<string, Node>(), new HashSet<string>());
                  }


                  // Find By Tags all nodes connected to this one.
                  public IEnumerable<Node> FindByTags(IEnumerable<string> tags) {
                        return FindByTags(tags, new Dictionary<string, Node>(), new HashSet<string>());
                  }


                  // Find By Tags all nodes connected to this one. 
                  private IEnumerable<Node> FindByTags(
                        IEnumerable<string>       tags,
                        IDictionary<string, Node> nodesVisited,
                        ISet<string>              edgesVisited) {
                        if (nodesVisited.ContainsKey(ID)) return new HashSet<Node>();

                        nodesVisited.Add(ID, this);

                        var match = tags.Select(tag => Tags.Contains(tag)).Aggregate(true, (acc, current) => acc && current);
                        var found = Edges.Values
                                         .Where(edge => !edgesVisited.Contains(edge.ID))
                                         .Select(edge => (edgesVisited.Add(edge.ID), edge.To))
                                         .Select(_ => _.To.FindByTags(tags, nodesVisited, edgesVisited))
                                         .Where(allFound => allFound.Any())
                                         .SelectMany(allFound => allFound);

                        var allFound = found.ToList();
                        if (match) allFound.Add(this);

                        return allFound;
                  }

            }

      }

}