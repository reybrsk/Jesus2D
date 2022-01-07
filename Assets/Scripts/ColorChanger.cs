using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] public Color color1;
    [SerializeField] public Color color2;
    [SerializeField, Range(0f,3f)] private float colorTime;
    
    private SpriteRenderer _spriteRenderer;
    private float _targetColorTime;
    private bool _isBack;

    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void FixedUpdate()
    {
        _spriteRenderer.material.color = Color.Lerp(color1, color2, _targetColorTime / colorTime);
        
        
        if (_isBack) _targetColorTime -= Time.fixedDeltaTime;
        else _targetColorTime += Time.fixedDeltaTime;
        
        if (_targetColorTime > colorTime)
        {
            _targetColorTime = colorTime;
            _isBack = !_isBack;
        }

        if (_targetColorTime < 0f)
        {
            _targetColorTime = 0f;
            _isBack = !_isBack; 
        }
    }
}
