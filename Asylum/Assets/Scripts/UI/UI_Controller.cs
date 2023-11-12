using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    [Header("Menu Game Objects")]
    public GameObject pauseMenu = null;
    public GameObject Button_resume = null;
    public GameObject GameWon = null;
    public GameObject GameLost = null;

    private bool isPaused;
    private bool isResume;

    [Header("Transition Settings")]
    public GameObject blackOutSquare;
    [Range(0.1f, 5.0f)] public float adj_FadeInSpeed;
    [Range(0.1f, 5.0f)] public float adj_FadeOutSpeed;

    [Header("Sanity")]
    public Slider slider_sanity;
    public static int sanityValue; //readable between scripts
    public int initialSanityValue = 1;
    public int maxSanityValue;

    void Start()
    {
        StartCoroutine(FadeBlackOutSquare(false, adj_FadeInSpeed));

        isPaused = false;
        pauseMenu.SetActive(isPaused);
        Button_resume.SetActive(isPaused);
        GameWon.SetActive(isPaused);
        GameLost.SetActive(isPaused);

        slider_sanity_setup();
        StartCoroutine(DecreaseSanityOverTime());
    }

    void Update()
    {
        slider_sanity.value = sanityValue; //sets current value

        if (Input.GetKeyDown(KeyCode.Escape) && GameEnd.GameProgState == 0) //actively playing
        {
            isResume = true;
            PauseFunction();
        }
        if (GameEnd.GameProgState == 1) //game won 
        {
            print("Game Won");
            isResume = false;
            StartCoroutine(FadeBlackOutSquare(true, adj_FadeOutSpeed, true));
        }
        DeathFunction();
    }

    IEnumerator DecreaseSanityOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f);
            Sanity_decr();
        }
    }

    public void DeathFunction()
    {
        if (sanityValue < 1)
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
            GameLost.SetActive(isPaused);
            Cursor.visible = isPaused;
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
            //print(isPaused); //debug
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
        //print(isPaused); //debug
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
                GameWon.SetActive(true);
                pauseMenu.SetActive(false);
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


    //Sanity Functions
    //When the script is started the values are initiated and will start at these values
    public void slider_sanity_setup()
    {
        sanityValue = initialSanityValue;
        slider_sanity.maxValue = maxSanityValue; //sets the  limit for the health
    }
    //Mental Health
    public void Sanity_decr()
    {
        print("Sanity Decreased");
        sanityValue--; 
    }
    public void Sanity_incr()
    {
        print("Sanity Increased");
        slider_sanity.value += 1; 
    }


}


