using Assets.Script;
using Assets.Script.Enums;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisp : MonoBehaviour
{

    /// <summary>
    /// UI affichant les pv du joueur
    /// </summary>
    public TextMeshProUGUI PlayerHp;
    /// <summary>
    /// UI affichant la mana du joueur
    /// </summary>
    public TextMeshProUGUI PlayerMana;

    /// <summary>
    /// Image qui affichera le Sprite du joueur
    /// </summary>
    public Image displayImage;

    // Start is called before the first frame update
    void Start()
    {
        Data.Player.OnHpChange += Data.Player.PlayerDisp_OnHpChange;
    }

    // Update is called once per frame
    void Update()
    {
        displayImage.sprite = Data.Player.Sprite;
        PlayerHp.text = $"{Data.Player.Hp}";
        PlayerMana.text = $"{Data.Player.Mana}";
        if (Data.Player.Hp <= 0)
        {
            Data.gameState = States.Paused;
            Data.GameOverTitle = "You have been Slain:";
        }
    }

    public void HealPlayer()
    {
        Data.Player.Mana += 10;
        Data.Player.Hp += 10;
    }
}
