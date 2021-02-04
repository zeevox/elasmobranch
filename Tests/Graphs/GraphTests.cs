using Elasmobranch.Graphs;
using NUnit.Framework;

namespace Tests.Graphs
{
    public class GraphTests
    {
        [Test]
        public void DenseCyclicGraphTest()
        {
            var graph = new Graph();
            int[][] edges =
            {
                new[] {1, 2},
                new[] {1, 3},
                new[] {1, 4},
                new[] {2, 4},
                new[] {3, 4},
                new[] {2, 5},
                new[] {4, 5}
            };
            foreach (var edge in edges)
                graph.AddEdge(edge[0].ToString(), edge[1].ToString());

            Assert.AreEqual(5, graph.Count);

            Assert.IsTrue(graph.IsConnected());
            Assert.AreEqual(graph.DiscoverVertices(Graph.SearchAlgorithm.BreadthFirst),
                graph.DiscoverVertices(Graph.SearchAlgorithm.DepthFirst));

            for (var i = 1; i <= 5; i++)
            {
                Assert.IsTrue(graph.Contains(i.ToString()));
                Assert.IsTrue(graph.Contains(graph.GetVertex(i.ToString())));
            }

            for (var i = 6; i <= 10; i++)
            {
                Assert.IsFalse(graph.Contains(i.ToString()));
                Assert.IsFalse(graph.Contains(graph.GetVertex(i.ToString())));
            }
            
            Assert.AreEqual(int.MaxValue, graph.GetVertex(1.ToString()).DistanceTo(graph.GetVertex(5.ToString())));

            Assert.IsFalse(graph.Remove(graph.GetVertex(6.ToString())));
            Assert.IsTrue(graph.Remove(graph.GetVertex(5.ToString())));
            Assert.AreEqual(4, graph.Count);
            Assert.IsFalse(graph.Contains(5.ToString()));
            Assert.IsNull(graph.GetVertex(5.ToString()));
            Assert.AreEqual(int.MaxValue, graph.GetVertex(4.ToString()).DistanceTo(graph.GetVertex(5.ToString())));
        }
    }
}