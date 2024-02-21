using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject _pipe;

    static public List<GameObject> _linkedPipes_StartPipe1;
    static public List<GameObject> _linkedPipes_StartPipe2;
    static public List<GameObject> _linkedPipes_StartPipe3;
    static public List<GameObject> _linkedPipes_StartPipe4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {if (StageController.Instance._isStageClear) return;
        if (_pipe.GetComponent<PipeController>() != null)
        {
            if (_pipe.GetComponent<PipeController>()._isAnimFinished == false)
            {
                _pipe.GetComponent<PipeController>()._isAnimStarted = true;

                Color color = _pipe.GetComponent<PipeController>()._material.color;
                color.r += _pipe.GetComponent<PipeController>()._pipeFillSpeed * 0.01f * Time.deltaTime;
                color.g -= _pipe.GetComponent<PipeController>()._pipeFillSpeed * 0.01f * Time.deltaTime;
                color.b -= _pipe.GetComponent<PipeController>()._pipeFillSpeed * 0.01f * Time.deltaTime;
                if (color.r >= 1.0f)
                {
                    color.r = 1.0f;
                }
                Debug.Log(color.g);
                if (color.g <= 0.15f)
                {
                    _pipe.GetComponent<PipeController>()._isAnimFinished = true;
                    if (_pipe.GetComponent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End1 ||
                        _pipe.GetComponent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End2 ||
                        _pipe.GetComponent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End3 ||
                        _pipe.GetComponent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End4)
                    {
                        StageController.Instance._endCount++;
                        return;
                    }
                    for (int i = 0; i < _pipe.GetComponent<PipeController>()._linkedPipeList.Length; ++i)
                    {
                        if (_pipe.GetComponent<PipeController>()._linkedPipeList[i] != null)
                        {
                            if (_pipe.GetComponent<PipeController>()._linkedPipeList[i].GetComponent<PipeAnimation>().enabled == true) continue;
                            if(_pipe.GetComponent<PipeController>()._linkedPipeList[i].GetComponent<PipeAnimation>().enabled == false)
                            {
                                _pipe.GetComponent<PipeController>()._linkedPipeList[i].GetComponent<PipeAnimation>().enabled = true;
                            }
                            if (_pipe.GetComponent<PipeController>()._linkedPipeList[i].GetComponent<PipeController>()._fluidType == PipeController.FluidType.None)
                            {
                                _pipe.GetComponent<PipeController>()._linkedPipeList[i].GetComponent<PipeController>()._fluidType = _pipe.GetComponent<PipeController>()._fluidType;
                            }
                            /*if (_pipe.GetComponent<PipeController>()._fluidType == PipeController.FluidType.Start1)
                            {
                                if (!_linkedPipes_StartPipe1.Contains(gameObject))
                                    _linkedPipes_StartPipe1.Add(_pipe.GetComponent<PipeController>()._linkedPipeList[i]);
                                //_linkedPipes_StartPipe1 = StageController.Instance._linkedPipes_StartPipe1.Distinct().ToList();
                            }*/
                            /*else if (_pipe.GetComponent<PipeController>()._fluidType == PipeController.FluidType.Start2)
                            {
                                StageController.Instance._linkedPipes_StartPipe2.Add(_pipe.GetComponent<PipeController>()._linkedPipeList[i]);
                                StageController.Instance._linkedPipes_StartPipe2 = StageController.Instance._linkedPipes_StartPipe2.Distinct().ToList();
                            }
                            else if (_pipe.GetComponent<PipeController>()._fluidType == PipeController.FluidType.Start3)
                            {
                                StageController.Instance._linkedPipes_StartPipe3.Add(_pipe.GetComponent<PipeController>()._linkedPipeList[i]);
                                StageController.Instance._linkedPipes_StartPipe3 = StageController.Instance._linkedPipes_StartPipe3.Distinct().ToList();
                            }
                            else if (_pipe.GetComponent<PipeController>()._fluidType == PipeController.FluidType.Start4)
                            {
                                StageController.Instance._linkedPipes_StartPipe4.Add(_pipe.GetComponent<PipeController>()._linkedPipeList[i]);
                                StageController.Instance._linkedPipes_StartPipe4 = StageController.Instance._linkedPipes_StartPipe4.Distinct().ToList();
                            }*/
                        }
                    }
                }
                gameObject.GetComponent<PipeController>()._material.color = color;
            }
            else
            {
                // Particles
                for (int i = 0; i < _pipe.GetComponent<PipeController>()._linkedPipeList.Length; ++i)
                {
                    if (_pipe.GetComponent<PipeController>()._linkedPipeList[i] == null)
                    {
                        if (_pipe.GetComponentsInChildren<ParticleSystem>()[i].isPlaying == false)
                        {
                            _pipe.GetComponentsInChildren<ParticleSystem>()[i].Play();
                            Invoke("ReduceLifeCount", 1.0f);
                            //Invoke("InitLinkedPipe", 5.0f);
                        }
                    }
                }
                for (int i = 0; i < _pipe.GetComponent<PipeController>()._linkedPipeList.Length; ++i)
                {
                    if (_pipe.GetComponent<PipeController>()._linkedPipeList[i] != null)
                    {
                        if (_pipe.GetComponentsInChildren<ParticleSystem>()[i].isPlaying == true)
                        {
                            _pipe.GetComponentsInChildren<ParticleSystem>()[i].Stop();
                            _pipe.GetComponent<PipeController>()._linkedPipeList[i].GetComponent<PipeAnimation>().enabled = true;
                        }
                    }
                }
            }
        }
    }


    public void InitializePipeState(GameObject go)
    {
        // Turn Off Particles
        for (int i = 0; i < go.GetComponent<PipeController>()._linkedPipeList.Length; ++i)
        {
            if (go.GetComponentsInChildren<ParticleSystem>()[i].isPlaying == true)
            {
                go.GetComponentsInChildren<ParticleSystem>()[i].Stop();
            }
        }
        go.GetComponent<PipeController>()._isAnimFinished = false;
        go.GetComponent<PipeController>()._isAnimStarted = false;

        go.GetComponent<PipeController>()._linkedCount = 0;
        go.GetComponent<PipeController>()._isLinked = false;
        for (int i = 0; i < go.GetComponent<PipeController>()._linkedPipeList.Length; ++i)
        {
            gameObject.GetComponent<PipeController>()._linkedPipeList[i] = null;
        }

        // Initialize Color
        go.GetComponent<PipeController>()._material.color = _pipe.GetComponent<PipeController>()._originColor;

        go.GetComponent<PipeController>()._fluidType = PipeController.FluidType.None;

        go.GetComponent<PipeAnimation>().enabled = false;

        go.GetComponent<PipeController>().gameObject.SetActive(false);

        // 시작 파이프는 사라지지 않게 해야 함.
        if ((go.name == "pipe_start1") ||
            (go.name == "pipe_start2") ||
            (go.name == "pipe_start3") ||
            (go.name == "pipe_start4"))
        {
            go.GetComponent<PipeController>().gameObject.SetActive(true);
        }
    }

    private void InitLinkedPipe()
    {
        switch (_pipe.GetComponent<PipeController>()._fluidType)
        {
            case PipeController.FluidType.Start1:
                {
                    for (int i = _linkedPipes_StartPipe1.Count - 1; i >= 0; --i)
                    {
                        if (_linkedPipes_StartPipe1[i] == _pipe)
                            continue;
                        if (_linkedPipes_StartPipe1[i].GetComponent<PipeAnimation>() != null)
                        {
                            _linkedPipes_StartPipe1[i].GetComponent<PipeAnimation>().InitializePipeState(_linkedPipes_StartPipe1[i]);
                        }
                    }
                    _pipe.GetComponent<PipeAnimation>().InitializePipeState(_pipe);
                    _linkedPipes_StartPipe1.Clear();
                }
                break;
            case PipeController.FluidType.Start2:
                {
                    for (int i = 0; i < _linkedPipes_StartPipe2.Count; ++i)
                    {
                        if (_linkedPipes_StartPipe2[i].GetComponent<PipeAnimation>() != null)
                        {
                            _linkedPipes_StartPipe2[i].GetComponent<PipeAnimation>().InitializePipeState(_linkedPipes_StartPipe2[i]);
                        }
                    }
                }
                break;
            case PipeController.FluidType.Start3:
                {
                    for (int i = 0; i < _linkedPipes_StartPipe3.Count; ++i)
                    {
                        if (_linkedPipes_StartPipe3[i].GetComponent<PipeAnimation>() != null)
                        {
                            _linkedPipes_StartPipe3[i].GetComponent<PipeAnimation>().InitializePipeState(_linkedPipes_StartPipe3[i]);
                        }
                    }
                }
                break;
            case PipeController.FluidType.Start4:
                {
                    for (int i = 0; i < _linkedPipes_StartPipe4.Count; ++i)
                    {
                        if (_linkedPipes_StartPipe4[i].GetComponent<PipeAnimation>() != null)
                        {
                            _linkedPipes_StartPipe4[i].GetComponent<PipeAnimation>().InitializePipeState(_linkedPipes_StartPipe4[i]);
                        }
                    }
                }
                break;
        }
    }

    private void ReduceLifeCount()
    {
        StageController.Instance._lifeCount--;
    }
}
