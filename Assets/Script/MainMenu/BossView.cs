using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossView : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	void Update()
	{
		if(LevelSelectObject.RepopulateBoss)
        {
			DeleteAndPopulate();
			LevelSelectObject.RepopulateBoss = false;
		}

	}


	void Populate()
	{
		GameObject newObj; // Create GameObject instance

		for (int i = 0; i < LevelSelectObject.LevelDescBoss.Count; i++)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);
			newObj.GetComponent<EnemyDescObject>().Sprite = LevelSelectObject.LevelDescBoss[i].Sprite;
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
