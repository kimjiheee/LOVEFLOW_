using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class PipeCube : MonoBehaviour
{
    // �� �ڽ� ������
    public GameObject _childObject;
    public Player1 _player;

    // ��ġ ���� and ��ġ ����
    public bool _isCanInstall;
    public bool _isInstalled;

    // ������ ��ġ ������ ������ ���͸���
    // 1. �� �ٶ󺸰� ������ �����Ѱ�
    // 2. �ٶ󺸰� ���� �� �����°�
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
            Debug.Log("�浹");
            // �÷��̾�� �浹���� �� ������ ����
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            Debug.Log("Ż��");
            _myRenderer.material = _none;
            // �÷��̾�� �浹�� ������ �� ������ ����
        }
    }

}
