using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public EPipeType _pipeType = EPipeType.Empty;
    public FluidType _fluidType = FluidType.None;
    public GameObject[] _linkedPipeList;
    public int _linkedCount = 0;

    public bool _isLinked = false;
    public bool _isAnimFinished = false;
    public bool _isAnimStarted = false;

    public float _pipeFillSpeed = 5.0f;

    public MeshRenderer _meshRenderer;
    public Material _material;
    public Color _originColor;

    public int flashCount = 0;

    public enum EPipeType
    {
        Empty,
        Pipe_T,
        Pipe_Straight,
        Pipe_Elbow,
        Pipe_Cross,
        Pipe_Start1,
        Pipe_Start2,
        Pipe_Start3,
        Pipe_Start4,
        Pipe_End1,
        Pipe_End2,
        Pipe_End3,
        Pipe_End4,
    }
    public enum LinkedDirection
    {
        Forward,
        Backward,
        Rightward,
        Leftward,
    }
    public enum FluidType
    {
        None,
        Start1,
        Start2,
        Start3,
        Start4,
    }

    private void Start()
    {
        if (gameObject.name == "pipe_T")
        {
            _pipeType = EPipeType.Pipe_T;
            _linkedPipeList = new GameObject[3];
        }
        else if (gameObject.name == "pipe_straight")
        {
            _pipeType = EPipeType.Pipe_Straight;
            _linkedPipeList = new GameObject[2];
        }
        else if (gameObject.name == "pipe_90elbow" || gameObject.name == "pipe_90elbow ")
        {
            _pipeType = EPipeType.Pipe_Elbow;
            _linkedPipeList = new GameObject[2];
        }
        else if (gameObject.name == "pipe_cross")
        {
            _pipeType = EPipeType.Pipe_Cross;
            _linkedPipeList = new GameObject[4];
        }
        else if (gameObject.name == "pipe_start1")
        {
            _pipeType = EPipeType.Pipe_Start1;
            _linkedPipeList = new GameObject[1];
        }
        else if (gameObject.name == "pipe_start2")
        {
            _pipeType = EPipeType.Pipe_Start2;
            _linkedPipeList = new GameObject[1];
        }
        else if (gameObject.name == "pipe_start3")
        {
            _pipeType = EPipeType.Pipe_Start3;
            _linkedPipeList = new GameObject[1];
        }
        else if (gameObject.name == "pipe_start4")
        {
            _pipeType = EPipeType.Pipe_Start4;
            _linkedPipeList = new GameObject[1];
        }
        else if (gameObject.name == "pipe_end1")
        {
            _pipeType = EPipeType.Pipe_End1;
            _linkedPipeList = new GameObject[1];
        }
        else if (gameObject.name == "pipe_end2")
        {
            _pipeType = EPipeType.Pipe_End2;
            _linkedPipeList = new GameObject[1];
        }
        else if (gameObject.name == "pipe_end3")
        {
            _pipeType = EPipeType.Pipe_End3;
            _linkedPipeList = new GameObject[1];
        }
        else if (gameObject.name == "pipe_end4")
        {
            _pipeType = EPipeType.Pipe_End4;
            _linkedPipeList = new GameObject[1];
        }

        //Material
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        _originColor = _material.color;

    }

    private void Update()
    {
        int count = 0;
        for (int i = 0; i < _linkedPipeList.Length; i++)
        {
            if (_linkedPipeList[i] != null && _linkedPipeList[i].activeSelf == false)
            {
                _linkedPipeList[i] = null;
            }

            if (_linkedPipeList[i] != null)
            {
                count++;
            }
        }
        _linkedCount = count;
        if (_linkedCount == 0)
            _isLinked = false;

        if (StageController.Instance != null)
        {
            if (gameObject.GetComponent<PipeAnimation>().enabled == false)
            {
                switch (_pipeType)
                {
                    case EPipeType.Pipe_Start1:
                        {
                            if (StageController.Instance._isFirstPipeStart)
                            {
                                if (gameObject.GetComponent<PipeAnimation>() != null)
                                {
                                    gameObject.GetComponent<PipeAnimation>().enabled = true;
                                    _fluidType = FluidType.Start1;
                                    //if (!PipeAnimation._linkedPipes_StartPipe1.Contains(gameObject))
                                    //    PipeAnimation._linkedPipes_StartPipe1.Add(gameObject);
                                }
                            }
                        }
                        break;
                    case EPipeType.Pipe_Start2:
                        {
                            if (StageController.Instance._isSecondPipeStart)
                            {
                                if (gameObject.GetComponent<PipeAnimation>() != null)
                                {
                                    gameObject.GetComponent<PipeAnimation>().enabled = true;
                                    _fluidType = FluidType.Start2;
                                    if (!PipeAnimation._linkedPipes_StartPipe2.Contains(gameObject))
                                        PipeAnimation._linkedPipes_StartPipe2.Add(gameObject);
                                }
                            }
                        }
                        break;
                    case EPipeType.Pipe_Start3:
                        {
                            if (StageController.Instance._isThirdPipeStart)
                            {
                                if (gameObject.GetComponent<PipeAnimation>() != null)
                                {
                                    gameObject.GetComponent<PipeAnimation>().enabled = true;
                                    _fluidType = FluidType.Start3;
                                    if (!PipeAnimation._linkedPipes_StartPipe3.Contains(gameObject))
                                        PipeAnimation._linkedPipes_StartPipe3.Add(gameObject);
                                }
                            }
                        }
                        break;
                    case EPipeType.Pipe_Start4:
                        {
                            if (StageController.Instance._isFourthPipeStart)
                            {
                                if (gameObject.GetComponent<PipeAnimation>() != null)
                                {
                                    gameObject.GetComponent<PipeAnimation>().enabled = true;
                                    _fluidType = FluidType.Start4;
                                    if (!PipeAnimation._linkedPipes_StartPipe4.Contains(gameObject))
                                        PipeAnimation._linkedPipes_StartPipe4.Add(gameObject);
                                }
                            }
                        }
                        break;
                }
            }
        }
    }

    public void PipeFlash()
    {
        if(_material.color.a >= 0.843f)
        {           
            Color color = _material.color;
            color.a = 0.470f;
            _material.color = color;
        }
        else if(_material.color.a <= 0.471f)
        {
            Color color = _material.color;
            color.a = 0.843f;
            _material.color = color;
        }
    }
}
