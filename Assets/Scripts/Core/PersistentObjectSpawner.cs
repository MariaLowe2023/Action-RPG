using System;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned = false;

        void Awake()
        {
            if (hasSpawned) { return; }

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        void SpawnPersistentObjects()
        {
            GameObject persistentObjects = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObjects);
        }
    }
}
