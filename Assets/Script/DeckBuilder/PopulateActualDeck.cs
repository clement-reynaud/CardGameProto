using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateActualDeck : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	void Start()
	{
		Populate();
	}

	void Update()
	{

	}

	void Populate()
	{
		GameData.Player.CardInventory.Sort();
		GameData.Player.Deck.Sort();

		List<StatsCard> playerDeck = GameData.PlayerStringDeckToCardDeck();
		GameObject newObj; // Create GameObject instance

		for (int i = 0; i < playerDeck.Count; i++)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);
			newObj.GetComponent<CardListObject>().card = playerDeck[i];
		}
		;
	}

	public void DeleteAndPopulate()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
		Populate();
	}
}
