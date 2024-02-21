using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;



public class Player1 : MonoBehaviour
{
    public enum GameMode
    {
        PLAYMODE,
        INSTALLMODE,
        DESTROYMODE,
        CLEARMODE,
    }    
    public string _joyStickNum;
    public Transform _CameraTM;
    private Vector3 moveDirection;

    public GameMode _mode; // 플레이어 현재 모드
    public float _playerSpeed;

    public GameObject _triggerForInstall;
    public PipeCube _selectCube;

    public GameObject _hammer;
    public bool _isHaveHammer;

    [SerializeField]
    GameObject _hammerItem;
    [SerializeField]
    Player2 _otherPlayer;


    private bool _isJump = false;
    private bool _isInstallPipe = false;

    private float _buttonCoolTime;
    private bool _buttonOn;


    //RaycastHit _hitData;
    [SerializeField]
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _joyStickNum = "Player1";
        _mode = GameMode.PLAYMODE;
        _playerSpeed = 3.0f;
        _isHaveHammer = false;

        _buttonOn = true;
        _buttonCoolTime = 1;

        _animator = GetComponent<Animator>();
        _hammer.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //UnityEngine.Debug.Log(_buttonCoolTime);
        if (_buttonCoolTime >= 0.3)
        {
            _buttonOn = true;
            _buttonCoolTime = 0f;
        }
        if (_buttonOn == false)
        {
            _buttonCoolTime += Time.deltaTime;
        }
        InputAction();
        moveDirection = Vector3.zero;

    }

    void InputAction()
    {
        switch (_mode)
        {
            case GameMode.PLAYMODE:
                {
                    if (_isInstallPipe) return;
                    Vector3 vect3 = Vector3.zero;

                    if (Input.GetAxis(_joyStickNum + "_LeftHorizontal") > 0.08 ||
                        Input.GetKey(KeyCode.D))
                    {
                        vect3 += Vector3.right * _playerSpeed * Time.deltaTime;
                        transform.Translate(Vector3.right * _playerSpeed * Time.deltaTime);
                    }
                    if (Input.GetAxis(_joyStickNum + "_LeftHorizontal") < -0.08 ||
                        Input.GetKey(KeyCode.A))
                    {
                        vect3 += Vector3.left * _playerSpeed * Time.deltaTime;
                        transform.Translate(Vector3.left * _playerSpeed * Time.deltaTime);
                    }
                    if (Input.GetAxis(_joyStickNum + "_LeftVertical") > 0.05 ||
                        Input.GetKey(KeyCode.S))
                    {
                        vect3 += Vector3.back * _playerSpeed * Time.deltaTime;
                        transform.Translate(Vector3.back * _playerSpeed * Time.deltaTime);
                    }
                    if (Input.GetAxis(_joyStickNum + "_LeftVertical") < -0.05 ||
                        Input.GetKey(KeyCode.W))
                    {
                        vect3 += Vector3.forward * _playerSpeed * Time.deltaTime;
                        transform.Translate(Vector3.forward * _playerSpeed * Time.deltaTime);
                    }

                    // 캐릭터 회전 왼쪽
                    if (Input.GetAxis(_joyStickNum + "_RightVertical") < -0.05 || Input.GetKey(KeyCode.Q))
                    {
                        Vector4 Lookvector = new Vector3(_CameraTM.transform.position.x, 0.0f, _CameraTM.transform.position.z);
                        moveDirection = -Lookvector;
                        //transform.Rotate(0, -40.0f * Time.deltaTime, 0);
                    }
                    if (Input.GetAxis(_joyStickNum + "_RightVertical") > 0.05 || Input.GetKey(KeyCode.E))
                    {
                        Vector4 Lookvector = new Vector3(_CameraTM.transform.position.x, 0.0f, _CameraTM.transform.position.z);
                        moveDirection = -Lookvector;
                        //transform.rotation = Quaternion.LookRotation(Lookvector);
                        //transform.Rotate(0, 40.0f * Time.deltaTime, 0);
                    }
                    
                    if (moveDirection != Vector3.zero)
                    {
                        transform.forward = moveDirection * _playerSpeed * Time.deltaTime;
                    }

                    if (vect3 != Vector3.zero)
                    {
                        SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.CH_Walk);
                        _animator.SetBool("isRun", true);
                    }
                    else
                    {
                        _animator.SetBool("isRun", false);
                    }

                    // 아이템
                    if (Input.GetButtonDown(_joyStickNum + "_XButton") && _buttonOn == true || Input.GetKeyDown(KeyCode.CapsLock) && _buttonOn == true)
                    {
                        // 망치 줍기
                        if (!_isHaveHammer && _otherPlayer._isHaveHammer == false)
                        {
                            if (_hammerItem.GetComponent<Hammer>().isHammerNearby)
                            {
                                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Get_Hammer);

                                _hammerItem.gameObject.SetActive(false);
                                _isHaveHammer = true;
                                _hammer.SetActive(true);
                                UnityEngine.Debug.Log(_joyStickNum + "망치 줍기");
                                _buttonOn = false;
                                break;
                            }
                        }
                        else if (_isHaveHammer) // 놓기
                        {
                            SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Get_Hammer);

                            _hammerItem.gameObject.transform.position = transform.position;
                            _hammerItem.gameObject.SetActive(true);
                            _isHaveHammer = false;
                            _hammer.SetActive(false);
                            UnityEngine.Debug.Log(_joyStickNum + "망치 버리기");
                            _buttonOn = false;

                            break;
                        }


                    }

                    // 점프
                    if (Input.GetButtonDown(_joyStickNum + "_YButton") ||
                        Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        if (_isJump == false)
                        {
                            SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.CH_Jump);

                            gameObject.GetComponent<Rigidbody>().velocity = Vector3.up * 5f;
                            _isJump = true;
                            _animator.SetBool("isJump", true);
                        }
                    }

                    // 모드 진입
                    if (_selectCube != null && Input.GetButtonDown(_joyStickNum + "_BButton") && _selectCube.CompareTag("Cube")
                        || Input.GetKeyDown(KeyCode.F))
                    {
                        if (_buttonOn == true)
                        {
                            // _selectCube = _hitData.transform.GetComponent<PipeCube>();
                            if (_selectCube._isCanInstall == true && _isHaveHammer == false)
                            {
                                _mode = GameMode.INSTALLMODE;
                                _selectCube._childObject.SetActive(true);
                            }
                            else if (_selectCube._isInstalled == true && _isHaveHammer == true)
                            {
                                if (!_selectCube._childObject.GetComponent<Pipe>().GetComponentInChildren<PipeController>()._isAnimStarted)
                                {
                                    _mode = GameMode.DESTROYMODE;
                                    UnityEngine.Debug.Log("파괴 모드");
                                }
                            }
                            else
                            {
                                _selectCube = null;
                            }
                            _buttonOn = false;
                        }
                    }
                }
                break;
            case GameMode.INSTALLMODE:
                {
                    if (_isInstallPipe) break;

                    _animator.SetBool("isRun", false);

                    if (Input.GetButtonDown(_joyStickNum + "_AButton") || Input.GetKeyDown(KeyCode.V))
                    {
                        Debug.Log("취소 버튼 누르긴 함");
                        if (!_selectCube._isInstalled)
                        {
                            _selectCube._childObject.GetComponent<Pipe>()._currentObject = 0;
                            _selectCube._childObject.SetActive(false);
                        }
                        else
                        {
                            _selectCube._isInstalled = true;
                        }
                        _selectCube = null;
                        _mode = GameMode.PLAYMODE;
                        UnityEngine.Debug.Log(_joyStickNum + "설치 모드 취소");
                    }

                    //파이프 모양 선택
                    if (Input.GetAxis(_joyStickNum + "_LeftTrigger") == 1 || Input.GetKeyDown(KeyCode.Tab))
                    {
                        if (_buttonOn == true)
                        {
                            _selectCube._childObject.GetComponent<Pipe>().ChangePipeType(1);
                            UnityEngine.Debug.Log("파이프 선택 오른쪽");
                            UnityEngine.Debug.Log(Input.GetAxis(_joyStickNum + "_LeftTrigger"));
                            _buttonOn = false;
                        }
                    }
                    //if (Input.GetAxis(_joyStickNum + "_RightTrigger") == 1)
                    //{
                    //    if (_buttonOn == true)
                    //    {
                    //        _selectCube._childObject.GetComponent<Pipe>().ChangePipeType(0);
                    //        UnityEngine.Debug.Log("파이프 선택 오른쪽");
                    //        UnityEngine.Debug.Log(Input.GetAxis(_joyStickNum + "_RightTrigger"));

                    //        _buttonOn = false;
                    //    }
                    //}

                    // 파이프 회전
                    if (Input.GetAxis(_joyStickNum + "_DpadHorizontal") == 1 || Input.GetKeyDown(KeyCode.Z))
                    {
                        if (_buttonOn == true)
                        {
                            SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Install_mode_Pipe_Turn);
                            _selectCube._childObject.GetComponent<Pipe>().ChangePipeRadianY();
                            UnityEngine.Debug.Log(_joyStickNum + "X축 기준 회전");
                            _buttonOn = false;
                        }
                    }
                    if (Input.GetAxis(_joyStickNum + "_DpadHorizontal") == -1 || Input.GetKeyDown(KeyCode.X))
                    {
                        if (_buttonOn == true)
                        {
                            SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Install_mode_Pipe_Turn);
                            _selectCube._childObject.GetComponent<Pipe>().ChangePipeRadianZ();
                            _buttonOn = false;
                            UnityEngine.Debug.Log(_joyStickNum + "Z축 기준 회전");
                        }
                    }
                    if (Input.GetAxis(_joyStickNum + "_DpadVertical") == 1 || Input.GetKeyDown(KeyCode.C))
                    {
                        if (_buttonOn == true)
                        {
                            SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Install_mode_Pipe_Turn);
                            _selectCube._childObject.GetComponent<Pipe>().ChangePipeRadianX();
                            _buttonOn = false;
                            UnityEngine.Debug.Log(_joyStickNum + "Y축 기준 회전");
                        }
                    }


                    if ((Input.GetButtonUp(_joyStickNum + "_BButton") && (_buttonOn == true) && _selectCube._childObject.GetComponent<Pipe>()._currentObject >= 1)
                        || Input.GetKeyDown(KeyCode.F) && _selectCube._childObject.GetComponent<Pipe>()._currentObject >= 1)
                    {
                        _buttonOn = false;
                        _selectCube._isInstalled = true;
                        _selectCube = null;
                        _isInstallPipe = true;
                        SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Install_mode_Btn_B);
                        _animator.SetBool("isInstallPipe", true);
                        UnityEngine.Debug.Log(_joyStickNum + "파이프 설치");
                        Invoke("FinishInstallPipe", 0.18f);
                        _mode = GameMode.PLAYMODE;
                    }
                }
                break;
            case GameMode.DESTROYMODE:
                {
                    if ((Input.GetButtonUp(_joyStickNum + "_BButton") && _buttonOn == true) || Input.GetKeyDown(KeyCode.F))
                    {
                        _buttonOn = false;
                        _selectCube._childObject.GetComponent<Pipe>()._currentObject = 0;                       
                        // 큐브 상태 초기화(물 애니메이션 초기화 등...)
                        _selectCube._childObject.GetComponent<Pipe>().GetComponentInChildren<PipeAnimation>().InitializePipeState(_selectCube._childObject.GetComponent<Pipe>().GetComponentInChildren<PipeController>().gameObject);
                        //_selectCube._childObject.GetComponent<Pipe>().GetComponentInChildren<PipeAnimation>().enabled = false;                       
                        _selectCube._childObject.SetActive(false);
                        _selectCube._isInstalled = false;
                        //_isInstallPipe = false;
                        _selectCube = null;
                        SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Pipe_Fix);
                        _animator.SetBool("isHammering", true);
                        Invoke("FinishHammering", 1.85f);
                        //_mode = GameMode.PLAYMODE;
                        UnityEngine.Debug.Log(_joyStickNum + "파이프 파괴");
                    }

                    if (Input.GetButtonDown(_joyStickNum + "_AButton") || Input.GetKeyDown(KeyCode.V))
                    {
                        UnityEngine.Debug.Log(_joyStickNum + "설치 모드 취소");
                        if (!_selectCube._isInstalled)
                        {
                            _selectCube._childObject.GetComponent<Pipe>()._currentObject = 0;
                            _selectCube._childObject.SetActive(false);
                        }
                        _selectCube = null;
                        _mode = GameMode.PLAYMODE;
                    }
                }
                break;
            case GameMode.CLEARMODE:
                break;
            default:
                break;
        }
    }

    void FinishInstallPipe()
    {
        _mode = GameMode.PLAYMODE;
        _isInstallPipe = false;
        _animator.SetBool("isInstallPipe", false);
    }
    void FinishHammering()
    {
        _mode = GameMode.PLAYMODE;
        _animator.SetBool("isHammering", false);
    }
    public void StageClear()
    {
        if(_mode != GameMode.CLEARMODE)
        {
            _mode = GameMode.CLEARMODE;
            _animator.SetBool("isStageClear", true);
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Cube"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _isJump = false;
            _animator.SetBool("isJump", false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            _hammerItem.GetComponent<Hammer>().isHammerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            _hammerItem.GetComponent<Hammer>().isHammerNearby = false;
        }
    }
}