using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elasmobranch.Graphs.common;
using Elasmobranch.Graphs.vertex;

namespace Elasmobranch.Graphs.graph
{
    /// <inheritdoc cref="Graph{T}" />
    public class Graph : Graph<object>
    {
    }

    /// <summary>
    ///     Represents a collection of nodes / vertices, linked together by shared edges
    /// </summary>
    public class Graph<T> : NodeStructure<T>, ICollection<Vertex<T>>
    {

        /// <summary>
        ///     Store the vertices of the graph as a dictionary, with each vertex having a unique key
        /// </summary>
        private readonly HashSet<Vertex<T>> _vertices;

        /// <summary>
        ///     Initialises a new <see cref="Graph{T}" />
        /// </summary>
        public Graph()
        {
            _vertices = new HashSet<Vertex<T>>();
        }

        /// <summary>
        ///     Initialises a new <see cref="Graph{T}" /> from an adjacency matrix array
        /// </summary>
        /// <param name="vertices"><see cref="Array" /> of vertex values</param>
        /// <param name="adjacencyMatrix">An adjacency matrix of edge weights</param>
        /// <exception cref="ArgumentException">
        ///     Thrown when the provided adjacency matrix is not compatible with the provided list
        ///     of vertices.
        /// </exception>
        public Graph(IReadOnlyCollection<T> vertices, IReadOnlyList<int[]> adjacencyMatrix)
        {
            if (vertices.Count == 0 ||                         // no vertices provided
                adjacencyMatrix.Count != vertices.Count ||    // ;
                adjacencyMatrix[0].Length != vertices.Count)
                throw new ArgumentException(
                    "Adjacency matrix must be of size N×N where N is the non-zero number of vertices provided in the first argument.");
            var tmpVertices = vertices.Select(value => new Vertex<T>(value)).ToList();
            for (var i = 0; i < vertices.Count; i++)
            for (var j = 0; j < vertices.Count; j++)
                if (adjacencyMatrix[i][j] != int.MaxValue &&
                    adjacencyMatrix[i][j] != 0)
                    tmpVertices[i].AddNeighbour(tmpVertices[j], adjacencyMatrix[i][j]);
            _vertices = new HashSet<Vertex<T>>(tmpVertices);
        }

        /// <summary>
        ///     Add a vertex to the graph
        /// </summary>
        /// <remarks>
        ///     The vertex that is added is not connected to any other vertices by default
        /// </remarks>
        /// <param name="item"></param>
        public void Add(Vertex<T> item)
        {
            if (item != null) _vertices.Add(item);
        }

        /// <summary>
        ///     Remove all vertices from the graph and start afresh
        /// </summary>
        public void Clear()
        {
            _vertices.Clear();
        }

        /// <summary>
        ///     Whether the <see cref="Graph{T}" /> contains a given vertex
        /// </summary>
        /// <param name="item">The vertex for which to check its existence</param>
        /// <returns>Whether the given vertex is in this graph</returns>
        public bool Contains(Vertex<T> item)
        {
            return _vertices.Contains(item);
        }

        public void CopyTo(Vertex<T>[] array, int arrayIndex)
        {
            _vertices.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Remove a vertex and all associated edges with it
        /// </summary>
        /// <param name="item">The vertex to remove</param>
        /// <returns>Whether the removal was successful</returns>
        public bool Remove(Vertex<T> item)
        {
            if (item == null) return false;

            foreach (var vertex in _vertices)
                vertex.RemoveNeighbour(item);

            return _vertices.Remove(item);
        }

        /// <summary>
        ///     The number of nodes in the graph
        /// </summary>
        public int Count => _vertices.Count;

        /// <summary>
        ///     The number of nodes in the graph
        /// </summary>
        public int Size => Count;

        /// <summary>
        ///     The graph is never read-only
        /// </summary>
        public bool IsReadOnly => false;

        public override IEnumerator<Vertex<T>> GetEnumerator()
        {
            return _vertices.GetEnumerator();
        }

        public override bool Contains(T item)
        {
            return this[item] != null;
        }

        /// <summary>
        ///     Get a vertex with a given value
        /// </summary>
        /// <param name="item">The value to look for</param>
        public Vertex<T> this[T item] => _vertices.FirstOrDefault(vertex => vertex.Value.Equals(item));

        public override IEnumerable<Vertex<T>> DiscoverVertices(SearchAlgorithm algorithm)
        {
            return DiscoverVertices(GetStartNode(), algorithm);
        }

        /// <summary>
        ///     Create an edge between two nodes
        /// </summary>
        /// <param name="startNode">The node to start from</param>
        /// <param name="endNode">The node to end at</param>
        /// <param name="weight">[optional] the length / weight of the edge, default = 1</param>
        /// <param name="directional">
        ///     [optional] whether the edge is directional, (i.e. route only from start to end), default =
        ///     false
        /// </param>
        public void AddEdge(Vertex<T> startNode, Vertex<T> endNode, int weight = 1, bool directional = false)
        {
            if (!Contains(startNode))
                Add(startNode);
            if (!Contains(endNode))
                Add(endNode);
            startNode.AddNeighbour(endNode, weight);
            if (!directional) endNode.AddNeighbour(startNode, weight);
        }

        /// <summary>
        ///     Get some / any node in the graph
        /// </summary>
        /// <returns>A reference to a single node somewhere in the graph</returns>
        private Vertex<T> GetStartNode()
        {
            using var enumerator = _vertices.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current;
        }

        /// <summary>
        ///     Find out whether all the nodes in the graph are connected
        /// </summary>
        /// <remarks>
        ///     A graph is connected if there is a path between any two nodes of the graph. Thus,
        ///     we can check if a graph is connected by starting at an arbitrary node and finding
        ///     out if we can reach all other nodes.
        /// </remarks>
        /// <returns>Where there is a path between any two nodes of the graph</returns>
        public bool IsConnected()
        {
            return _vertices.Count > 1 &&
                   new HashSet<Vertex<T>>(DiscoverVertices(GetStartNode(), SearchAlgorithm.BreadthFirst)).SetEquals(
                       _vertices);
        }
    }
}