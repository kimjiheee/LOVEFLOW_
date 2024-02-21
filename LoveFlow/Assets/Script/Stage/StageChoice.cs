using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageChoice : MonoBehaviour
{
    // �̰� ������ ���Ʒ��� �� ����ٴ�.....
    private float lastMoveTime;         // ���̽�ƽ�� ���������� ������ �ð�
    private float moveInterval = 0.2f;   // ���̽�ƽ�� �����̴� �� �ʿ��� �ּ� �ð� ����


    private bool hasStarted = false; // A ��ư�� ó�� ���ȴ����� Ȯ��

    public Button[] buttons;
    public Image arrowImage;
    public Vector3[] arrowpos;      // ȭ��ǥ ��ġ �� 4��

    int selectedIndex = 0;

    int num = 0;


    private void Start()
    {
        if(GameManager.GetSceneChangeCount() >= 4) 
            SoundManager.Instance.PlayBGM(SoundManager.ClipBGM.BGM_Opening);
        
        arrowImage.rectTransform.anchoredPosition = arrowpos[0];
    }

    void Update()
    {
        if (Input.GetButtonDown("Player1_AButton")||Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_A);

            if (!hasStarted)    // A ��ư�� ó�� ���� ���
            {
                Debug.Log(" ó�� ������ ȭ��ǥ ����");
                arrowImage.gameObject.SetActive(true);
                hasStarted = true;
            }

            else // a��ư�� ó�� ������ �ƴ� ��� 
            {
                buttons[selectedIndex].onClick.Invoke();
                Debug.Log(buttons[selectedIndex]);
            }
        }

        if (Time.time - lastMoveTime >= moveInterval) // ������ ������ ���ķ� �ּ����� �ð��� ������ ���� �������� üũ
        {
            if (Input.GetAxis("Player1_LeftVertical") > 0.5 )
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                Debug.Log("�Ʒ� ����");

                num++;

                if (num % 2 != 0)
                {
                    selectedIndex = 3;
                }
                else
                {
                    selectedIndex = 0;
                }

                Selectbuttonpos(selectedIndex);

                lastMoveTime = Time.time; // ���������� ������ �ð��� ������Ʈ
            }

            else if (Input.GetAxis("Player1_LeftVertical") < -0.5)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                Debug.Log("�� ����");

                num++;

                if(num%2!=0)
                {
                    selectedIndex = 3;
                }
                else
                {
                    selectedIndex = 0;
                }

                Selectbuttonpos(selectedIndex);

                lastMoveTime = Time.time; // ���������� ������ �ð��� ������Ʈ
            }

            else if (Input.GetAxis("Player1_LeftHorizontal") < -0.5)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                selectedIndex = (selectedIndex - 1 < 0) ? 2 : selectedIndex - 1;
                Selectbuttonpos(selectedIndex);

                lastMoveTime = Time.time; // ���������� ������ �ð��� ������Ʈ

                Debug.Log("���� ����");

                
            }

            else if (Input.GetAxis("Player1_LeftHorizontal") > 0.5)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                Debug.Log("������ ����");

                selectedIndex = (selectedIndex + 1) % 3;

                Selectbuttonpos(selectedIndex);

                lastMoveTime = Time.time; // ���������� ������ �ð��� ������Ʈ
            }
        }
    }

    private void Selectbuttonpos(int index)
    {
        // ȭ��ǥ �̵�
        arrowImage.rectTransform.anchoredPosition = arrowpos[index];
    }


    ///��ư
    ///

    public void toMainScene()
    {
        SceneManager.LoadScene("Opening_MainScene");
    }

    public void toStage1()
    {
        GameManager.SceneChange("TalkScene_s1");
    }

    public void toStage2()
    {
        GameManager.SceneChange("TalkScene_s2_1");
    }

    public void toStage3()
    {
        GameManager.SceneChange("TalkScene_s3_1");
    }
}
