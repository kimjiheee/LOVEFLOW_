//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

///
/// /// 혹시 몰라서 그냥 백업용
///

//public class TallkScene : MonoBehaviour
//{
//    public Image[] images;
//    private int currentImageIndex = 0;
//    public float fadeInDuration = 0.5f;
//    public float fadeOutDuration = 1f;

//    public Image fadeOutImage;


//    private void Update()
//    {
//        // 'A' 버튼이 눌렸는지 감지합니다.
//        if (Input.GetButtonDown("Player1_AButton"))
//        {
//            if (currentImageIndex < images.Length)
//            {
//                StartCoroutine(FadeIn(images[currentImageIndex], fadeInDuration));
//                currentImageIndex++;
//            }

//            else
//                StartCoroutine(FadeOut(fadeOutImage, fadeOutDuration));
//        }
//    }

//    IEnumerator FadeIn(Image image, float duration)
//    {
//        image.gameObject.SetActive(true);
//        Color color = image.color;
//        color.a = 0f;
//        float startTime = Time.time;

//        while (Time.time < startTime + duration)
//        {
//            color.a = (Time.time - startTime) / duration;
//            image.color = color;
//            yield return null;
//        }

//        color.a = 1f;
//        image.color = color;
//    }

//    // 화면 전환용 페이드아웃
//    IEnumerator FadeOut(Image image, float duration)
//    {
//        image.gameObject.SetActive(true);
//        Color color = image.color;
//        color.a = 0f;
//        float startTime = Time.time;

//        while (Time.time < startTime + duration)
//        {
//            color.a = (Time.time - startTime) / duration;
//            image.color = color;
//            yield return null;
//        }

//        color.a = 1f;
//        image.color = color;

//        // Once the fade out is complete, load the next scene
//        LoadNextScene();
//    }

//    private void LoadNextScene()
//    {
//        string currentScene = SceneManager.GetActiveScene().name;

//        switch (currentScene)
//        {
//            case "TalkScene_s1":
//                SceneManager.LoadScene("Stage1");
//                break;
//            case "TalkScene_s2_1":
//                SceneManager.LoadScene("Stage1");
//                break;
//            case "TalkScene_s2_2":
//                SceneManager.LoadScene("Stage1");
//                break;
//            case "TalkScene_s3_1":
//                SceneManager.LoadScene("Stage1");
//                break;
//            //case "TalkScene_s3_2":
//            //    SceneManager.LoadScene("Stage1");
//            //    break;
//            case "TalkScene_s3_3":
//                SceneManager.LoadScene("Stage1");
//                break;
//        }
//    }
//}
