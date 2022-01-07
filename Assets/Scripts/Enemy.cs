using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject blood;
    public void Kill()
    {
        var rand = Random.Range(0f,1f);
        Debug.Log(rand);
        if (rand < .3f)
        {
            Instantiate(blood, transform.position + Vector3.up*0.5f, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
