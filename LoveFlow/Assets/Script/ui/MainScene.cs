using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class MainScene : MonoBehaviour
{
    public GameObject settingPanel;
    
    public Image settingArrow;  // 설정 패널에서의 화살표
    public Vector3[] settingArrowPos;


    public Slider[] sliders;
    int selectedSlider = 0; // 현재 선택된 슬라이더 구별용


    public Button[] buttons;
    public Vector3[] arrowpos;    // 화살표 위치 배열

    public Image arrowImage;

    int selectedIndex = 0;
    private bool hasStarted = false; // A 버튼이 처음 눌렸는지를 확인

    private bool panel_hasStarted = false;


    private SoundManager soundManager;


    // 이거 없으면 위아래로 막 날라다님.....
    private float lastMoveTime;         // 조이스틱을 마지막으로 움직인 시간
    private float moveInterval = 0.2f;   // 조이스틱을 움직이는 데 필요한 최소 시간 간격


    private void Start()
    {
        arrowImage.rectTransform.anchoredPosition = arrowpos[0];


        /// 사운드
        soundManager = SoundManager.Instance;

        sliders[0].onValueChanged.AddListener(OnBgmSliderValueChanged);
        sliders[1].onValueChanged.AddListener(OnSfxSliderValueChanged);

        if (soundManager != null)
        {
            sliders[0].value = soundManager.GetBGMVolume() * 7; ; // 초기 슬라이더 값 설정
            sliders[1].value = soundManager.GetSFXVolume() * 7; ;
        }
    }

    private void OnBgmSliderValueChanged(float value)
    {
        if (soundManager != null)
        {
            float normalizedVolume = value / 7.0f; // 슬라이더 값 범위(0~7)를 AudioSource 볼륨 범위로 변환
            soundManager.SetBGMVolume(normalizedVolume);
        }
    }

    private void OnSfxSliderValueChanged(float value)
    {
        if (soundManager != null)
        {
            float normalizedVolume = value / 7.0f; // 슬라이더 값 범위를 AudioSource 볼륨 범위로 변환
            soundManager.SetSFXVolume(normalizedVolume);
        }
    }

    private void Update()
    {
        if (!settingPanel.activeSelf)
        {            
            // a버튼 누른 경우
            if (Input.GetButtonDown("Player1_AButton")|| Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_A);

                if (!hasStarted)    // A 버튼이 처음 눌린 경우
                {
                    Debug.Log(" 처음 눌림");
                    arrowImage.gameObject.SetActive(true);
                    hasStarted = true;
                }

                else // a버튼이 처음 눌린게 아닌 경우 
                {
                    Debug.Log(buttons[selectedIndex]);
                    buttons[selectedIndex].onClick.Invoke();
                }                
            }

            if (Time.time - lastMoveTime >= moveInterval) // 마지막 움직임 이후로 최소한의 시간이 지났을 때만 움직임을 체크
            {
                if (Input.GetAxis("Player1_LeftVertical") > 0.5)
                {
                    SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                    Debug.Log("아래로");

                    selectedIndex = (selectedIndex + 1) % buttons.Length;

                    Selectbuttonpos(selectedIndex);

                    lastMoveTime = Time.time; // 마지막으로 움직인 시간을 업데이트
                }

                else if (Input.GetAxis("Player1_LeftVertical") < -0.5 )
                {
                    SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                    Debug.Log("위로");

                    selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;

                    Selectbuttonpos(selectedIndex);

                    lastMoveTime = Time.time; // 마지막으로 움직인 시간을 업데이트
                }
            }
        }
       
        /// 패널 열려있을때
        else if(settingPanel.activeSelf)
        {
            // X 버튼 누른 경우
            if (Input.GetButtonDown("Player1_XButton") )
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_X);
                settingPanel.SetActive(false);
                SceneManager.LoadScene("Opening_MainScene");
            }

            // A 버튼을 처음 누른 경우
            if (Input.GetButtonDown("Player1_AButton") || Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_A);
                panel_hasStarted = true;
                settingArrow.gameObject.SetActive(true);
            }

            if (!panel_hasStarted)
            {
                return; // A 버튼이 처음 눌리기 전이라면 나머지 코드를 실행하지 않음
            }

            // 이제 A 버튼이 눌린 후에만 실행될 코드를 작성

            if (Time.time - lastMoveTime >= moveInterval) // 마지막 움직임 이후로 최소한의 시간이 지났을 때만 움직임을 체크
            {
                // 슬라이더를 위아래로 전환
                if (Input.GetAxis("Player1_LeftVertical") > 0.5)
                {
                    SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);
                    selectedSlider = (selectedSlider + 1) % sliders.Length;
                    settingArrow.rectTransform.anchoredPosition = settingArrowPos[selectedSlider];
                    lastMoveTime = Time.time;
                }
                else if (Input.GetAxis("Player1_LeftVertical") < -0.5 )
                {
                    SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);
                    selectedSlider = (selectedSlider - 1 + sliders.Length) % sliders.Length;
                    settingArrow.rectTransform.anchoredPosition = settingArrowPos[selectedSlider];
                    lastMoveTime = Time.time;
                }

                // 슬라이더 값을 감소시킴
                else if (Input.GetAxis("Player1_LeftHorizontal") < -0.5 )
                {
                    SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Control_Node);
                    sliders[selectedSlider].value = Mathf.Max(sliders[selectedSlider].value - 1, sliders[selectedSlider].minValue);
                    lastMoveTime = Time.time;
                }

                // 슬라이더 값을 증가시킴
                else if (Input.GetAxis("Player1_LeftHorizontal") > 0.5)
                {
                    SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Control_Node);
                    sliders[selectedSlider].value = Mathf.Min(sliders[selectedSlider].value + 1, sliders[selectedSlider].maxValue);
                    lastMoveTime = Time.time;
                }
            }
        }
    }

    private void Selectbuttonpos(int index)
    {
        // 화살표 이동
        arrowImage.rectTransform.anchoredPosition = arrowpos[index];
    }


    /// <summary>
    /// 버튼 클릭
    /// </summary>

    public void SceneChange()
    {
        GameManager.SceneChange("StageChoice");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void SettingClick()
    {
        settingPanel.SetActive(true);
        arrowImage.gameObject.SetActive(false);     // 기존 화살표는 비활성화
        settingArrow.rectTransform.anchoredPosition = settingArrowPos[0];
    }
}
