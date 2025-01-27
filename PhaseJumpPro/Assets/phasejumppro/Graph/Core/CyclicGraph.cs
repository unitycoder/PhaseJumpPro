﻿using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * RATING: 5 stars
 * Has unit tests
 * CODE REVIEW: 4/9/22
 */
namespace PJ
{
	namespace Graph
	{
		/// <summary>
		/// Supports a graph with cycles (State machines, story sequences, etc.)
        /// The graph holds on to nodes, and the nodes have weak references to each other
		/// </summary>
		public class CyclicGraph<EdgeModel>
		{
			/// <summary>
            /// Holds strong references to nodes added to graph
            /// </summary>
			public HashSet<CyclicNode<EdgeModel>> nodes = new HashSet<CyclicNode<EdgeModel>>();

			protected WeakReference<CyclicNode<EdgeModel>> rootNode;

			public CyclicNode<EdgeModel> RootNode
            {
				get
                {
					if (null != rootNode && rootNode.TryGetTarget(out CyclicNode<EdgeModel> result))
                    {
						return result;
                    }
					return null;
                }
				set
				{
					if (null != value)
					{
						rootNode = new WeakReference<CyclicNode<EdgeModel>>(value);
						nodes.Add(value);
					}
					else
                    {
						rootNode = null;
                    }
				}
            }

			public void Remove(CyclicNode<EdgeModel> node)
            {
				var iterFromNodes = new HashSet<HashedWeakReference<SomeGraphNode<EdgeModel>>>(node.FromNodes);
				foreach (HashedWeakReference<SomeGraphNode<EdgeModel>> fromNodeReference in iterFromNodes)
                {
					if (fromNodeReference.Reference.TryGetTarget(out SomeGraphNode<EdgeModel> fromNode)) {
						fromNode.RemoveEdgesTo(node);
					}
                }

				var iterEdges = new List<SomeGraphNode<EdgeModel>.Edge>(node.Edges);
				foreach (SomeGraphNode<EdgeModel>.Edge edge in iterEdges)
                {
					node.RemoveEdge(edge);
                }

				if (node == RootNode)
                {
					RootNode = null;
                }
				nodes.Remove(node);
            }

			public void AddEdge(CyclicNode<EdgeModel> fromNode, EdgeModel model, CyclicNode<EdgeModel> toNode)
            {
				if (null == fromNode || null == toNode) { return; }

				fromNode.AddEdgeInternal(model, new WeakReferenceType<SomeGraphNode<EdgeModel>>(toNode));

				nodes.Add(fromNode);
				nodes.Add(toNode);
            }
		}
	}
}
