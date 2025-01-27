﻿using UnityEngine;
using System.Collections;

/*
 * RATING: 5 stars
 * Simple animation
 * CODE REVIEW: 4/12/22
 */
namespace PJ
{
	public class RotateAnimation2D : MonoBehaviour
	{
		/// <summary>
		/// Angles per second
		/// </summary>
		public float anglesPerSecond = 360.0f;

		protected Node2D node;

		void Start()
		{
			node = GetComponent<Node2D>();
			if (null == node)
            {
				Debug.Log("Error. Node2D required for 2D animation");
            }
		}

		void Update()
		{
			if (null == node) { return; }

			node.RotationDegreeAngle += anglesPerSecond * Time.deltaTime;
		}
	}
}
