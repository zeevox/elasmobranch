using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasmobranch.Graphs.vertex;

namespace Elasmobranch.Graphs.common
{
    /// <summary>
    ///     An abstract data structure consisting of nodes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NodeStructure<T> : IEnumerable<Vertex<T>>
    {
        /// <summary>
        ///     Whether the node structure contains a node with the given value
        /// </summary>
        /// <param name="item">The value for which to find a matching vertex</param>
        /// <returns>Whether a vertex with the given value is in this graph</returns>
        public abstract bool Contains(T item);

        /// <summary>
        ///     Get a list of vertices accessible from the root / starting node
        /// </summary>
        /// <param name="algorithm">The algorithm to use for discovering nodes, <see cref="SearchAlgorithm" /></param>
        /// <returns>List of vertices accessible from the given starting node</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if an invalid search algorithm is provided
        ///     <see cref="SearchAlgorithm" />
        /// </exception>
        public abstract IEnumerable<Vertex<T>> DiscoverVertices(SearchAlgorithm algorithm);
        
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
        public IEnumerable<Vertex<T>> DiscoverVertices(Vertex<T> startNode, SearchAlgorithm algorithm)
        {
            // use a HashSet for better performance, since we know that
            // no nodes should be marked as discovered more than once
            var visited = new HashSet<Vertex<T>> {startNode};
            
            // first node already discovered, return to user
            yield return startNode;

            // add the starting node to the processing list
            var collection = new LinkedList<Vertex<T>>();
            collection.AddLast(startNode);

            while (collection.Count > 0)
            {
                Vertex<T> vertex;

                // take a vertex from the processing list
                switch (algorithm)
                {
                    case SearchAlgorithm.DepthFirst:
                        vertex = collection.Last.ValueRef;
                        collection.RemoveLast();
                        break;
                    case SearchAlgorithm.BreadthFirst:
                        vertex = collection.First.ValueRef;
                        collection.RemoveFirst();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
                }
                        
                // for each node attached to this node
                foreach (var child in vertex.GetNeighbours())
                {
                    // this node has already been processed, abort
                    if (visited.Contains(child)) continue;

                    // mark this child node as visited
                    visited.Add(child);

                    // add the child node to the queue for processing
                    collection.AddLast(child);
                            
                    // return the found node to user
                    yield return child;
                }
            }
        }

        public abstract IEnumerator<Vertex<T>> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}