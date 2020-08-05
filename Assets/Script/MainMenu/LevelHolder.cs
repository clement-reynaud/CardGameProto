using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour
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
		List<EncounterTable> allLevel = new List<EncounterTable>(Resources.LoadAll<EncounterTable>("Scriptable Object/EncounterTable"));
		GameObject newObj; // Create GameObject instance

		for (int i = 0; i < allLevel.Count; i++)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);
			newObj.GetComponent<LevelSelectObject>().level = allLevel[i];
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
