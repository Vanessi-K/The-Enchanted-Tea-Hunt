using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveFollow : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private float speedModifier = 0.3f;
    private int _routeToGo;
    private float _tParam;
    private Vector3 _objectPosition;
    private bool _coroutineAllowed;
    private int _routeDirection = 1;

    void Awake()
    {
        _routeToGo = 0;
        _tParam = 0f;
        _coroutineAllowed = true;
    }

    private void OnEnable()
    {
        _routeToGo = 0;
        _tParam = 0f;
        _coroutineAllowed = true;
        AkSoundEngine.PostEvent("Play_steps", gameObject);
        AkSoundEngine.PostEvent("Play_shouts", gameObject);
    }

    void Update()
    {
        if(_coroutineAllowed) StartCoroutine(GoByTheRoute(_routeToGo));
    }
    
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        _coroutineAllowed = false;
        
        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;
        
        while (_tParam < 1)
        {
            _tParam += Time.deltaTime * speedModifier;

            if (_routeDirection > 0)
            {
                _objectPosition = MoveAlongPath(p0, p1, p2, p3);
            }
            else
            {
                _objectPosition = MoveAlongPath(p3, p2, p1, p0);
            }
            
            Vector3 direction = _objectPosition - transform.position;

            transform.position = _objectPosition;
            transform.rotation = Quaternion.LookRotation(direction, transform.up);
            yield return new WaitForEndOfFrame();
        }

        _tParam = 0f;
        _routeToGo += _routeDirection;

        if (_routeToGo >= routes.Length || _routeToGo <= 0)
        {
            _routeDirection *= -1;
            _routeToGo += _routeDirection;
        }
        
        _coroutineAllowed = true;
    }
    
    private Vector3 MoveAlongPath(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Pow(1 - _tParam, 3) * p0 +
               3 * Mathf.Pow(1 - _tParam, 2) * _tParam * p1 +
               3 * (1 - _tParam) * Mathf.Pow(_tParam, 2) * p2 +
               Mathf.Pow(_tParam, 3) * p3;
    }
}
