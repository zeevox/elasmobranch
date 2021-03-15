using System;
using System.Linq;
using Elasmobranch.Graphs.common;
using Elasmobranch.Graphs.graph;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Tests.Graphs
{
    [TestFixture]
    public class GraphTests
    {
        [SetUp]
        public void SetUp()
        {
            _graph = new Graph<int>(Vertices, AdjacencyMatrix);
        }

        private Graph<int> _graph;

        private const int Infinity = int.MaxValue;
        private static readonly int[] Vertices = {1, 2, 3, 4, 5};

        private static readonly int[][] AdjacencyMatrix =
        {
            new[] {0, 5, Infinity, 9, 1},
            new[] {5, 0, 2, Infinity, Infinity},
            new[] {Infinity, 2, 0, 7, Infinity},
            new[] {9, Infinity, 7, 0, 2},
            new[] {1, Infinity, Infinity, 2, 0}
        };

        [Test]
        public void GraphTest()
        {
            // these two should be the same, no. of nodes in graph
            Assert.AreEqual(5, _graph.Count);
            Assert.AreEqual(5, _graph.Size);
            // check that all nodes have been connected
            Assert.AreEqual(true, _graph.IsConnected());
            // check that GetNeighbours is returning expected vertices
            Assert.That(_graph[4].GetNeighbours().Select(f => f.Value), 
                Is.EquivalentTo(new[] {1,3,5}));
            // check that all vertices are discovered starting from any node using any search algorithm
            foreach (var vertex in Vertices)
            foreach (var algorithm in (SearchAlgorithm[]) Enum.GetValues(typeof(SearchAlgorithm)))
                Assert.That(_graph.DiscoverVertices(_graph[vertex], algorithm).ToArray()
                    .Select(f => f.Value).ToArray(), Is.EquivalentTo(Vertices));
        }
    }
}