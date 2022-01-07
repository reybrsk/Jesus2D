using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Coin : MonoBehaviour
{
    [SerializeField] private int amount;
    private bool _isGetting = false;
    
    


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_isGetting) return;
        if (col.gameObject.TryGetComponent<Player>(out Player player))
        {
            _isGetting = true;
            player.Coins += amount;
            
            if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                spriteRenderer.enabled = false;

            if(TryGetComponent(out AudioSource audioSource))
                audioSource.PlayOneShot(audioSource.clip);

            if(TryGetComponent(out ParticleSystem system))
                system.Play();
        }
    }

}
