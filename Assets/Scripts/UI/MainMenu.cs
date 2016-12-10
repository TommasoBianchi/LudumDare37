using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private bool Paused;

    public MainMenu PauseMenu;
    public GameObject UI;
    public GameObject Options;
    public GameObject Controls;
    public bool Control;

	// Use this for initialization
	void Start () {
        Paused = false;
        Control = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            if (Paused)
            {
                Paused = false;
                Time.timeScale = 1;
                UI.gameObject.SetActive(false);
                if (Control)
                {
                    Options.gameObject.SetActive(false);
                    Controls.gameObject.SetActive(false);
                    Control = false;
                }
            }
            else
            {
                Paused = true;
                Time.timeScale = 0;
                UI.gameObject.SetActive(true);
            }
        }        		
	}

    public void LoadScene(int scene)
    {
        Application.LoadLevel(scene);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        PauseMenu.Paused = false;
        UI.gameObject.SetActive(false);
    }

    public void SubMenu()
    {
        PauseMenu.Control = true;
    }
}
