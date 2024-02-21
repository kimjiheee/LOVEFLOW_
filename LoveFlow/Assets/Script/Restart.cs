using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            LoadNewGame();
        }
    }

    private void LoadNewGame()
    {
        // ������� ���� ���� �޾ƿ���
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Stage1":
                SceneManager.LoadScene("Stage1");
                break;
                //case "Stage2":
                //    SceneManager.LoadScene("Stage2");
                //break;
                //case "Stage3":
                //    SceneManager.LoadScene("Stage3");
                //break;
        }
    }
}
