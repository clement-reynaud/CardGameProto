using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardCrafted : MonoBehaviour
{
    public StatsCard card;

    public Image CardImage;
    public TextMeshProUGUI CardName;

    private void Start()
    {
        CardImage.sprite = Resources.Load<Sprite>("Sprites/CardBack");
        CardName.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RevealButtonClick()
    {
        CardImage.sprite = card.Sprite;
        CardName.text = card.Name;
    }
}
