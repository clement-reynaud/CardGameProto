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
            TextDesc.text = GameData.CardDetailsToString(selected);
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
        if(GameData.Player.Deck.Count < 20)
        {
            GameData.Player.Deck.Add(selected.Name);
            GameData.Player.CardInventory.Remove(selected.Name);
        }
        else
        {
            //Message d'erreur
        }
        RefreshDisplay();
    }

    public void RemoveCardFromDeck()
    {
        if (GameData.Player.Deck.Count > 1)
        {
            GameData.Player.CardInventory.Add(selected.Name);
            GameData.Player.Deck.Remove(selected.Name);
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
        GameData.SavePlayer();
    }
}
