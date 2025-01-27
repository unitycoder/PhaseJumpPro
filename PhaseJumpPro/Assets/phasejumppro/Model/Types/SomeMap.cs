﻿using System;
using UnityEngine;

/*
 * RATING: 5 stars
 * Simple class
 * CODE REVIEW: 3/12/22
 */
namespace PJ
{
    /// <summary>
    /// A class that maps a key to a value
    /// </summary>
    public abstract class SomeMap<Key, Value>
    {
        public abstract Value ValueFor(Key key);

        public object this[Key key]
        {
            get { return ValueFor(key); }
        }
    }
}
