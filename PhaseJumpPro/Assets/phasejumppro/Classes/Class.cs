﻿using System;
using UnityEngine;

/*
 * RATING: 5 stars
 * Simple class
 * CODE REVIEW: 4/8/22
 */
namespace PJ
{
	/// <summary>
	/// Class objects are useful for defining modular behavior based on object type
	/// We might want to have properties that are defined dynamically (during runtime),
    /// but also shared by multiple objects of the same type
	/// </summary>
	public class Class
	{
		public string identifier;

		/// <summary>
        /// Set of string tags that define type properties
        /// </summary>
		public TypeTagsSet typeTags = new TypeTagsSet();

		/// <summary>
        /// Tag metadata
        /// </summary>
		public Tags tags = new Tags();

		public Class(string identifier, TypeTagsSet typeTags)
		{
			this.identifier = identifier;
			this.typeTags = typeTags;
		}
	}
}
