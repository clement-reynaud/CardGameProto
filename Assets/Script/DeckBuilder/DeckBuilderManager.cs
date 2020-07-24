using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilderManager : MonoBehaviour
{
    public Image CurrentDeckList;
    public Image CardInvetoryList;



    // Start is called before the first frame update
    void Start()
    {
        Data.Player.Deck.Sort();
    }


}
