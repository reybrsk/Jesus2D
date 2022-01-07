using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ControlType targetControlType;
    [SerializeField, Range(0f, 10f)] private float speed;
    [SerializeField, Range(0f, 10f)] private float jumpForce;
    
    private Animator _animator;
    private Rigidbody2D _rb;
    private float _targetScaleX;
    private Vector2 _moveDirection;
    private Player _player;
    
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int FightTrigger = Animator.StringToHash("FightTrigger");


    enum ControlType
    {
        Jump,
        Flat
    }

    


    // Start is called before the first frame update
    void Start()
    {
        _targetScaleX = transform.localScale.x;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else _animator.SetFloat(Speed, 0f);

        if (Input.GetKeyDown(KeyCode.W) && targetControlType == ControlType.Jump)
            Jump();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fight();
        }
    }


    void Fight()
    {
        StartCoroutine(TimeToFight());
        _animator.SetTrigger(FightTrigger);
        
    }

    private IEnumerator TimeToFight()
    {
        _player.IsFight = true;
        yield return new WaitForSeconds(.5f);
        _player.IsFight = false;
    }


    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce*1000);
    }

    private void Move(float horizontal, float vertical)
    {
        // Scale 
        Vector3 scale = transform.localScale;
        if (horizontal < 0f) scale.x = -_targetScaleX;
        else scale.x = _targetScaleX;
        transform.localScale = scale;
        
        // Move 
        switch (targetControlType)
        {
            case ControlType.Flat:
                _moveDirection.x = horizontal * speed * Time.deltaTime;
                _moveDirection.y = vertical * speed * Time.deltaTime;
                goto default;
                
            case ControlType.Jump:
                _moveDirection.x = horizontal * speed * Time.deltaTime;
                _moveDirection.y = 0f;
                goto default;
                
            default: 
                transform.Translate(_moveDirection);
                _animator.SetFloat(Speed, Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.y));
                break;
        }  
    }
}
