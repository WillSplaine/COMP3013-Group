using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_player : MonoBehaviour
{
    public GameObject pauseMenu = null;
    public GameObject Button_resume = null;

    public bool isPaused;
    public bool isResume;

    // Start is called before the first frame update
    void Start()
    {

        isPaused = false;
        pauseMenu.SetActive(isPaused);
        Button_resume.SetActive(isPaused);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            isResume = true;
            PauseFunction();            
        }    
    }

    public void PauseFunction()
    {

        if (isResume == false)
        {
            isPaused = true;
            Button_resume.SetActive(false);
        }
        else
        {
            Button_resume.SetActive(true);
            isPaused = !isPaused;
        }

        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        //print(isPaused);
    }

}
