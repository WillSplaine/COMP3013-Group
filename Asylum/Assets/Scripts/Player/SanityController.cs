using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SanityController : MonoBehaviour
{
    [Header("Sanity")]
    public Slider slider_sanity;
    public static int sanityValue; //readable between scripts
    [SerializeField] int sanityValueReader;
    public int initialSanityValue = 1; //starting value
    [SerializeField] int maxSanityValue; //max value
    public float SanityTickDown = 0.1f; //seconds// rate that sanity decreases
    public static float SanityRateMultiplier = 1.0f; //multiplier for sanity decrease rate

    // Start is called before the first frame update
    void Start()
    {
        slider_sanity_setup();
        StartCoroutine(DecreaseSanityOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        slider_sanity.value = sanityValue; //sets current value
        sanityValueReader = sanityValue; //display current value
        if (sanityValue <= 0) //game over
        {
            GameEnd.GameProgState = 2;
        }
    }

    IEnumerator DecreaseSanityOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(SanityTickDown * SanityRateMultiplier);
            Sanity_decr();
        }
    }

    //Sanity Functions
    //When the script is started the values are initiated and will start at these values
    public void slider_sanity_setup()
    {
        slider_sanity.maxValue = maxSanityValue; //sets the  limit for the health
        sanityValue = initialSanityValue;
    }
    //Mental Health
    public void Sanity_decr()
    {
        sanityValue--;
    }
    public void Sanity_incr()
    {
        sanityValue++;
    }
}
