using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.7f;


    void Start()
    {
        StartCoroutine(Shake());
        Debug.Log("흔들림 효과 주는중");
    }

    private IEnumerator Shake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 originalPosition = rect.anchoredPosition;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1, 1) * shakeMagnitude;
            //float y = Random.Range(-1, 1) * shakeMagnitude;

            rect.anchoredPosition = originalPosition + new Vector2(x, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Restore original position
        rect.anchoredPosition = originalPosition;
    }
}
