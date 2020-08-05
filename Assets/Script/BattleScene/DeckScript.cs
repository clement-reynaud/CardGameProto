using Assets.Script;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public CardReader cardReader;

    /// <summary>
    /// Temporaire. Toutes les cartes du jeu
    /// </summary>
    private StatsCard[] allCard;

    /// <summary>
    /// Deck actuel du joueur
    /// </summary>
    public List<StatsCard> _deck;

    public static List<GameObject> cardInHand = new List<GameObject>();

    /// <summary>
    /// Prefab de base de la carte
    /// </summary>
    public UnityEngine.Object cardPrefab;

    public TextMeshProUGUI DescText;

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (cardInHand.Count > 0) DestroyAllCard();
        _deck.Clear();

        _deck = GameData.PlayerStringDeckToCardDeck();
        GameData.Shuffle(_deck);

        TakeCard(4);
    }

    public void OnClick()
    {
        if (GameData.gameState == Assets.Script.Enums.States.PlayerTurn)
        {
            TakeCard(1);
            cardReader.BroadcastMessage("ActionPick");
        }
    }

    /// <summary>
    /// Permet de tirer nb carte du paquet et dans la main
    /// </summary>
    /// <param name="nb">Nombre de carte a tirer</param>
    public void TakeCard(int nb)
    {
        for (int i = 0; i < nb;i++) {
            if (_deck.Count == 0)
            {
                //To do
                Debug.LogError("deck vide");
                return;
            }

            StatsCard card = _deck[0];
            _deck.RemoveAt(0);
            //_discardPile.Add(card);

            CreateCard(card);
        }
    }

    /// <summary>
    /// Permet de physiquement créer une carte dans la main
    /// </summary>
    /// <param name="draw"></param>
    private void CreateCard(StatsCard draw)
    {
        GameObject newCard = Instantiate(cardPrefab) as GameObject;
        CardScript cs = newCard.GetComponent<CardScript>();
        cs.actualCard = draw;
        cs.DescText = this.DescText;
        newCard.transform.position = transform.position;
        newCard.transform.DOMove(new Vector3(GameData.rng.Next(-3, 3), GameData.rng.Next(-4, -2), 0), 0.5f);
        cardInHand.Add(newCard);
    }

    public void DestroyCard(int nb)
    {
        for (int i = 0; i < nb; i++)
        {
            GameObject toDestroy = cardInHand[GameData.rng.Next(DeckScript.cardInHand.Count)];
            Destroy(toDestroy);
        }   
    }

    public void DestroyAllCard()
    {
        foreach (GameObject item in cardInHand)
        {
            Destroy(item);
        }
    }

}
