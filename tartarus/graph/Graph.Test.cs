using UnityEngine;

namespace tartarus.graph {

      public class GraphTest : MonoBehaviour {

            public void Start() {
                  var nodeA = Graph.Node.New("A");
                  var nodeB = Graph.Node.New("B");
                  var nodeC = Graph.Node.New("C");
                  var nodeD = Graph.Node.New("D");
                  var nodeE = Graph.Node.New("E");

                  nodeA.Append(nodeB, bidirectional: true).Append(nodeC).Append(nodeD).Append(nodeE);
                  nodeC.Append(nodeA, bidirectional: true);

                  nodeE.Append(nodeA, bidirectional: true);
                  nodeE.Append(nodeB, bidirectional: true);

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