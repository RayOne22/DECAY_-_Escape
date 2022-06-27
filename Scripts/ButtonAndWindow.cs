using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAndWindow: MonoBehaviour
{
    Image image;
    Text text;


    void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponent<Text>();
    }

    IEnumerator ColorAlphaObjectOpen()
    {
        float c;
        Color color = image.color;
        for (c = 0f; c < 1.08f;c += 0.06f)
        {
            color.a = c;
            image.color = color;
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }

    IEnumerator ColorAlphaObjectClose()
    {
        float c;
        Color color = image.color;
        for (c = 1f; c > -0.08f; c -= 0.06f)
        {
            color.a = c;
            image.color = color;
            yield return new WaitForSecondsRealtime(0.001f);
        }
        if (c < 0f)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator ColorAlphaPauseOpen()
    {
        float c;
        Color color = image.color;
        for (c = 0f; c < 0.54f; c += 0.03f)
        {
            color.a = c;
            image.color = color;
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }
  
    IEnumerator ColorAlphaPauseClose()
    {
        float c;
        Color color = image.color;
        for (c = 0.54f; c > 0f; c -= 0.03f)
        {
            color.a = c;
            image.color = color;
            yield return new WaitForSecondsRealtime(0.001f);
        }
        if (c < 0)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator ColorAlphaTextOpen()
    {
        float c;
        Color color = text.color;
        for (c = 0f; c < 1.08f; c += 0.06f)
        {
            color.a = c;
            text.color = color;
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }

    IEnumerator ColorAlphaTextClose()
    {
        float c;
        Color color = text.color;
        for (c = 1f; c > -0.08f; c -= 0.06f)
        {
            color.a = c;
            text.color = color;
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }
}
