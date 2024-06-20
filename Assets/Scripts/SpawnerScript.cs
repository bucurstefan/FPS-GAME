using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject cubePrefab;
    public float spawnInterval = 10;


    void Start()
    {
        StartCoroutine(SpawnCube());
    }
    IEnumerator SpawnCube()

    {
        while (true)
        {
            Instantiate(cubePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
