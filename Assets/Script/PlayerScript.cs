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

    public int MaxHp;

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

    public int MaxMana;
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
    /// Stats that define the player Physical Damage
    /// </summary>
    public int Str;

    /// <summary>
    /// Stats that define the player Magical Damage
    /// </summary>
    public int Mag;

    /// <summary>
    /// Stats that define the player Supportive Magic strength
    /// </summary>
    public int Wis;
    
    /// <summary>
    /// Nom du joeur
    /// </summary>
    public string Name;

    public List<string> Deck = new List<string>();

    public List<string> CardInventory = new List<string>();

    public int Souls = 0;

    public int Golds = 0;

    public int HPPots = 0;
    public int ManaPots = 0;

    /*public void Init()
    {
        MaxHp = 30;
        MaxMana = 30;
        Hp = MaxHp;
        Mana = MaxMana;
        Name = "Hero";
        Str = 5;
        Mag = 5;
        Wis = 5;

        Souls = 10;

        ManaPots = 2;
        HPPots = 2;

        Deck.Add("Fire I");
        Deck.Add("Fire I");
        Deck.Add("Water I");
        Deck.Add("Water I");
        Deck.Add("Thunder I");
        Deck.Add("Thunder I");
        Deck.Add("Wind I");
        Deck.Add("Wind I");
    }*/

    public PlayerScript(int maxHp, int maxMana, string name)
    {
        MaxHp = maxHp;
        MaxMana = maxMana;
        Hp = MaxHp;
        Mana = MaxMana;
        Name = name;
       
        Str = 5;
        Mag = 5;
        Wis = 5;

        Souls = 10;

        ManaPots = 2;
        HPPots = 2;

        Deck.Add("Fire I");
        Deck.Add("Fire I");
        Deck.Add("Water I");
        Deck.Add("Water I");
        Deck.Add("Thunder I");
        Deck.Add("Thunder I");
        Deck.Add("Wind I");
        Deck.Add("Wind I");
    }

    public void PlayerDisp_OnHpChange(int newVal)
    {
        if (hpvaria > 0) CardReader.ActionDescription = $"{Name} received {hpvaria} damage";
        else CardReader.ActionDescription = $"{Name} healed for {hpvaria * -1} Hp";
    }
}
