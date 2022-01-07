using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class ExitTrigger : MonoBehaviour
{
    
    [SerializeField] private int coinsToExit;
    
    private ParticleSystem _ps;
    private AudioSource _audioSource;
    private bool _isWin = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isWin) return;
        if (other.gameObject.TryGetComponent<Player>(out Player player))
            if (player.Coins >= coinsToExit)
            {
                _isWin = true;
                Debug.Log("YOU WIN!!");
                
                var particleSystems = GetComponentsInChildren<ParticleSystem>();
                if (particleSystems == null) return;
                
                foreach (var system in particleSystems)
                    system.Play();
                
                _audioSource.PlayOneShot(_audioSource.clip);
            }
    }
}
