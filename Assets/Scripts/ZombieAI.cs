using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

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
    
    private Vector3 _rightPoint;
    private Vector3 _leftPoint;
    private float _baseLocalScale;

    private Tween _tween;
    private float _wayPointX;
    
    
    
    
    

    private void Start()
    {
        _rightPoint = new Vector3(transform.position.x + pointDistance, transform.position.y, 0);
        _leftPoint = new Vector3(transform.position.x - pointDistance, transform.position.y, 0);
        _baseLocalScale = transform.localScale.x;
        targetToAttack = GameObject.FindGameObjectWithTag("Player");
        
        if (status == Status.Patrol) GoRight();

        _tween = transform.DOMoveX(_wayPointX, 3);



    }

    private void Update()
    {
        switch (status)
        {
            case Status.Patrol:
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


    private void GoRight()
    {
        transform.DOMoveX(_rightPoint.x, 3).OnComplete(() => GoLeft()); 
        transform.localScale = new Vector3(_baseLocalScale, transform.localScale.y, transform.localScale.z);
    }
    private void GoLeft()
    {
        transform.DOMoveX(_leftPoint.x, 3).OnComplete(() => GoRight()); 
        transform.localScale = new Vector3(-_baseLocalScale, transform.localScale.y, transform.localScale.z);
    }
}
