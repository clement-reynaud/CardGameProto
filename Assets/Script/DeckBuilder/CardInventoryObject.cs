using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInventoryObject : MonoBehaviour
{
    public StatsCard card;

    public Image CardImage;
    public TextMeshProUGUI CardName;
    public TextMeshProUGUI CardCost;

    // Update is called once per frame
    void Update()
    {
        CardImage.sprite = card.Sprite;
        CardName.text = card.Name;
        CardCost.text = $"{card.Cout}";
    }

    public void DetailsButtonClick()
    {
        CardDetails.CardType = GetType();
        CardDetails.selected = card;
    }
}
