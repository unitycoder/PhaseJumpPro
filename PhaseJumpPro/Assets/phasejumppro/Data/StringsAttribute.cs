﻿using System.Collections;

/*
 * RATING: 5 stars
 * Simple type
 * CODE REVIEW: 4/11/22
 */
namespace PJ {

	/// <summary>
	/// Key-value pair of strings
	/// </summary>
	public class StringsAttribute : Attribute<string, string>
	{
		public StringsAttribute(string key, string value)
			: base(key, value)
		{
		}
	}
}
