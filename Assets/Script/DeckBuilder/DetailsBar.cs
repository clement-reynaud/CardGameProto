﻿using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetailsBar : MonoBehaviour
{
    private int maxNbOfCardInDeck = 20;

    public TextMeshProUGUI cardInDeck;

    private void Update()
    {
        cardInDeck.text = $"{Data.Player.Deck.Count}/{maxNbOfCardInDeck}";
        if(Data.Player.Deck.Count == 1 || Data.Player.Deck.Count == maxNbOfCardInDeck)
        {
            cardInDeck.color = Color.red;
        }
        else
        {
            cardInDeck.color = Color.black;
        }
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("Menu");
    }


    public void CraftButton()
    {
        SceneManager.LoadScene("CardCrafter");
    }


}   