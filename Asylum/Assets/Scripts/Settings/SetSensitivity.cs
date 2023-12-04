using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SetSensitivity : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI SensValue;
    [SerializeField] Slider SensSlider;

    private void Start()
    {
        SensSlider.value = PlayerCam.sensX;
    }
    private void Update()
    {
        SensValue.text = RoundToSignificantFigures(SensSlider.value, 2).ToString();
        PlayerCam.sensX = SensSlider.value;
        PlayerCam.sensY = SensSlider.value;
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
}
