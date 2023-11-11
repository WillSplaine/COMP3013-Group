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

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.GetComponent<Image>().color = OnHoverColour;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        image.GetComponent<Image>().color = new Color32(220, 220, 220, 255); //white
    }
}
