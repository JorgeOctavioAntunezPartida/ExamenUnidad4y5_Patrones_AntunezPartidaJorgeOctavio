using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    public Queue<GameObject> zombiePool = new Queue<GameObject>();
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private int queueCount;

    public int CurrentSizePool
    {
        get { return zombiePool.Count; }
    }

    void Start()
    {
        for (int i = 0; i < queueCount; i++)
        {
            GameObject zombie = Instantiate(zombiePrefab);
            Zombie zombieScript = zombie.GetComponent<Zombie>();

            zombie.transform.parent = transform;

            if (zombieScript != null)
            {
                zombieScript.Id = i;
            }

            zombiePool.Enqueue(zombie);
            zombie.SetActive(false);
        }
    }

    public GameObject ObtainZombie()
    {
        if (zombiePool.Count > 0)
        {
            GameObject zombie = zombiePool.Dequeue();

            zombie.SetActive(true);
            return zombie;
        }
        return null;
    }

    public void ReturnZombie(GameObject zombie)
    {
        zombie.transform.position = transform.position;
        zombie.SetActive(false);
        zombiePool.Enqueue(zombie);
    }

    #region Eventos Susbcritos
    private void OnEnable()
    {
        Zombie.OnZombieDead += ReturnZombie;
    }

    private void OnDisable()
    {
        Zombie.OnZombieDead -= ReturnZombie;
    }
    #endregion
}