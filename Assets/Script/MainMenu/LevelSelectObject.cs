using Assets.Script;
using Assets.Script.Enums;
using Coffee.UIEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectObject : MonoBehaviour
{
    public EncounterTable level;
    
    public static string LevelTitle;
    public static List<StatsEnemy> LevelDescEnemy;
    public static List<StatsEnemy> LevelDescBoss;
    public static string SoulsReward;
    public static bool RepopulateEnemy { get; internal set; }
    public static bool RepopulateBoss { get; internal set; }

    private bool doubleTouch = false;

    public void Start()
    {
        doubleTouch = false;
        GetComponentInChildren<UIGradient>().color1 = level.GradientColor1;
        GetComponentInChildren<UIGradient>().color2 = level.GradientColor2;
    }

    public void Click()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (!doubleTouch)
            {
                doubleTouch = true;
            }
            else StartBattle();
        }
        else
        {
            StartBattle();
        } 
    }

    public void StartBattle()
    {
        //EncounterManager.acumulatedGoldReward = 0;
        GameData.gameState = States.PlayerTurn;
        GameData.EncounterTableAdvancement = 1;
        GameData.ActualEncounterTable = level;
        if (GameData.Player.hp <= 0) GameData.Player.hp = GameData.Player.MaxHp;
        GameData.Player.Mana = GameData.Player.MaxMana;
        SceneManager.LoadScene("BattleScene");
    }

    public void HoverEnter()
    { 
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1);
        }
        LevelTitle = level.Name;
        LevelDescEnemy = level.NormalEncounterEnemy;
        LevelDescBoss = level.BossEncounterEnemy;
        SoulsReward = $"{level.SoulsReward}";
        RepopulateEnemy = true;
        RepopulateBoss = true;

    }

    public void HoverExit()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
