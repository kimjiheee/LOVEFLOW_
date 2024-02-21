using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeTrigger : MonoBehaviour
{
    public GameObject _select;
    public Player1 _player;

    public Material _pipenone;
    public Material _pipeshow;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    //void Update()
    //{
    //    OnTriggerEnter(_collider);
    //}

    private void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
             _select = other.gameObject;
             _select.GetComponent<MeshRenderer>().material = _pipeshow;
            _player = GetComponentInParent<Player1>();
            _player._selectCube = _select.GetComponent<PipeCube>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            _select = other.gameObject;
            _select.GetComponent<MeshRenderer>().material = _pipenone;
            _player._selectCube = null;
        }
    }
}
