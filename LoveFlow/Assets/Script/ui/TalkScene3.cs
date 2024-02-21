using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkScene3 : MonoBehaviour
{
    public Image[] images1;
    public Image[] images2;

    public Image[] bg;

    private int currentImageIndex = 0;
    public float fadeOutDuration = 1f;
    public float fadeInDuration = 0.5f;

    private int buttonPressCount = 0;

    private void Update()
    {
        // 'A' ��ư�� ���ȴ��� ����
        if (Input.GetButtonDown("Player1_AButton") || Input.GetButtonDown("Player2_AButton") || Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlayEffect(SoundManager.ClipEffect.Input_A);
            buttonPressCount++;

            if (buttonPressCount < 4)
            {
                if (currentImageIndex < images1.Length)
                {
                    StartCoroutine(FadeIn(images1[currentImageIndex], fadeInDuration));
                    currentImageIndex++;
                }
            }
            else if(buttonPressCount<6)
            {
                bg[0].gameObject.SetActive(false);
                bg[1].gameObject.SetActive(true);

                if (buttonPressCount == 4)
                {
                    // Images1 �迭�� ��� �̹����� ��Ȱ��ȭ
                    foreach (Image image in images1)
                    {
                        image.gameObject.SetActive(false);
                    }
                    currentImageIndex = 0;
                }

                // Images2 �迭�� ��ȯ
                if (currentImageIndex < images2.Length)
                {
                    StartCoroutine(FadeIn(images2[currentImageIndex], fadeOutDuration));
                    currentImageIndex++;
                }        
            }
            else
                SceneManager.LoadScene("Stage3-2");
        }
    }

    IEnumerator FadeIn(Image image, float duration)
    {
        image.gameObject.SetActive(true);
        Color color = image.color;
        color.a = 0f;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            color.a = (Time.time - startTime) / duration;
            image.color = color;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }

    // ȭ�� ��ȯ�� ���̵�ƿ�
    IEnumerator FadeOut(Image image, float duration)
    {
        image.gameObject.SetActive(true);
        Color color = image.color;
        color.a = 0f;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            color.a = (Time.time - startTime) / duration;
            image.color = color;
            yield return null;
        }

        color.a = 1f;
        image.color = color;

        // Once the fade out is complete, load the next scene
       
    }
}
                                           