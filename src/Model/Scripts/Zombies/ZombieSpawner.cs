using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private ZombiePool zombiePool;

    [Header("Puntos de spawn (5 en total)")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Tiempo entre spawns")]
    [SerializeField] private float minSpawnTime = 3f;
    [SerializeField] private float maxSpawnTime = 5f;

    [Header("Probabilidad de Equipar Decorador")]
    [SerializeField] public float helmet = 0.4f;
    [SerializeField] public float boots = 0.3f;
    [SerializeField] public float giantPotion = 0.2f;
    [SerializeField] public float gloves = 0.4f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        GameObject zombie = zombiePool.ObtainZombie();

        if (zombie == null)
        {
            Debug.LogWarning("No hay zombies disponibles en la piscina.");
            return;
        }

        Zombie z = zombie.GetComponent<Zombie>();
        z.DecorateZombie();

        zombie.transform.position = spawnPoint.position;
    }
}