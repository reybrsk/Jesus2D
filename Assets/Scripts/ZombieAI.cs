using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject targetToAttack;

    
    private bool _isRight;
    private Vector3 _rightPoint;
    private Vector3 _leftPoint;
    private float _baseLocalScale;
    private Vector3[] path;
    
    
    

    private void Start()
    {
        _rightPoint = new Vector3(transform.position.x + pointDistance, transform.position.y, 0);
        _leftPoint = new Vector3(transform.position.x - pointDistance, transform.position.y, 0);
        _baseLocalScale = transform.localScale.x;
        targetToAttack = GameObject.FindGameObjectWithTag("Player");
        path = new[] { _leftPoint, _rightPoint };
    }

    private void Update()
    {
        switch (status)
        {
            case Status.Patrol:
                Patrol();
                break;
            case Status.Attack:
                if (targetToAttack == null)
                {
                    status = Status.Patrol;
                    Debug.LogError("Zombie Need targetToAttack");
                    break;
                }
                Attack();
                break;

        }
    }

    private void Attack()
    {
        var translateVector = Vector3.Normalize(targetToAttack.transform.position - transform.position);
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
            transform.DOMoveX(_rightPoint.x, 3).OnComplete(() => _isRight = false);
            transform.localScale = new Vector3(_baseLocalScale, transform.localScale.y, transform.localScale.z);
        
        }
        else
        {
            transform.DOMoveX(_leftPoint.x, 3).OnComplete(() => _isRight = true);
            transform.localScale = new Vector3(-_baseLocalScale, transform.localScale.y, transform.localScale.z);
        }
        


    }
}
