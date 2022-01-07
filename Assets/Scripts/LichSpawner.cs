using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LichSpawner : MonoBehaviour
{
     [SerializeField] private float timeBeforeSpawn;
     [SerializeField] private GameObject lich;


     private void Awake()
     {
          StartCoroutine(CycleSpawner());
     }

     private IEnumerator CycleSpawner()
     {
          yield return new WaitForSeconds(timeBeforeSpawn);
          Instantiate(lich, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
          Repeat();
     }

     private void Repeat()
     {
          StartCoroutine(CycleSpawner());
     }
}
