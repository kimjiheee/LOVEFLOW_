using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // 미리 가지고 있는 파이프 데이터
    public GameObject[] _pipeObjects;
    public int _currentObject;

    public Rigidbody _PipeRigidBody;

    public bool _isLinked = false;
    public bool _isAnimFinished = false;
    //public bool _isFilled = false;

    private float _radianX = 0;
    private float _radianY = 0;
    private float _radianZ = 0;


    // Start is called before the first frame update
    void Start()
    {
        _currentObject = 0;
        _pipeObjects[0].SetActive(true);
        _pipeObjects[1].SetActive(false);
        _pipeObjects[2].SetActive(false);
        _pipeObjects[3].SetActive(false);
        _pipeObjects[4].SetActive(false);

        _PipeRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentObject == 0)
        {
            _PipeRigidBody.isKinematic = true;
        }
        else
        {
            _PipeRigidBody.isKinematic = false;
        }
    }

    public void ChangePipeType(int arrow)
    {
        switch (arrow)
        {
            case 0:
                {
                    if (_currentObject == 4 || _currentObject == 0)
                    {
                        _pipeObjects[_currentObject].SetActive(false);
                        _currentObject = 1;
                        _pipeObjects[_currentObject].SetActive(true);
                    }
                    else
                    {
                        _pipeObjects[_currentObject].SetActive(false);
                        _currentObject++;
                        _pipeObjects[_currentObject].SetActive(true);
                    }
                }
                break;
            case 1:
                {
                    if(_currentObject == 0 || _currentObject == 1)
                    {
                        _pipeObjects[_currentObject].SetActive(false);
                        _currentObject = 4;
                        _pipeObjects[_currentObject].SetActive(true);
                    }
                    else
                    {
                        _pipeObjects[_currentObject].SetActive(false);
                        _currentObject--;
                        _pipeObjects[_currentObject].SetActive(true);
                    }
                }
                break;
            default:
                break;

        }
    }

    public void ChangePipeRadianX()
    {
        _radianX += 90;
        _pipeObjects[_currentObject].transform.rotation = Quaternion.Euler(_radianX, _radianY, _radianZ);
    }

    public void ChangePipeRadianZ()
    {
        _radianZ += 90;
        _pipeObjects[_currentObject].transform.rotation = Quaternion.Euler(_radianX, _radianY, _radianZ);
    }

    public void ChangePipeRadianY()
    {
        _radianY += 90;
        _pipeObjects[_currentObject].transform.rotation = Quaternion.Euler(_radianX, _radianY, _radianZ);
    }
}
