using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public float waitDuration = 2f; // �̹��� ��ȯ ����
    public float fadeDuration = 4f; // ���̵� ��/�ƿ��� �ɸ��� �ð�

    public Image[] images;


    void Start()
    {
        StartCoroutine(FadeImages());
        SoundManager.Instance.PlayBGM(SoundManager.ClipBGM.BGM_Opening);
    }

    private IEnumerator FadeImages()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(true);
            yield return StartCoroutine(FadeIn(images[i]));

            if (i > 0)
            {
                yield return StartCoroutine(FadeOut(images[i - 1]));
            }
        }

        // ������ �̹����� ���̵� �ƿ�
        yield return FadeOut(images[images.Length - 1]);

        GameManager.SceneChange("Opening_MainScene");       
    }

    private IEnumerator FadeIn(Image image)
    {
        float t = 0f;

        Color c = image.color;
        c.a = 0f;
        image.color = c;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            image.color = c;

            yield return null;
        }
    }

    private IEnumerator FadeOut(Image image)
    {
        float t = 0f;

        Color c = image.color;
        c.a = 1f;
        image.color = c;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            image.color = c;

            yield return null;
        }

        image.gameObject.SetActive(false);
    }
}
