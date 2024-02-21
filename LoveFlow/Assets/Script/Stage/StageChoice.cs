using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageChoice : MonoBehaviour
{
    // 이거 없으면 위아래로 막 날라다님.....
    private float lastMoveTime;         // 조이스틱을 마지막으로 움직인 시간
    private float moveInterval = 0.2f;   // 조이스틱을 움직이는 데 필요한 최소 시간 간격


    private bool hasStarted = false; // A 버튼이 처음 눌렸는지를 확인

    public Button[] buttons;
    public Image arrowImage;
    public Vector3[] arrowpos;      // 화살표 위치 총 4개

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

            if (!hasStarted)    // A 버튼이 처음 눌린 경우
            {
                Debug.Log(" 처음 눌려서 화살표 등장");
                arrowImage.gameObject.SetActive(true);
                hasStarted = true;
            }

            else // a버튼이 처음 눌린게 아닌 경우 
            {
                buttons[selectedIndex].onClick.Invoke();
                Debug.Log(buttons[selectedIndex]);
            }
        }

        if (Time.time - lastMoveTime >= moveInterval) // 마지막 움직임 이후로 최소한의 시간이 지났을 때만 움직임을 체크
        {
            if (Input.GetAxis("Player1_LeftVertical") > 0.5 )
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                Debug.Log("아래 누름");

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

                lastMoveTime = Time.time; // 마지막으로 움직인 시간을 업데이트
            }

            else if (Input.GetAxis("Player1_LeftVertical") < -0.5)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                Debug.Log("위 누름");

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

                lastMoveTime = Time.time; // 마지막으로 움직인 시간을 업데이트
            }

            else if (Input.GetAxis("Player1_LeftHorizontal") < -0.5)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                selectedIndex = (selectedIndex - 1 < 0) ? 2 : selectedIndex - 1;
                Selectbuttonpos(selectedIndex);

                lastMoveTime = Time.time; // 마지막으로 움직인 시간을 업데이트

                Debug.Log("왼쪽 누름");

                
            }

            else if (Input.GetAxis("Player1_LeftHorizontal") > 0.5)
            {
                SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_Move);

                Debug.Log("오른쪽 누름");

                selectedIndex = (selectedIndex + 1) % 3;

                Selectbuttonpos(selectedIndex);

                lastMoveTime = Time.time; // 마지막으로 움직인 시간을 업데이트
            }
        }
    }

    private void Selectbuttonpos(int index)
    {
        // 화살표 이동
        arrowImage.rectTransform.anchoredPosition = arrowpos[index];
    }


    ///버튼
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
