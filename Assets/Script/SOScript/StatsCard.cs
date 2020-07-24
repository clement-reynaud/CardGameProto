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

    public Sprite Sprite;

}
