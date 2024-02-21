using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Skip"))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // 현재씬이 뭔지 정보 받아오기
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Stage1":
                SceneManager.LoadScene("TalkScene_s2_1");
                break;
            case "Opening_CutScene":
                SceneManager.LoadScene("Opening_MainScene");
                break;
                //case "Stage2":
                //    SceneManager.LoadScene("TalkScene_s2_2")
        }
    }
}
