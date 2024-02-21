using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class MainScene : MonoBehaviour
{
    public GameObject settingPanel;
    
    public Image settingArrow;  // ���� �гο����� ȭ��ǥ
    public Vector3[] settingArrowPos;


    public Slider[] sliders;
    int selectedSlider = 0; // ���� ���õ� �����̴� ������


    public Button[] buttons;
    public Vector3[] arrowpos;    // ȭ��ǥ ��ġ �迭

    public Image arrowImage;

    int selectedIndex = 0;
    private bool hasStarted = false; // A ��ư�� ó�� ���ȴ����� Ȯ��

    private bool panel_hasStarted = false;


    private SoundManager soundManager;


    // �̰� ������ ���Ʒ��� �� ����ٴ�.....
    private float lastMoveTime;         // ���̽�ƽ�� ���������� ������ �ð�
    private float moveInterval = 0.2f;   // ���̽�ƽ�� �����̴� �� �ʿ��� �ּ� �ð� ����


    private void Start()
    {
        arrowImage.rectTransform.anchoredPosition = arrowpos[0];


        /// ����
        soundManager = SoundManager.Instance;

        sliders[0].onValueChanged.AddListener(OnBgmSliderValueChanged);
        sliders[1].onValueChanged.AddListener(OnSfxSliderValueChanged);

        if (soundManager != null)
        {
            sliders[0].value = soundManager.GetBGMVolume() * 7; ; // �ʱ� �����̴� �� ����
            sliders[1].value = soundManager.GetSFXVolume() * 7; ;
        }
    }

    private void OnBgmSliderValueChanged(float value)
    {
        if (soundManager != null)
        {
            float normalizedVolume = value / 7.0f; // �����̴� �� ����(0~7)�� AudioSource ���� ������ ��ȯ
            soundManager.SetBGMVolume(normalizedVolume);
        }
    }

    private void OnSfxSliderValueChanged(float value)
    {
        if (soundManager != null)
        {
            float normalizedVolume = value / 7.0f; // �����̴� �� ������ AudioSource ���� ������ ��ȯ
            soundManager.SetSFXVolume(normalizedVolume);
        }
    }

    private void Update()
    {
        if (!settingPanel.activeSelf)
        {            
            // a��ư ���� ���
            if (Input.GetButtonDown("Player1_AButton")|| Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_A);

                if (!hasStarted)    // A ��ư�� ó�� ���� ���
                {
                    Debug.Log(" ó�� ����");
                    arrowImage.gameObject.SetActive(true);
                    hasStarted = true;
                }

                else // a��ư�� ó�� ������ �ƴ� ��� 
                {
                    Debug.Log(buttons[selectedIndex]);
                    buttons[selectedIndex].onClick.Invoke();
                }                
            }

            if (Time.time - lastMoveTime >= moveInterval) // ������ ������ ���ķ� �ּ����� �ð��� ������ ���� �������� üũ
            {
                if (Input.GetAxis("Player1_LeftVertical") > 0.5)
                {
                    SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                    Debug.Log("�Ʒ���");

                    selectedIndex = (selectedIndex + 1) % buttons.Length;

                    Selectbuttonpos(selectedIndex);

                    lastMoveTime = Time.time; // ���������� ������ �ð��� ������Ʈ
                }

                else if (Input.GetAxis("Player1_LeftVertical") < -0.5 )
                {
                    SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                    Debug.Log("����");

                    selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;

                    Selectbuttonpos(selectedIndex);

                    lastMoveTime = Time.time; // ���������� ������ �ð��� ������Ʈ
                }
            }
        }
       
        /// �г� ����������
        else if(settingPanel.activeSelf)
        {
            // X ��ư ���� ���
            if (Input.GetButtonDown("Player1_XButton") )
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_X);
                settingPanel.SetActive(false);
                SceneManager.LoadScene("Opening_MainScene");
            }

            // A ��ư�� ó�� ���� ���
            if (Input.GetButtonDown("Player1_AButton") || Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_A);
                panel_hasStarted = true;
                settingArrow.gameObject.SetActive(true);
            }

            if (!panel_hasStarted)
            {
                return; // A ��ư�� ó�� ������ ���̶�� ������ �ڵ带 �������� ����
            }

            // ���� A ��ư�� ���� �Ŀ��� ����� �ڵ带 �ۼ�

            if (Time.time - lastMoveTime >= moveInterval) // ������ ������ ���ķ� �ּ����� �ð��� ������ ���� �������� üũ
            {
                // �����̴��� ���Ʒ��� ��ȯ
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

                // �����̴� ���� ���ҽ�Ŵ
                else if (Input.GetAxis("Player1_LeftHorizontal") < -0.5 )
                {
                    SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Control_Node);
                    sliders[selectedSlider].value = Mathf.Max(sliders[selectedSlider].value - 1, sliders[selectedSlider].minValue);
                    lastMoveTime = Time.time;
                }

                // �����̴� ���� ������Ŵ
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
        // ȭ��ǥ �̵�
        arrowImage.rectTransform.anchoredPosition = arrowpos[index];
    }


    /// <summary>
    /// ��ư Ŭ��
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
        arrowImage.gameObject.SetActive(false);     // ���� ȭ��ǥ�� ��Ȱ��ȭ
        settingArrow.rectTransform.anchoredPosition = settingArrowPos[0];
    }
}
