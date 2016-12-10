using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeScreen : MonoBehaviour {

    public static FadeScreen instance;

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

            if (timer >= duration / 2f)
            {
                callback.Invoke();
            }
        }
    }

    public void Animate(float duration, UnityAction callback)
    {
        this.callback = callback;
        this.duration = duration;
        this.timer = 0;
    }
}
