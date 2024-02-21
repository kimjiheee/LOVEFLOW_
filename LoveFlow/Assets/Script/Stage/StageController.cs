using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public enum StageState
    {
        None,
        Stage_One,
        Stage_Two1,
        Stage_Two2,
        Stage_Three1,
        Stage_Three2,
        Stage_Three3,
    }

    public GameObject clear;

    public Image red;
    public int setBlinkCount = 3;  // �����̴� Ƚ��
    private int blinkCount;  // �����̴� Ƚ��
    public float blinkInterval = 0.5f;  // �����̴� ����(��)
    private bool isBlinking = false;  // �������� ���� ������ ����


    public StageState _sceneState = StageState.None;

    static public float _currentTime = 0.0f;
    public int _lifeCount = 3;
    public int _endCount = 0; // ������ �����ؼ� ���������� ���޽�Ų Ƚ��

    public bool _isStageClear = false;
    public bool _isStageFailed = false;

    public bool _isFirstPipeStart = false;
    public bool _isSecondPipeStart = false;
    public bool _isThirdPipeStart = false;
    public bool _isFourthPipeStart = false;

    // ������ ���� ������ �ٲ� ����� ȭ��ǥ
    public GameObject[] dashboardArrow;

    // ���ӽ���
    public GameObject failure;

    private int _clearConditionCount = 0;

    static public StageController Instance { get; private set; }

    [SerializeField]
    Player1 _player1;
    [SerializeField]
    Player2 _player2;

    [SerializeField]
    float _pipeStartTime_First = 10.0f;
    [SerializeField]
    float _pipeStartTime_Second = 20.0f;
    [SerializeField]
    float _pipeStartTime_Third = 30.0f;
    [SerializeField]
    float _pipeStartTime_Fourth = 40.0f;

    [SerializeField]
    GameObject _startPipe1;
    [SerializeField]
    GameObject _startPipe2;
    [SerializeField]
    GameObject _startPipe3;
    [SerializeField]
    GameObject _startPipe4;

    bool _isPipeBlink = false;
    bool _isSoundPlay = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;           
        }
        else
        {
            Destroy(gameObject);
        }
        SetSceneState();
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.ClipBGM.BGM_Stage);
        _currentTime = 0.0f;
        blinkCount = setBlinkCount;        
    }

    private void Update()
    {
        if (_lifeCount == 2)
        {
            if (!_isSoundPlay)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Fluid_On_Floor);
                _isSoundPlay = true;   
            }
            StartBlink();

            dashboardArrow[0].SetActive(false);
            dashboardArrow[1].SetActive(true);
        }

        if (_lifeCount == 1)
        {
            if (_isSoundPlay)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Fluid_On_Floor);
                _isSoundPlay = false;
                blinkCount = setBlinkCount;
            }

            StartBlink();

            dashboardArrow[1].SetActive(false);
            dashboardArrow[2].SetActive(true);
        }

        if (_lifeCount <= 0)
        {
            if (!_isSoundPlay)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Stage_UI_Fail);
                _isSoundPlay = true;
            }
            // Gameover
            _isStageFailed = true;

            //
            failure.SetActive(true);
            if(Input.GetButtonDown("Player1_AButton") || Input.GetButtonDown("Player2_AButton")
                || Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.SceneChange("StageChoice");
            }    
        }

        _currentTime += 0.5f * Time.deltaTime;

        if(_currentTime >= _pipeStartTime_First - 5.0f && _currentTime < _pipeStartTime_First)
        {
            if (_sceneState != StageState.None)
            {
                if (_isPipeBlink == false)
                {
                    _isPipeBlink = true;
                    //Color color = _startPipe1.GetComponent<PipeController>()._material.color;
                    Color color = new Color(1.0f, 0.156f, 0.156f, 0.843f);                  
                    _startPipe1.GetComponent<PipeController>()._material.color = color;
                    IEnumerator coroutine = PipeFlash(_startPipe1);
                    StartCoroutine(coroutine);
                }
            }                                     
        }         
        
        if(_currentTime >= _pipeStartTime_First)
        {
            if(_sceneState != StageState.None)
            {
                if(_isFirstPipeStart == false)
                {
                    _isFirstPipeStart = true;
                    _isPipeBlink = false;
                    Color color = new Color(1.0f, 0.862f, 0.862f, 0.843f);
                    _startPipe1.GetComponent<PipeController>()._material.color = color;
                }                
            }
                
        }                            

        if (_endCount >= _clearConditionCount)
        {
            if (!_isStageClear)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Stage_UI_Clear);
                _isStageClear = true;
            }
            
            _player1.StageClear();
            _player2.StageClear();
        }

        if(_isStageClear==true)
        {
            stageClear();
        }
    }
      
    private void SetSceneState()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Stage1")
        {
            _sceneState = StageState.Stage_One;
            _clearConditionCount = 1;
        }
        else if (scene.name == "Stage2-1")
        {
            _sceneState = StageState.Stage_Two1;
            _clearConditionCount = 2;
        }
        else if (scene.name == "Stage2-2")
        {
            _sceneState = StageState.Stage_Two2;
            _clearConditionCount = 2;
        }
        else if (scene.name == "Stage3-1")
        {
            _sceneState = StageState.Stage_Three1;
            _clearConditionCount = 1;
        }
        else if (scene.name == "Stage3-2")
        {
            _sceneState = StageState.Stage_Three2;
            _clearConditionCount = 2;
        }
        else if (scene.name == "Stage3-3")
        {
            _sceneState = StageState.Stage_Three3;
            _clearConditionCount = 2;
        }
    }

    public void StartBlink()
    {
        if (!isBlinking && blinkCount == setBlinkCount)
        {
            isBlinking = true;
            StartCoroutine(BlinkRoutine());
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while(blinkCount != 0)
        {
            if (isBlinking) blinkCount -= 1;

            // UI �̹����� ��Ȱ��ȭ�մϴ�.
            SetImageActive(true);

            // blinkInterval ��ŭ ��ٸ��ϴ�.
            yield return new WaitForSeconds(blinkInterval);

            // UI �̹����� �ٽ� Ȱ��ȭ�մϴ�.
            SetImageActive(false);

            // blinkInterval ��ŭ �߰��� ��ٸ��ϴ�.
            yield return new WaitForSeconds(blinkInterval);
        }

        // �������� �������Ƿ� isBlinking�� false�� �����մϴ�.
        isBlinking = false;
    }
    private IEnumerator PipeFlash(GameObject go)
    {
        for(int i = 0; i < 40; ++i)
        {
            go.GetComponent<PipeController>().PipeFlash();
            yield return new WaitForSeconds(0.25f);
        }
        
    }

    private void SetImageActive(bool isActive)
    {
        if (red != null)
        {
            red.gameObject.SetActive(isActive);
        }
    }

    void stageClear()
    {
        clear.SetActive(true);

        if (Input.GetButtonDown("Player1_AButton"))
        {
            SceneManager.LoadScene("StageChoice");
        }
    }
}
