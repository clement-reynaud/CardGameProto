using Assets.Script;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using TMPro.EditorUtilities;
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
        //Temporaire. Rajoute toutes les cartes du jeu dans le deck, puis mélange le deck
        allCard = Resources.LoadAll<StatsCard>("Scriptable Object/Card");
        foreach (StatsCard item in allCard)
        {
            _deck.Add(item);
            _deck.Add(item);
        }
        Data.Shuffle(_deck);

        TakeCard(4);
    }

    /*private void OnMouseDown()
    {
        if (Data.gameState == Assets.Script.Enums.States.PlayerTurn)
        {
            TakeCard(1);
            cardReader.BroadcastMessage("ActionPick");
        }
    }*/

    public void OnClick()
    {
        if (Data.gameState == Assets.Script.Enums.States.PlayerTurn)
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
        newCard.transform.DOMove(new Vector3(Data.rng.Next(-3, 3), Data.rng.Next(-4, -2), 0), 0.5f);
        cardInHand.Add(newCard);
    }

}
