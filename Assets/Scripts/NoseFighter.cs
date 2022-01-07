using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseFighter : MonoBehaviour
{
    private Player _player;
    
    private void OnEnable()
    {
        _player = GetComponentInParent<Player>();
        if(_player == null) Debug.LogWarning("Parent Was Need <Player> Component");
    }

    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_player.IsFight) return;
        
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Kill();
        }
    }
}
