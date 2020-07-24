using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerScript
{
    /// <summary>
    /// Sprite a affiché
    /// </summary>
    [NonSerialized]
    public Sprite Sprite;

    public List<string> Deck = new List<string>();

    public List<string> CardInventory = new List<string>();

    /// <summary>
    /// Pv du actuel du joueur
    /// </summary>
    public int Hp {
        get { return hp; }
        set
        {
            if (hp == value) return;
            hpvaria = hp - value;
            hp = value;
            OnHpChange?.Invoke(hp);
        }
    }
    public int hp;
    /// <summary>
    /// Variation des Pv lors du dernier changement de pv, positif = a recu des dmg, négatif = a été soigné
    /// </summary>
    [NonSerialized]
    public int hpvaria;
    /// <summary>
    /// Event appelé au changement des hp du joueur
    /// </summary>
    /// <param name="newVal"> nouvelle valeur des pv du joueur </param>
    public delegate void OnVariableChangeDelegate(int newVal);
    public event OnVariableChangeDelegate OnHpChange;

    /// <summary>
    /// Mana actuel du joueur
    /// </summary>
    public int Mana
    {
        get
        {
            return mana;
        }
        set
        {
            mana = value;
            if (mana < 0) mana = 0;
        } 
    }

    public int mana;
    /// <summary>
    /// Nom du joeur
    /// </summary>
    public string Name;

    public PlayerScript(int hp, int mana, string name)
    {
        Hp = hp;
        Mana = mana;
        Name = name;
    }

    public void PlayerDisp_OnHpChange(int newVal)
    {
        if (hpvaria > 0) CardReader.ActionDescription = $"{Name} received {hpvaria} damage";
        else CardReader.ActionDescription = $"{Name} healed for {hpvaria * -1} Hp";
    }
}
