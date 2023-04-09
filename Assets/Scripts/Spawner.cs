using UnityEngine;
using System;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public List<SpawnableObject> objects = new List<SpawnableObject>();

    public SpawnableObject mom;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        if (PlayerPrefs.GetString("day", "Monday") == "Saturday" || PlayerPrefs.GetString("day", "Monday") == "Sunday")
        {
            objects.Add(mom);
        }
            
        Invoke(nameof(Spawn), UnityEngine.Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = UnityEngine.Random.value;

        foreach (var obj in objects)
        {
            if (!GameManager.Instance.isPaused && spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), UnityEngine.Random.Range(minSpawnRate, maxSpawnRate));
    }

}
