using Assets.Script;
using Assets.Script.Enums;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class CardReader : MonoBehaviour
{
    /// <summary>
    /// Objet "EnemyDisp" de la scène
    /// </summary>
    public GameObject enemyDisp;

    /// <summary>
    /// Objet "Deck" de la scène
    /// </summary>
    public GameObject Deck;

    /// <summary>
    /// Script de "EnemyDisp" permetant d'accéder aux données de l'ennemi actuel
    /// </summary>
    private EnemyDisp actualEnemy;

    /// <summary>
    /// Script de "PlayerDisp" permetant d'accéder aux données du joueur actuel, non traité par les données statiques
    /// </summary>
    private PlayerScript ps = GameData.Player;

    /// <summary>
    /// Script du "Deck" permetant d'accéder aux données de ce dernier
    /// </summary>
    private DeckScript actualDeck;

    
    /// <summary>
    /// Nom de la dernière action effectué, utilisé par l'UI
    /// </summary>
    public static string ActionName;
    public static string ActionDescription;
    public static string ActionDescriptionBuffer;
    [Header("UI:")]
    /// <summary>
    /// Texte UI du Log
    /// </summary>
    public TextMeshProUGUI LogText;
    public GameObject EndBattleMenu;
    public TextMeshProUGUI EndBattleMenuTitle;

    public static bool playerDead;

    private void Start()
    {
        ResetFlexVariable();
    }

    private void Update()
    {
        actualEnemy = enemyDisp.GetComponent<EnemyDisp>();
        actualDeck = Deck.GetComponent<DeckScript>();

        EndBattleMenuFunc();
    }

    private void EndBattleMenuFunc()
    {
        EndBattleMenuTitle.text = GameData.GameOverTitle;
        if (GameData.gameState == States.BattleEnded)
        {
            EndBattleMenu.SetActive(true);
            if (playerDead || GameData.EncounterTableAdvancement == GameData.ActualEncounterTable.NbOfBossEncounter + GameData.ActualEncounterTable.NbOfNormalEncounter)
            {
                EndBattleMenu.transform.Find("NextButton").gameObject.SetActive(false);
            }
            else
            {
                EndBattleMenu.transform.Find("NextButton").gameObject.SetActive(true);
            }
        }
        else
        {
            EndBattleMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Fonction de début de tour du joueur, permetant de faire avancer l'état du jeu
    /// et de mettre a jour l'UI.
    /// </summary>
    /// <param name="card"> Carte joué par le joueur </param>
    void ActionCard(StatsCard card)
    {
        ActionDescription = ""; 
        ActionDescriptionBuffer = "";
        ReadCard(card);
        if (GameData.gameState == States.EnemyTurn) Invoke("EnemyAction",UnityEngine.Random.Range(1.5f,3.5f));
    }

    void ActionPick()
    {
        ActionDescription = "";
        ActionDescriptionBuffer = "";
        UpdateUI(2);
        GameData.PassTurn();
        Invoke("EnemyAction", UnityEngine.Random.Range(1.5f, 3.5f));
    }

    /// <summary>
    /// Met a jour l'UI
    /// </summary>
    /// <param name="action">0 = Joueur, 1 = Ennemi, 2 = Player pick a card, 3 = Reset</param>
    private void UpdateUI(int action)
    {
        switch (action)
        {
            case 0:
                LogText.text = $"<color=blue>{GameData.Player.Name}</color>\n uses \n<color=white>{ActionName}</color>\n\n{ActionDescription} {ActionDescriptionBuffer}";
                break;
            case 1:
                LogText.text = $"<color=red>{actualEnemy.actualName}</color>\n uses \n<color=white>{ActionName}</color>\n\n{ActionDescription} {ActionDescriptionBuffer}";
                break;
            case 2:
                LogText.text = $"{GameData.Player.Name}\n picks a card";
                break;
            case 3:
                LogText.text = "";
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Fonction de base pour blesser un ennemi
    /// </summary>
    /// <param name="cible">Ennemi a blesser</param>
    /// <param name="nb">Degats a infliger</param>
    /// <param name="element">Element de l'attaque</param>
    /// <param name="cout">Cout de la carte en Mana</param>
    void DamageEnemy(EnemyDisp cible, int nb, Elements element, int cout)
    {
        if (cible.actualWeakness.Contains(element))
        {
            cible.actualHp -= nb * 2;
            ActionName += " (weak)";
            LogText.color = Color.yellow;
        }
        else
        {
            cible.actualHp -= nb;
        }
        GameData.Player.Mana -= cout;
        Shake(1);
    }

    /// <summary>
    /// Fonction de base pour blesser un ennemi
    /// </summary>
    /// <param name="cible">Ennemi a blesser</param>
    /// <param name="nb">Degats a infliger</param>
    /// <param name="cout">Cout de la carte en Mana</param>
    void DamageEnemy(EnemyDisp cible, int nb, int cout)
    {
        cible.actualHp -= nb;
        GameData.Player.Mana -= cout;
        Shake(1);
    }

    /// <summary>
    /// Foncion permetant d'utilisé une carte et d'utiliser son mana (utile pour la lecture)
    /// </summary>
    /// <param name="cout">Cout de la carte en Mana</param>
    void UseCard(int cout)
    {
        GameData.Player.Mana -= cout;
    }

    /// <summary>
    /// Fait trembler le transform du perso ou de l'ennemi
    /// </summary>
    /// <param name="target">0 = Joueur, 1 = Ennemi</param>
    private void Shake(int target)
    {
        if (target == 0)
        {
            CameraShake.shakeDuration = 0.2f;
        }
        else if (target == 1)
        {
            actualEnemy.GetComponent<ShakeTransformS>().Begin();
        }
    }

    #region EndBattleMenu

    public void NextClick()
    {
        GameData.EncounterTableAdvancement++;

        if(GameData.EncounterTableAdvancement > GameData.ActualEncounterTable.NbOfNormalEncounter)
        {
            actualEnemy.PickFromBossEncounterTable();
        }
        else
        {
            actualEnemy.PickFromNormalEncounterTable();
        }

        ResetFlexVariable();

        actualEnemy.Refresh();
        actualDeck.Refresh();
        UpdateUI(3);
        GameData.gameState = States.PlayerTurn;   
    }

    public void MenuClick()
    {
        GameData.EncounterTableAdvancement = 0;
        GameData.Player.Golds += EncounterManager.acumulatedGoldsReward;
        GameData.Player.Souls += EncounterManager.acumulatedSoulsReward;
        GameData.SavePlayer();
        SceneManager.LoadScene("Menu"); // Charge le menu principal     
    }

    #endregion

    private void ResetFlexVariable()
    {
        flexVariable1E = 0;
        flexVariable2E = 0;
        flexVariable3E = 0;
        flexVariable4E = 0;
    }
}