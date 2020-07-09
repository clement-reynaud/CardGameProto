using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy",menuName = "Scripatble Object/Enemy")]
public class StatsEnemy : ScriptableObject
{
    public string Name;

    public int Hp;

    public List<Elements> Weakness = new List<Elements>();

    public Sprite Sprite;

}
