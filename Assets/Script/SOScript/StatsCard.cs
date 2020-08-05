using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Scripatble Object/Card/Card")]
public class StatsCard : ScriptableObject
{
    //Care Case Sensitive
    public string Name;
    public int Cout;

    public string Description;

    public Rarity rarity;

    public Sprite Sprite;

}

public enum Rarity
{
    NONE,
    COMMON,
    UNCOMMON,
    RARE,
    MYTHIC,
    LEGENDARY,
    UNIQUE
}