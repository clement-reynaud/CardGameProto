using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public Slider EncounterProgress;
    public TextMeshProUGUI SoulsText;
    public TextMeshProUGUI GoldsText;

    public static int acumulatedGoldsReward;
    public static int acumulatedSoulsReward;

    private void Start()
    {
        acumulatedGoldsReward = 0;
        acumulatedSoulsReward = 0;
        EncounterProgress.maxValue = GameData.ActualEncounterTable.NbOfNormalEncounter + GameData.ActualEncounterTable.NbOfBossEncounter;
    }

    private void Update()
    {
        SoulsText.text = $"{acumulatedSoulsReward}";
        GoldsText.text = $"{acumulatedGoldsReward}";
        EncounterProgress.value = GameData.EncounterTableAdvancement;
    }
}
