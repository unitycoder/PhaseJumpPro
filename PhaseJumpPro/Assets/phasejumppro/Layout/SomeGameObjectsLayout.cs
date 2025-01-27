﻿using UnityEditor;
using UnityEngine;

/*
 * RATING: 5 stars
 * Simple abstract class
 * CODE REVIEW: 1/13/22
 */
namespace PJ
{
    /// <summary>
    /// Defines the layout of child game objects
    /// (different than Unity's Layout Group for UI, which requires a Canvas)
    /// </summary>
    public abstract class SomeGameObjectsLayout : PJ.MonoBehaviour
    {
        /// <summary>
        /// Return the bounds-size of the layout
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 Size()
        {
            return Vector3.zero;
        }

        /// <summary>
        /// Arrange child objects according to the layout
        /// </summary>
        public abstract void ApplyLayout();

        public virtual void Start()
        {
            ApplyLayout();
        }

        public virtual Vector3 LayoutPositionAt(int index)
        {
            return Vector3.zero;
        }

#if UNITY_EDITOR
        public virtual void OnValidate()
        {
            ApplyLayout();
        }

		[CustomEditor(typeof(SomeGameObjectsLayout))]
		public class Editor : UnityEditor.Editor
		{
			public override void OnInspectorGUI()
			{
				DrawDefaultInspector();

                SomeGameObjectsLayout layout = (SomeGameObjectsLayout)target;
                if (GUILayout.Button("Apply Layout"))
				{
                    layout.ApplyLayout();
				}
			}
		}
#endif
	}
}
