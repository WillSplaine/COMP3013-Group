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

    [Header("Transition Settings")]
    public GameObject blackOutSquare;
    [Range(0.1f, 5.0f)] public float adj_FadeInSpeed;
    [Range(0.1f, 5.0f)] public float adj_FadeOutSpeed;

    private bool showControlEnabled;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;

        showControlEnabled = false;
        controlsImage.SetActive(showControlEnabled);
        StartCoroutine(FadeBlackOutSquare(false, adj_FadeInSpeed));
    }


    public void startGame() 
    {
        Click.Play();
        StartCoroutine(FadeBlackOutSquare(true, adj_FadeOutSpeed, true));
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

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, float fadeSpeed = 1, bool GameStart = false)
    {
        float fadeAmount;
        Color objectColor = blackOutSquare.GetComponent<Image>().color;

        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount); blackOutSquare.GetComponent<Image>().color = objectColor;
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
            if (GameStart)
            {
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
