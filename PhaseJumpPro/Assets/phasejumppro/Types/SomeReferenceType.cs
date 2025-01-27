﻿using System;

/*
 * RATING: 5 stars
 * Simple utility
 * CODE REVIEW: 4/11/22
 */
namespace PJ
{
    /// <summary>
    /// Interface for a reference type. Allows either strong or weak reference to value
    /// `ReferenceType`, not `Reference, to avoid name collision with WeakReference
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface SomeReferenceType<T>
    {
        public T Value { get; set; }
    }
}
