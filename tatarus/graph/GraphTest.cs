using System;
using UnityEngine;

namespace tatarus.graph {

      public class GraphTest : MonoBehaviour {

            public void Start() {
                  var graphA = Graph.Create(Graph.GenerateID("test"));
                  var graphB = Graph.Create(Graph.GenerateID("test"));

                  // TODO:: Graph Encapsulation is still buggy. Fix!
                  Debug.Log(
                        $"graph Size for {graphA.ID} is [{graphA.CountNodes()}] nodes created and [{graphA.CountConnections()}] connections");
                  Debug.Log(
                        $"graph Size for {graphB.ID} is [{graphB.CountNodes()}] nodes created and [{graphB.CountConnections()}] connections");

                  graphA.Add(graphB);

                  Debug.Log(
                        $"graph Size for {graphA.ID} is [{graphA.CountNodes()}] nodes created and [{graphA.CountConnections()}] connections");
                  Debug.Log(
                        $"graph Size for {graphB.ID} is [{graphB.CountNodes()}] nodes created and [{graphB.CountConnections()}] connections");

                  var nodeA = Graph.Node.New();
                  var nodeB = Graph.Node.New();
                  var nodeC = Graph.Node.New();
                  var nodeD = Graph.Node.New();
                  var nodeE = Graph.Node.New();

                  nodeA.ConnectChain(nodeB, bidirectional: true).ConnectChain(nodeC).ConnectChain(nodeD).ConnectChain(nodeE);
                  nodeC.ConnectChain(nodeA, bidirectional: true);

                  nodeE.ConnectChain(nodeA, bidirectional: true);
                  nodeE.ConnectChain(nodeB, bidirectional: true);

                  Debug.Log(
                        $"Graph Size for {nodeA.ID} is [{nodeA.CountAll()}] nodes created and [{nodeA.Connections.Count}].");

                  Debug.Log(
                        $"Graph Size for {nodeE.ID} is [{nodeE.CountAll()}] nodes created and [{nodeE.Connections.Count}].");
            }

      }

}