using System;
using System.Collections.Generic;
using System.Linq;
using Elasmobranch.Graphs.common;
using Elasmobranch.Graphs.vertex;

namespace Elasmobranch.Graphs.tree
{
    public class Tree<T> : NodeStructure<T>, ICollection<Vertex<T>>

    {
        public Tree(T root)
        {
            Root = new Vertex<T>(root);
        }

        public Vertex<T> Root { get; private set; }

        public override IEnumerable<Vertex<T>> DiscoverVertices(SearchAlgorithm algorithm)
        {
            return DiscoverVertices(Root, SearchAlgorithm.DepthFirst);
        }

        public override IEnumerator<Vertex<T>> GetEnumerator()
        {
            return DiscoverVertices(SearchAlgorithm.BreadthFirst).GetEnumerator();
        }

        public override bool Contains(T item)
        {
            return DiscoverVertices(SearchAlgorithm.DepthFirst).Any(vertex => vertex.Value.Equals(item));
        }

        public void Add(Vertex<T> item)
        {
            Root = item;
        }

        public void Add(Vertex<T> parent, Vertex<T> child)
        {
            parent.AddNeighbour(child);
        }

        public void Clear()
        {
            Root = null;
        }

        public bool Contains(Vertex<T> item)
        {
            return DiscoverVertices(SearchAlgorithm.BreadthFirst).Contains(item);
        }

        public void CopyTo(Vertex<T>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Vertex<T> item)
        {
            throw new System.NotImplementedException();
        }

        public int Count => DiscoverVertices(SearchAlgorithm.BreadthFirst).Count();
        public bool IsReadOnly => false;
    }
}