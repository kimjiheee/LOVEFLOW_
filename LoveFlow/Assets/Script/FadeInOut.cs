using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public float fadeTime = 2.0f;
    [SerializeField]
    private Image fadeImage;
    public bool isButtonClicked = false;
    private Button myButton;
    private float time = 0.0f;
    private bool isFadeOutStop = false;
    private bool isFadeInStop = true;
   

    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(SetButtonOn);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isButtonClicked)
            return;
        PlayFadeOut();
        PlayFadeIn();
    }

    public void SetButtonOn()
    {
        if(!isButtonClicked)
            isButtonClicked = true;
    }

    public void PlayFadeOut()
    {
        if (isFadeOutStop)
            return;

        time += Time.deltaTime / fadeTime;
        Color color = fadeImage.color;
        color.a = Mathf.Lerp(1.0f, 0.0f, time);
        fadeImage.color = color;

        if(time > fadeTime)
        {
            isFadeOutStop = true;
            isFadeInStop = false;
            time = 0.0f;
            isButtonClicked = false;
        }
    }

    public void PlayFadeIn()
    {
        if (isFadeInStop)
            return;

        time += Time.deltaTime / fadeTime;
        Color color = fadeImage.color;
        color.a = Mathf.Lerp(0.0f, 1.0f, time);
        fadeImage.color = color;

        if (time > fadeTime)
        {
            isFadeInStop = true;
            isFadeOutStop = false;
            time = 0.0f;
            isButtonClicked = false;
        }
    }
}
