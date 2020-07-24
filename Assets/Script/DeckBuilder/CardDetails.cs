using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDetails : MonoBehaviour
{
    public Image CardImage;
    public TextMeshProUGUI TextDesc;

    public PopulateActualDeck DeckDisplay;
    public PopulateCardInventory InvDisplay;

    public Button AddButton;
    public Button RemoveButton;

    public static Type CardType;
    public static StatsCard selected;

    private void Start()
    { 
    }

    private void Update()
    {
        if (selected != null) { 
            CardImage.sprite = selected.Sprite;
            TextDesc.text = Data.CardDetailsToString(selected);
            if (CardType == typeof(CardListObject))
            {
                TextDesc.text += "\n\n<u>In Deck</u>";
                AddButton.interactable = false;
                RemoveButton.interactable = true;
            }
            else if (CardType == typeof(CardInventoryObject)){ 
                TextDesc.text += "\n\n<u>In Inventory</u>";
                AddButton.interactable = true;
                RemoveButton.interactable = false;
            }
        }
        else
        {
            CardImage.sprite = Resources.Load<Sprite>("Sprites/CardBack");
            TextDesc.text = "";
            AddButton.interactable = false;
            RemoveButton.interactable = false;
        }
    }

    public void AddCardToDeck()
    {
        if(Data.Player.Deck.Count < 20)
        {
            Data.Player.Deck.Add(selected.Name);
            Data.Player.CardInventory.Remove(selected.Name);
        }
        else
        {
            //Message d'erreur
        }
        RefreshDisplay();
    }

    public void RemoveCardFromDeck()
    {
        if (Data.Player.Deck.Count > 1)
        {
            Data.Player.CardInventory.Add(selected.Name);
            Data.Player.Deck.Remove(selected.Name);
        }
        else
        {
            //Message d'erreur
        }
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        selected = null;
        CardType = null;
        DeckDisplay.DeleteAndPopulate();
        InvDisplay.DeleteAndPopulate();
        Data.SavePlayer();
    }
}
