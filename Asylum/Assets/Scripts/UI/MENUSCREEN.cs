using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Diagnostics;

public class MENUSCREEN : MonoBehaviour
{
    public GameObject controlsImage = null;
    public AudioSource Click;

    public static bool hasWon = false;

    private bool showControlEnabled;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;

        showControlEnabled = false;
        controlsImage.SetActive(showControlEnabled);
    }


    public void startGame() 
    {
        Click.Play();
        SceneManager.LoadScene(1);
        print("Game Started");
    }

   public void return2menu()
    {
        Click.Play();
        SceneManager.LoadScene(0);
        print("Game finished");
    }
    public void showControls()
    {
        Click.Play();
        showControlEnabled = !showControlEnabled;
        controlsImage.SetActive(showControlEnabled);
    }
    public void exitgame()
    {
        Click.Play();
        Application.Quit();
        print("exited");
    }
}
