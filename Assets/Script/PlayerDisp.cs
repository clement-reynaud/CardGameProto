using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisp : MonoBehaviour
{
    /// <summary>
    /// Image qui affichera le Sprite du joueur
    /// </summary>
    public Image displayImage;

    /// <summary>
    /// Données du joueurs actuel
    /// </summary>
    public StatsPlayer actualPlayer;

    /// <summary>
    /// Sprite a affiché
    /// </summary>
    public static Sprite actualSprite;

    /// <summary>
    /// Pv du actuel du joueur
    /// </summary>
    public static int actualHp {
        get { return _actualHp; }
        set
        {
            if (_actualHp == value) return;
            hpvaria = _actualHp - value;
            _actualHp = value;
            OnHpChange?.Invoke(_actualHp);
        }
    }
    private static int _actualHp;
    /// <summary>
    /// Variation des Pv lors du dernier changement de pv, positif = a recu des dmg, négatif = a été soigné
    /// </summary>
    public static int hpvaria;
    /// <summary>
    /// Event appelé au changement des hp du joueur
    /// </summary>
    /// <param name="newVal"> nouvelle valeur des pv du joueur </param>
    public delegate void OnVariableChangeDelegate(int newVal);
    public static event OnVariableChangeDelegate OnHpChange;

    /// <summary>
    /// Mana actuel du joueur
    /// </summary>
    public static int actualMana
    {
        get
        {
            return _actualMana;
        }
        set
        {
            _actualMana = value;
            if (_actualMana < 0) _actualMana = 0;
        } 
    }
    public static int _actualMana;
    /// <summary>
    /// Nom du joeur
    /// </summary>
    public static string actualName;

    /// <summary>
    /// UI affichant les pv du joueur
    /// </summary>
    public TextMeshProUGUI PlayerHp;
    /// <summary>
    /// UI affichant la mana du joueur
    /// </summary>
    public TextMeshProUGUI PlayerMana;

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        actualPlayer.Hp = actualHp;
        displayImage.sprite = actualSprite;
        PlayerHp.text = $"{actualHp}";
        PlayerMana.text = $"{actualMana}";
    }


    /// <summary>
    /// Fonction rafraichisant les données du joueur a afficher. Utiliser en cas de changement de joueur.
    /// </summary>
    void Refresh()
    {
        actualSprite = actualPlayer.Sprite;
        actualHp = actualPlayer.Hp;
        actualMana = actualPlayer.Mana;
        actualName = actualPlayer.Name;
        OnHpChange += PlayerDisp_OnHpChange;
    }

    public void PlayerDisp_OnHpChange(int newVal)
    {
        if (hpvaria > 0) CardReader.ActionDescription = $"{actualName} received {hpvaria} damage";
        else CardReader.ActionDescription = $"{actualName} healed for {hpvaria * -1} Hp";
    }
}
