using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    static public float _currentTime;
    public bool _isGameStart = false;

    private void Start()
    {
        _currentTime = 0.0f;
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime > 3.0f)
        {
            _isGameStart = true;
        }
    }
}
