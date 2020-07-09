using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scripatble Object/Card")]
public class StatsCard : ScriptableObject
{
    //Care Case Sensitive
    public string Name;
    public int Cout;

    public string Description;

    public Sprite Sprite;
}
