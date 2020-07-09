using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Scripatble Object/Player")]
public class StatsPlayer : ScriptableObject
{
    public string Name;

    public int Hp;
    public int Mana;

    public Sprite Sprite;
}
