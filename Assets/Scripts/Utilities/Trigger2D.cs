using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Trigger2D : MonoBehaviour {

    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerStay;
    public UnityEvent OnTriggerExit;

    private bool isIn = false;

    void OnTriggerEnter2D()
    {
        OnTriggerEnter.Invoke();
        isIn = true;
    }

    void Update()
    {
        if(isIn)
            OnTriggerStay.Invoke();
    }

    void OnTriggerExit2D()
    {
        OnTriggerExit.Invoke();
        isIn = false;
    }
}
