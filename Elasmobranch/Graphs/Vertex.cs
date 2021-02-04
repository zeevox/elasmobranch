using System.Collections.Generic;
using System.Linq;

namespace Elasmobranch.Graphs
{
    /// <summary>
    ///     Vertex – the fundamental unit from which graphs are formed
    /// </summary>
    public class Vertex
    {
        private readonly Dictionary<Vertex, int> _neighbours;

        /// <summary>
        ///     A key that uniquely identifies this vertex, in other terms, this is its value
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///     Initialise a new vertex
        /// </summary>
        /// <param name="key">The identifier for this vertex</param>
        public Vertex(string key)
        {
            Key = key;
            _neighbours = new Dictionary<Vertex, int>();
        }

        /// <summary>
        ///     Add a vertex that is directly accessible from this one
        /// </summary>
        /// <param name="neighbour">A reference to the adjacent vertex</param>
        /// <param name="weight">The length of the edge between this vertex and the adjacent one</param>
        public void AddNeighbour(Vertex neighbour, int weight = 1)
        {
            _neighbours.Add(neighbour, weight);
        }

        /// <summary>
        ///     Remove a neighbouring vertex from this one
        /// </summary>
        /// <param name="neighbour">The adjacent vertex to remove</param>
        /// <returns>Returns false if the provided vertex is not a neighbour</returns>
        public bool RemoveNeighbour(Vertex neighbour)
        {
            return _neighbours.Remove(neighbour);
        }

        /// <summary>
        ///     Get an iterable collection of vertices that are adjacent to this one
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vertex> GetNeighbours()
        {
            return _neighbours.Keys;
        }

        /// <summary>
        ///     Get the weight of an edge that directly connects this vertex to the given one
        /// </summary>
        /// <param name="vertex">The adjacent vertex for which to get the edge length</param>
        /// <returns>
        ///     The distance to the vertex, or <code>int.MaxValue</code> if the provided vertex is not directly accessible
        ///     from this one
        /// </returns>
        public int DistanceTo(Vertex vertex)
        {
            return vertex == null ? int.MaxValue : _neighbours.GetValueOrDefault(vertex, int.MaxValue);
        }

        public override string ToString()
        {
            return string.Format(
                $"{Key} neighbours: {string.Join(',', _neighbours.Select(kv => kv.Key.Key + ':' + kv.Value).ToArray())}");
        }
    }
}