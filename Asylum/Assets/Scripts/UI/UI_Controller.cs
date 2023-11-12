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
    public GameObject obj_GameOver = null;
    //public GameObject GameLost = null;
    public TextMeshProUGUI txt_GameEnd = null;

    private bool isPaused;
    private bool isResume;

    [Header("Transition Settings")]
    public GameObject blackOutSquare;
    [Range(0.1f, 5.0f)] public float adj_FadeInSpeed;
    [Range(0.1f, 5.0f)] public float adj_FadeOutSpeed;

    [Header("Sanity")]
    public Slider slider_sanity;
    public static int sanityValue; //readable between scripts
    public int initialSanityValue = 1; //starting value
    public int maxSanityValue; //max value
    public float SanityTickDown = 0.1f; //seconds// rate that sanity decreases
    public TextMeshProUGUI txt_Objective = null; //

    void Start()
    {
        StartCoroutine(FadeBlackOutSquare(false, adj_FadeInSpeed));
        
        isPaused = false;
        pauseMenu.SetActive(isPaused);
        Button_resume.SetActive(isPaused);
        obj_GameOver.SetActive(isPaused);
        //GameLost.SetActive(isPaused);

        slider_sanity_setup();
        StartCoroutine(DecreaseSanityOverTime());
        StartCoroutine(TextVanishOverTime());
    }

    void Update()
    {
        slider_sanity.value = sanityValue; //sets current value
        if (sanityValue <= 0) //game over
        {
            GameEnd.GameProgState = 2;
        }

        switch (GameEnd.GameProgState)
        {
            case 1:
                print("Game Won");
                isResume = false;
                StartCoroutine(FadeBlackOutSquare(true, adj_FadeOutSpeed, true));
                break;

            case 2:
                print("Game Lost");
                isResume = false;
                StartCoroutine(FadeBlackOutSquare(true, adj_FadeOutSpeed, true));
                break;

            default:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isResume = true;
                    PauseFunction();
                }
                break;
        }
    }

    IEnumerator DecreaseSanityOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(SanityTickDown);
            Sanity_decr();
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


    //Sanity Functions
    //When the script is started the values are initiated and will start at these values
    public void slider_sanity_setup()
    {
        
        slider_sanity.maxValue = maxSanityValue; //sets the  limit for the health
        //initialSanityValue = maxSanityValue; not needed as value is set in inspector
        sanityValue = initialSanityValue;
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
        slider_sanity.value++; 
    }
}


