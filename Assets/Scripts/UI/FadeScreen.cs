using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeScreen : MonoBehaviour {

    public Text messageText;

    static FadeScreen instance;

    Image fadePanel;
    float timer;
    float duration;
    UnityAction callback;

    void Start()
    {
        fadePanel = GetComponent<Image>();

        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (timer < duration)
        {
            Color color = fadePanel.color;

            float alpha = timer / duration * 2;
            color.a = Mathf.PingPong(alpha, 1);

            fadePanel.color = color;

            Color messageColor = messageText.color;
            messageColor.a = color.a;
            messageText.color = messageColor;

            timer += Time.deltaTime;

            if (timer >= duration / 2f && callback != null)
            {
                callback.Invoke();
                callback = null;
            }
        }
    }

    public static void Animate(float duration, UnityAction callback)
    {
        instance.callback = callback;
        instance.duration = duration;
        instance.timer = 0;
        instance.messageText.text = "";
    }

    public static void Animate(float duration, UnityAction callback, string message)
    {
        Animate(duration, callback);
        instance.messageText.text = message;
    }

    public static bool IsAnimating()
    {
        return instance.timer < instance.duration;
    }
}
