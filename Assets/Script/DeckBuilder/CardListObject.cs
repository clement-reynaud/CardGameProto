using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardListObject : MonoBehaviour
{
    public StatsCard card;

    public TextMeshProUGUI CardName;
    public TextMeshProUGUI CardCost;

    // Update is called once per frame
    void Update()
    {
        CardName.text = card.Name;
        CardCost.text = $"{card.Cout}";
    }

    public void DetailsButtonClick()
    {
        CardDetails.CardType = GetType();
        CardDetails.selected = card;
    }



}
