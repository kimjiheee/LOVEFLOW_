using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static private int sceneChangeCount = 0;

    static public void SceneChange(string SceneName)
    {
        SetSceneChangeCount();
        SceneManager.LoadScene(SceneName);
    }

    static public void SetSceneChangeCount()
    {
        sceneChangeCount++;
    }

    static public int GetSceneChangeCount()
    {
        return sceneChangeCount;
    }
 }
