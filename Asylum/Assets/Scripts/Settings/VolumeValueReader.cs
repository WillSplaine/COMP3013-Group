using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class VolumeValueReader : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private TextMeshProUGUI sliderTxt;
    [SerializeField] private Slider VolSlider;
    [Header("Sound Objects")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string exposedParam;

    void Start()
    {
        audioMixer.GetFloat(exposedParam, out float value);
        VolSlider.value = RemapValue(value);
        sliderTxt.text = VolSlider.value.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        float PercentagedValue = Mathf.Lerp(0f, 100f, VolSlider.value / VolSlider.maxValue);
        sliderTxt.text = RoundToSignificantFigures((int)PercentagedValue, 2).ToString() + "%";
    }
    public string RoundToSignificantFigures(float value, int figures)
    {
        if (value == 0)
            return "0";

        float magnitude = Mathf.Abs(value);
        int digits = Mathf.Max(1, Mathf.FloorToInt(Mathf.Log10(magnitude)) + 1);

        int decimals = figures - digits;
        float rounded = Mathf.Round(value * Mathf.Pow(10, decimals)) / Mathf.Pow(10, decimals);

        return rounded.ToString();
    }
    float RemapValue(float value)
    {
        // Assuming the value is between -80 and 0 (adjust if different)
        float minDb = -120f;
        float maxDb = 0f;

        // Remap the value from the decibel range to the slider range (0-100)
        return Mathf.InverseLerp(minDb, maxDb, value) * VolSlider.maxValue;
    }
}
