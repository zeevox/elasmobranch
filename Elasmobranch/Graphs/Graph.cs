using System;
using System.Collections;
using System.Collections.Generic;

namespace Elasmobranch.Graphs
{
    /// <summary>
    ///     A graph is a collection of nodes / vertices, linked together by shared edges
    /// </summary>
    public class Graph : ICollection<Vertex>
    {
        /// <summary>
        ///     Available search algorithms to use to discover vertices on the graph
        /// </summary>
        public enum SearchAlgorithm
        {
            DepthFirst,
            BreadthFirst
        }

        /// <summary>
        ///     Store the vertices of the graph as a dictionary, with each vertex having a unique key
        /// </summary>
        private readonly Dictionary<string, Vertex> _vertices;

        /// <summary>
        ///     Initialises a new a graph
        /// </summary>
        public Graph()
        {
            _vertices = new Dictionary<string, Vertex>();
        }

        /// <summary>
        ///     Add a vertex to the graph
        /// </summary>
        /// <remarks>
        ///     The vertex that is added is not connected to any other vertices by default
        /// </remarks>
        /// <param name="vertex"></param>
        public void Add(Vertex item)
        {
            if (item != null) _vertices.Add(item.Key, item);
        }

        /// <summary>
        ///     Remove all vertices from the graph and start afresh
        /// </summary>
        public void Clear()
        {
            _vertices.Clear();
        }

        /// <summary>
        ///     Whether the graph contains a given vertex
        /// </summary>
        /// <param name="item">The vertex for which to check its existence</param>
        /// <returns>Whether the given vertex is in this graph</returns>
        public bool Contains(Vertex item)
        {
            return _vertices.ContainsValue(item);
        }

        public void CopyTo(Vertex[] array, int arrayIndex)
        {
            _vertices.Values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Remove a vertex and all associated edges with it
        /// </summary>
        /// <param name="item">The vertex to remove</param>
        /// <returns>Whether the removal was successful</returns>
        public bool Remove(Vertex item)
        {
            if (item == null) return false;

            foreach (var vertex in _vertices.Values)
                vertex.RemoveNeighbour(item);

            return _vertices.Remove(item.Key);
        }

        /// <summary>
        ///     Number of nodes in the graph
        /// </summary>
        public int Count => _vertices.Count;

        /// <summary>
        ///     The graph is never read-only
        /// </summary>
        public bool IsReadOnly => false;

        public IEnumerator<Vertex> GetEnumerator()
        {
            return _vertices.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Get a reference to the vertex by providing its unique key
        /// </summary>
        /// <param name="key">The key of the vertex to </param>
        /// <returns>A reference to the vertex with the specified key</returns>
        public Vertex GetVertex(string key)
        {
            return _vertices.GetValueOrDefault(key, null);
        }

        /// <summary>
        ///     Create an edge between two nodes
        /// </summary>
        /// <param name="startNodeKey">The node to start from</param>
        /// <param name="endNodeKey">The node to end at</param>
        /// <param name="weight">[optional] the length / weight of the edge, default = 1</param>
        /// <param name="directional">
        ///     [optional] whether the edge is directional, (i.e. route only from start to end), default =
        ///     false
        /// </param>
        public void AddEdge(string startNodeKey, string endNodeKey, int weight = 1, bool directional = false)
        {
            if (!Contains(startNodeKey))
                Add(new Vertex(startNodeKey));
            if (!Contains(endNodeKey))
                Add(new Vertex(endNodeKey));
            _vertices[startNodeKey].AddNeighbour(_vertices[endNodeKey], weight);
            if (!directional) _vertices[endNodeKey].AddNeighbour(_vertices[startNodeKey], weight);
        }

        /// <summary>
        ///     Whether a node with the specified key exists in this graph
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <returns>bool whether vertex with given key in this graph</returns>
        public bool Contains(string key)
        {
            return _vertices.ContainsKey(key);
        }

        /// <summary>
        ///     Get some / any node in the graph
        /// </summary>
        /// <returns>A reference to a single node somewhere in the graph</returns>
        private Vertex GetStartNode()
        {
            using var enumerator = _vertices.Values.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current;
        }
        
        /// <summary>
        ///     Get a list of vertices accessible from a given starting node
        /// </summary>
        /// <param name="algorithm">The algorithm to use for discovering nodes, <see cref="SearchAlgorithm" /></param>
        /// <returns>List of vertices accessible from the given starting node</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if an invalid search algorithm is provided
        ///     <see cref="SearchAlgorithm" />
        /// </exception>
        public IEnumerable<Vertex> DiscoverVertices(SearchAlgorithm algorithm)
        {
            return DiscoverVertices(GetStartNode(), algorithm);
        }

        /// <summary>
        ///     Get a list of vertices accessible from a given starting node
        /// </summary>
        /// <param name="startNode">The node from which to start the search</param>
        /// <param name="algorithm">The algorithm to use for discovering nodes, <see cref="SearchAlgorithm" /></param>
        /// <returns>List of vertices accessible from the given starting node</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if an invalid search algorithm is provided
        ///     <see cref="SearchAlgorithm" />
        /// </exception>
        public static IEnumerable<Vertex> DiscoverVertices(Vertex startNode, SearchAlgorithm algorithm)
        {
            var visited = new HashSet<Vertex> {startNode};

            switch (algorithm)
            {
                case SearchAlgorithm.DepthFirst:
                    var stack = new Stack<Vertex>();
                    stack.Push(startNode);
                    while (stack.Count > 0)
                        foreach (var neighbour in stack.Pop().GetNeighbours())
                        {
                            if (!visited.Contains(neighbour))
                                stack.Push(neighbour);
                            visited.Add(neighbour);
                        }

                    break;
                case SearchAlgorithm.BreadthFirst:
                    var queue = new Queue<Vertex>();
                    queue.Enqueue(startNode);
                    while (queue.Count > 0)
                        foreach (var neighbour in queue.Dequeue().GetNeighbours())
                        {
                            if (!visited.Contains(neighbour))
                                queue.Enqueue(neighbour);
                            visited.Add(neighbour);
                        }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
            }

            return visited;
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
                   new HashSet<Vertex>(DiscoverVertices(GetStartNode(), SearchAlgorithm.BreadthFirst)).SetEquals(
                       new HashSet<Vertex>(_vertices.Values));
        }
    }
}