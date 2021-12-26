using UnityEngine;

namespace tartarus.graph {

      public class GraphTest : MonoBehaviour {

            public void Start() {
                  var graphA = Graph.Create("graph.A");
                  var graphB = Graph.Create("graph.B");

                  // TODO:: Graph Encapsulation is still buggy. Fix!
                  /*
                  Debug.Log(
                        $"graph Size for {graphA.ID} is [{graphA.CountNodes()}] nodes created and [{graphA.CountConnections()}] connections");
                  Debug.Log(
                        $"graph Size for {graphB.ID} is [{graphB.CountNodes()}] nodes created and [{graphB.CountConnections()}] connections");

                  graphA.Add(graphB);

                  Debug.Log(
                        $"graph Size for {graphA.ID} is [{graphA.CountNodes()}] nodes created and [{graphA.CountConnections()}] connections");
                  Debug.Log(
                        $"graph Size for {graphB.ID} is [{graphB.CountNodes()}] nodes created and [{graphB.CountConnections()}] connections");
                  */

                  var nodeA = Graph.Node.New("A");
                  var nodeB = Graph.Node.New("B");
                  var nodeC = Graph.Node.New("C");
                  var nodeD = Graph.Node.New("D");
                  var nodeE = Graph.Node.New("E");

                  nodeA.ConnectChain(nodeB, bidirectional: true).ConnectChain(nodeC).ConnectChain(nodeD).ConnectChain(nodeE);
                  nodeC.ConnectChain(nodeA, bidirectional: true);

                  nodeE.ConnectChain(nodeA, bidirectional: true);
                  nodeE.ConnectChain(nodeB, bidirectional: true);

                  Debug.Log(
                        $"Graph Size for {nodeD.Name} is [{nodeD.CountAll(8)}] nodes created and [{nodeD.Edges.Count}].");
                  Debug.Log(
                        $"Graph Size for {nodeA.Name} is [{nodeA.CountAll(8)}] nodes created and [{nodeA.Edges.Count}].");
                  Debug.Log(
                        $"Size for {nodeB.Name} is [{nodeB.CountAll(8)}] nodes created and [{nodeB.Edges.Count}].");
                  Debug.Log(
                        $"Size for {nodeE.Name} is [{nodeE.CountAll(8)}] nodes created and [{nodeE.Edges.Count}].");
            }

      }

}