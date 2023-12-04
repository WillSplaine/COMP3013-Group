using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    [Header("Menu Game Objects")]
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] GameObject Button_resume = null;
    [SerializeField] GameObject obj_GameOver = null;
    //[SerializeField] GameObject GameLost = null;
    [SerializeField] TextMeshProUGUI txt_GameEnd = null;

    private bool isPaused;
    private bool isResume;

    [Header("Transition Settings")]
    [SerializeField] GameObject blackOutSquare;
    [Range(0.1f, 5.0f)] public float adj_FadeInSpeed;
    [Range(0.1f, 5.0f)] public float adj_FadeOutSpeed;

    [Header("HUD")]
    [SerializeField] TextMeshProUGUI txt_Objective = null; 

    void Start()
    {
        StartCoroutine(FadeBlackOutSquare(false, adj_FadeInSpeed));

        isPaused = false;
        pauseMenu.SetActive(isPaused);
        Button_resume.SetActive(isPaused);
        obj_GameOver.SetActive(isPaused);
        //GameLost.SetActive(isPaused);

        StartCoroutine(TextVanishOverTime());
    }

    void Update()
    {

        switch (GameEnd.GameProgState)
        {
            
            case 1: //Game Won
                //print("Game Won");
                isResume = false;
                StartCoroutine(FadeBlackOutSquare(true, adj_FadeOutSpeed, true));
                break;

            case 2: //Game Lost
                //print("Game Lost");
                isResume = false;
                StartCoroutine(FadeBlackOutSquare(true, adj_FadeOutSpeed, true));
                break;
            case 3: //Game paused, in settings
                isResume = false;
                PauseFunction();
                pauseMenu.SetActive(false);
                break;
            case 4: //transitioning from Settings to Paused
                pauseMenu.SetActive(true);
                Button_resume.SetActive(true);
                isResume = true;
                isPaused = true;
                GameEnd.GameProgState = 0;
                break;
            default: //Walking and pausing 
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    pauseMenu.SetActive(true);
                    isResume = true;
                    PauseFunction();
                }
                break;
        }
    }

    IEnumerator TextVanishOverTime(float fadeSpeed = 0.15f)
    {
        float fadeAmount;
        Color objectColor = txt_Objective.GetComponent<TextMeshProUGUI>().color;

        while (txt_Objective.GetComponent<TextMeshProUGUI>().color.a > 0)
        {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            txt_Objective.GetComponent<TextMeshProUGUI>().color = objectColor;
            yield return null;
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
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, float fadeSpeed = 1, bool GameOver = false)
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
            if (GameOver)
            {
                PauseFunction();
                pauseMenu.SetActive(false);
                obj_GameOver.SetActive(true);
                switch (GameEnd.GameProgState)
                {
                    case 1:
                        txt_GameEnd.text = "You Escaped!";
                        break;
                    case 2:
                        txt_GameEnd.text = "You Died!";
                        break;
                }
                GameEnd.GameProgState = 0;

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





