using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBooster", menuName = "Scripatble Object/Card/Booster")]
public class Booster : ScriptableObject
{
    int cardDrawed = 5;

    public int commonWeight = 30;
    public int uncommonWeight = 20;
    public int rareWeight = 10;
    public int mythicWeight = 6;
    public int legendaryWeight = 3;
    public int uniqueWeight = 1;

    public List<StatsCard> commonCards = new List<StatsCard>();
    public List<StatsCard> uncommonCards = new List<StatsCard>();
    public List<StatsCard> rareCards = new List<StatsCard>();
    public List<StatsCard> mythicCards = new List<StatsCard>();
    public List<StatsCard> legendaryCard = new List<StatsCard>();
    public List<StatsCard> uniqueCard = new List<StatsCard>();
}

