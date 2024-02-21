using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class PipeCube : MonoBehaviour
{
    // 내 자식 파이프
    public GameObject _childObject;
    public Player1 _player;

    // 설치 가능 and 설치 여부
    public bool _isCanInstall;
    public bool _isInstalled;

    // 파이프 설치 영역을 보여줄 머터리얼
    // 1. 안 바라보고 있을때 투명한거
    // 2. 바라보고 있을 때 빛나는거
    //RaycastHit _hitData;
    //Vector3 _castDirection = transform.position - Player.transform.position;

    public Collider _collider;
    Renderer _myRenderer;
    public Material _none;
    public Material _show;

    void Start()
    {
        _childObject = transform.GetChild(0).gameObject;
        _childObject.SetActive(false);
        _collider = GetComponent<Collider>();
        _myRenderer = GetComponent<Renderer>();

        _isCanInstall = true;
        _isInstalled = false; 
    }

    void Update()
    {
        //_collider.isTrigger = true;
        if (_isInstalled)
        {
            _collider.isTrigger = false;
        }
        else
        {
            _collider.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            Debug.Log("충돌");
            // 플레이어와 충돌했을 때 수행할 동작
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            Debug.Log("탈출");
            _myRenderer.material = _none;
            // 플레이어와 충돌이 끝났을 때 수행할 동작
        }
    }

}
