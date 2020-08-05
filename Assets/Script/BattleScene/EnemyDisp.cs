using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Script;
using Assets.Script.Enums;
using System;

public class EnemyDisp : MonoBehaviour
{
    /// <summary>
    /// Ennemis a afficher
    /// </summary>
    public StatsEnemy toDisp;


    /// <summary>
    /// Sprite de l'ennemi
    /// </summary>
    public Sprite actualSprite;
    /// <summary>
    /// Pv actuel de l'ennemi
    /// </summary>
    public int actualHp
    {
        get { return _actualHp; }
        set
        {
            if (_actualHp == value) return;
            hpvaria = _actualHp - value;
            _actualHp = value;
            OnHpChange?.Invoke(_actualHp);
        }
    }
    private int _actualHp;
    public int hpvaria;

    public delegate void OnVariableChangeDelegate(int newVal);
    public static event OnVariableChangeDelegate OnHpChange;

    /// <summary>
    /// Nom de l'ennemi
    /// </summary>
    public string actualName;
    /// <summary>
    /// Faiblesses de l'ennemi
    /// </summary>
    public List<Elements> actualWeakness;

    /// <summary>
    /// UI affichant les pv de l'ennemi
    /// </summary>
    public TextMeshProUGUI EnemyPv;
    /// <summary>
    /// UI affichant le nom de l'ennemi
    /// </summary>
    public TextMeshProUGUI EnemyName;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
    }

    // Start is called before the first frame update
    void Start()
    {
        PickFromNormalEncounterTable();
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = actualSprite;
        EnemyPv.text = $"{actualHp}";
        EnemyName.text = $"{actualName}";
        
        if (actualHp <= 0)
        {
            EnemyKilled();    
        }
    }

    private void EnemyKilled()
    {
        if (GameData.gameState != States.BattleEnded)
        {
            EncounterManager.acumulatedGoldsReward += GameData.rng.Next(toDisp.minGoldReward, toDisp.maxGoldReward);
            if (GameData.EncounterTableAdvancement > GameData.ActualEncounterTable.NbOfNormalEncounter)
            {
                EncounterManager.acumulatedSoulsReward += GameData.ActualEncounterTable.SoulsReward;
            }
        }
        GameData.gameState = States.BattleEnded;
        CardReader.playerDead = false;
        GameData.GameOverTitle = "Enemy Slain:";
    }

    /// <summary>
    /// Fonction rafraichisant les données de l'ennemi a afficher. Utiliser en cas de changement d'ennemi.
    /// </summary>
    public void Refresh()
    {
        actualSprite = toDisp.Sprite;
        actualHp = toDisp.Hp;
        actualName = toDisp.Name;
        actualWeakness = toDisp.Weakness;
        OnHpChange += EnemyDisp_OnHpChange;
    }

    private void EnemyDisp_OnHpChange(int newVal)
    {
        if (hpvaria > 0) CardReader.ActionDescription = $"{actualName} received {hpvaria} damage";
        else CardReader.ActionDescription = $"{actualName} healed for {hpvaria * -1} Hp";
    }

    public void PickFromNormalEncounterTable()
    {
        int drwnb = GameData.rng.Next(GameData.ActualEncounterTable.NormalEncounterEnemy.Count);
        toDisp = GameData.ActualEncounterTable.NormalEncounterEnemy[drwnb];
    }

    public void PickFromBossEncounterTable()
    {
        int drwnb = GameData.rng.Next(GameData.ActualEncounterTable.BossEncounterEnemy.Count);
        toDisp = GameData.ActualEncounterTable.BossEncounterEnemy[drwnb];
    }
}
