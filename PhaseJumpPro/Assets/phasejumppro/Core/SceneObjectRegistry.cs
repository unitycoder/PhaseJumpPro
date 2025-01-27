﻿using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/*
 * RATING: 5 stars
 * It works
 * CODE REVIEW: 4/9/22
 */
namespace PJ
{
    public class SceneObjectRegistry : PJ.MonoBehaviour
    {
        [System.Serializable]
        public struct Item<T>
        {
            public string identifier;
            public T @object;
        }

        public List<Item<GameObject>> gameObjectItems;
        public List<Item<AudioClip>> audioClipItems;

        /// <summary>
        /// Internal registry for identifier -> GameObject (or Prefab) map
        /// </summary>
        protected Dictionary<string, GameObject> gameObjectRegistry = new Dictionary<string, GameObject>();
        protected Dictionary<string, AudioClip> audioClipRegistry = new Dictionary<string, AudioClip>();

        public Dictionary<string, GameObject> GameObjectRegistry => gameObjectRegistry;
        public Dictionary<string, AudioClip> AudioClipRegistry => audioClipRegistry;

        public GameObject InstantiateGameObject(string identifier, Vector3 position, Quaternion rotation)
        {
            try
            {
                var gameObject = gameObjectRegistry[identifier];
                return Instantiate(gameObject, position, rotation);
            }
            catch
            {
                return null;
            }
        }

        public void Awake()
        {
            RegisterItems(gameObjectItems, gameObjectRegistry);
            RegisterItems(audioClipItems, audioClipRegistry);
        }

        protected void RegisterItems<T>(List<Item<T>> items, Dictionary<string, T> registry)
        {
            var anyMissingIds = false;
            foreach (Item<T> item in items)
            {
                if (item.identifier.Length == 0)
                {
                    anyMissingIds = true;
                    continue;
                }

                registry[item.identifier] = item.@object;
            }

            if (anyMissingIds)
            {
                Debug.Log("SceneObjectRegistry: Missing id");
            }
        }
    }
}
