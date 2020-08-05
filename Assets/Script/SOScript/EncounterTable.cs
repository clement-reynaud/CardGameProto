using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEncounterTable", menuName = "Scripatble Object/Enemy/Encounter Table")]
public class EncounterTable : ScriptableObject
{
    [Header("Details:")]
    public string Name;

    public Color GradientColor1;
    public Color GradientColor2;

    [Header("Encounter:")]
    public int NbOfNormalEncounter = 2;
    public int NbOfBossEncounter = 1;

    public List<StatsEnemy> NormalEncounterEnemy = new List<StatsEnemy>();
    public List<StatsEnemy> BossEncounterEnemy = new List<StatsEnemy>();

    [Header("Reward:")]
    public int SoulsReward = 15;
}
