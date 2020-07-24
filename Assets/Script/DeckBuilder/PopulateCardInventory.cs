using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateCardInventory : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector
	void Start()
	{
		Populate();
		Debug.Log(Data.Player.CardInventory.Count);	
	}

	void Update()
	{

	}

	public void Populate()
	{
		Data.Player.CardInventory.Sort();
		Data.Player.Deck.Sort();

		List<StatsCard> cardInventory = Data.StringDeckToCardDeck(Data.Player.CardInventory);
		GameObject newObj; // Create GameObject instance

		for (int i = 0; i < cardInventory.Count; i++)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);
			newObj.GetComponent<CardInventoryObject>().card = cardInventory[i];
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
