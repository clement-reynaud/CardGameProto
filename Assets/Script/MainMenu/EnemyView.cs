using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	void Update()
	{
		if (LevelSelectObject.RepopulateEnemy)
		{
			DeleteAndPopulate();
			LevelSelectObject.RepopulateEnemy = false;
		}
	}


	void Populate()
	{
		GameObject newObj; // Create GameObject instance

		for (int i = 0; i < LevelSelectObject.LevelDescEnemy.Count; i++)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);
			newObj.GetComponent<EnemyDescObject>().Sprite = LevelSelectObject.LevelDescEnemy[i].Sprite;
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
