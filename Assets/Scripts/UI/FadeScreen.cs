using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeScreen : MonoBehaviour {

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
            color.a = Mathf.PingPong(timer, duration / 2f);
            fadePanel.color = color;
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
    }
}
