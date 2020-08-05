using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardCrafted : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public StatsCard card;

    public UnityEngine.UI.Image CardImage;
    public UnityEngine.UI.Image BG;
    public TextMeshProUGUI CardName;

    private void Start()
    {
        CardImage.sprite = Resources.Load<Sprite>("Sprites/CardBack");
        CardName.text = "";
    }

    public void RevealButtonClick()
    {
        CardImage.sprite = card.Sprite;
        CardName.text = card.Name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BG.color = new Color(BG.color.r, BG.color.g, BG.color.b, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BG.color = new Color(BG.color.r, BG.color.g, BG.color.b, 0);
    }
}
