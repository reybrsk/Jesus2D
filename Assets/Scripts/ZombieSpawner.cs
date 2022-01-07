using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private float timeBeforeSpawn;
    [SerializeField] private GameObject zombie;


    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeBeforeSpawn);
        Instantiate(zombie, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
        Destroy(gameObject);
    }
}
