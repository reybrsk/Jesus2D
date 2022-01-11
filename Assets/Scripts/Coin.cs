using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Coin : MonoBehaviour
{
    [SerializeField] private int amount;
    public UnityEvent<int> getCoin;
    public UnityEvent getCoinClear;
    private bool _isGetting = false;



    private IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_isGetting) return;
        if (col.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.Coins += amount;
            getCoin?.Invoke(amount);
            getCoinClear?.Invoke();
            _isGetting = true;
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(Destroyer());
        }
    }

    

}
