using Assets.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour
{
	public TextMeshProUGUI ProgressionText;
	public Image ProgressBarCore;

	// Update is called once per frame
	void Update()
	{
		if (GameData.EncounterTableAdvancement < GameData.ActualEncounterTable.NbOfNormalEncounter + GameData.ActualEncounterTable.NbOfBossEncounter)
		{
			ProgressionText.text = $"{GameData.EncounterTableAdvancement}/{GameData.ActualEncounterTable.NbOfNormalEncounter + GameData.ActualEncounterTable.NbOfBossEncounter}";
		}
		else
		{
			ProgressionText.text = "<color=red>BOSS</color>";
		}
		ProgressBarCore.fillAmount = ((float)GameData.EncounterTableAdvancement) / (GameData.ActualEncounterTable.NbOfNormalEncounter + GameData.ActualEncounterTable.NbOfBossEncounter);
	}
}	