using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour
{
    public bool _isStageClear = false;

    //public GameObject player1;
    //public GameObject player2;

    public GameObject clear;
    public GameObject start;
    public GameObject ingame;

    public float waitingTime=3f;

    void Start()
    {
        Time.timeScale = 0;

        StartCoroutine(WaitForAButton());
    }

    void Update()
    {
        if (Input.GetButtonDown("Clear"))
        {
            //player1.SetActive(false);
            //player2.SetActive(false);
            clear.SetActive(true);
            _isStageClear = true;


        }

        if(_isStageClear==true)
        {
            if (Input.GetButtonDown("Player1_AButton"))
            {
                Debug.Log("a버튼 눌림");
                LoadNextScene();
            }
        }
   
    }

    private void LoadNextScene()
    {
        // 현재씬이 뭔지 정보 받아오기
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Stage1":
                SceneManager.LoadScene("StageChoice");
                break;
            case "Stage2-1":
                SceneManager.LoadScene("TalkScene_s2_2");
                break;
            case "Stage2-2":
                SceneManager.LoadScene("StageChoice");
                break;
            case "Stage3-1":
                SceneManager.LoadScene("TalkScene_s3_2");
                break;
            case "Stage3-2":
                SceneManager.LoadScene("TalkScene_s3_3");
                break;
            case "Stage3-3":
                SceneManager.LoadScene("EndingScene");
                break;
        }
    }


        IEnumerator WaitForAButton()
    {
        // Wait until the A button is pressed
        while (!Input.GetButtonDown("Player1_AButton") &&
            !Input.GetButtonDown("Player2_AButton") &&
            !Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        // After the A button is pressed, toggle the GameObjects
        Time.timeScale =1;
        start.SetActive(false);
        ingame.SetActive(true);
    }
}