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
        GameData.Player.OnHpChange += GameData.Player.PlayerDisp_OnHpChange;
    }

    // Update is called once per frame
    void Update()
    {
        displayImage.sprite = GameData.Player.Sprite;
        PlayerHp.text = $"{GameData.Player.Hp}";
        PlayerMana.text = $"{GameData.Player.Mana}";
        if (GameData.Player.Hp <= 0)
        {
            GameData.gameState = States.BattleEnded;
            CardReader.playerDead = true;
            GameData.GameOverTitle = "You have been Slain:";
        }
    }

    public void HealPlayer()
    {
        GameData.Player.Mana += 10;
        GameData.Player.Hp += 10;
    }
}
