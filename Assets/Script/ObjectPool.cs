using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField][Range(0f, 3f)] private float WaitForSpawn = 2f;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject[] Spawn;
    [SerializeField][Range(0, 50)] private int SpawnAmount = 5;

    private void Awake()
    {
     PopulatePool();
    }
    void Start()
    {
     StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        Spawn = new GameObject[SpawnAmount];
        for (int i = 0; i < Spawn.Length; i++)
        {
            Spawn[i] = Instantiate(Enemy, transform);
            Spawn[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for(int i =0; i < Spawn.Length; i++)
        {
            if( Spawn[i].activeInHierarchy == false)
            {
                Spawn[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
       while (true)
       {
        EnableObjectInPool();
        yield return new WaitForSeconds(WaitForSpawn);
       }
    }
}
