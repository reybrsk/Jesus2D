using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private enum Status
    {
        Patrol,
        Attack
    }
    
    [SerializeField] private Status status;
    [SerializeField] private float speed;
    [SerializeField] private float pointDistance;
    [SerializeField] private Player player;

    
    private bool _isRight;
    private Vector3 _rightPoint;
    private Vector3 _leftPoint;
    private float _baseLocalScale;
    
    

    private void Start()
    {
        _rightPoint = new Vector3(transform.position.x + pointDistance, transform.position.y, 0);
        _leftPoint = new Vector3(transform.position.x - pointDistance, transform.position.y, 0);
        _baseLocalScale = transform.localScale.x;
    }

    private void Update()
    {
        switch (status)
        {
            case Status.Patrol:
                Patrol();
                break;
            case Status.Attack:
                Attack();
                break;
        }
    }

    private void Attack()
    {
        var translateVector = Vector3.Normalize(player.transform.position - transform.position);
        transform.Translate(translateVector * speed * Time.deltaTime);
        
    }


    private void OnDrawGizmos()
    {
        Vector3 rightPoint = new Vector3(transform.position.x + pointDistance, transform.position.y, 0);
        Vector3 leftPoint = new Vector3(transform.position.x - pointDistance, transform.position.y, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rightPoint - Vector3.up*.5f, rightPoint + Vector3.up * .5f);
        Gizmos.DrawLine(leftPoint - Vector3.up*.5f, leftPoint + Vector3.up *.5f);

    }

    void Patrol()
    {
        if (_isRight)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(_baseLocalScale, transform.localScale.y, transform.localScale.z);

        }
        else
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(-_baseLocalScale, transform.localScale.y, transform.localScale.z);
        }
        


        if (transform.position.x < _leftPoint.x) _isRight = true;
        if (transform.position.x > _rightPoint.x) _isRight = false;
    }
}
