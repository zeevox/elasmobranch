using System;
using System.Linq;
using Elasmobranch.Graphs.common;
using Elasmobranch.Graphs.tree;
using Elasmobranch.Graphs.vertex;
using NUnit.Framework;

namespace Tests.Graphs
{
    public class TreeTests
    {
        private Tree<int> _tree;

        private static readonly int[] Nodes = {2, 7, 5, 3, 4, 6};
        
        [SetUp]
        public void SetUp()
        {
            _tree = new Tree<int>(2);
            
            var child1 = new Vertex<int>(7);
            child1.AddNeighbour(new Vertex<int>(5));
            child1.AddNeighbour(new Vertex<int>(3));
            var child2 = new Vertex<int>(4);
            child2.AddNeighbour(new Vertex<int>(6));
            _tree.Root.AddNeighbour(child1);
            _tree.Root.AddNeighbour(child2);
        }

        [Test]
        public void TreeTest()
        {
            foreach (var algorithm in (SearchAlgorithm[]) Enum.GetValues(typeof(SearchAlgorithm))) 
                Assert.That(_tree.DiscoverVertices(algorithm).ToArray()
                .Select(f => f.Value).ToArray(), Is.EquivalentTo(Nodes));
            Assert.AreEqual(Nodes.Length, _tree.Count);
            Assert.IsTrue(_tree.Contains(5));
            Assert.IsFalse(_tree.Contains(-1));
        }
    }
}