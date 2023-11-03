using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Diagnostics;

public class UI_Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color32 OnHoverColour;

    private bool OnHover = false;


    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover = true;
        image.GetComponent<Image>().color = OnHoverColour;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnHover = false;
        image.GetComponent<Image>().color = new Color32(220, 220, 220, 255); //white
    }
}
