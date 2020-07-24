using Assets.Script;
using Assets.Script.Enums;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class CardReader : MonoBehaviour
{
    /// <summary>
    /// Objet "EnemyDisp" de la scène
    /// </summary>
    public GameObject enemyDisp;

    /// <summary>
    /// Objet "PlayerDisp" de la scène
    /// </summary>
    public GameObject playerDisp;

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
    private PlayerScript ps = Data.Player;

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

    private void Update()
    {
        actualEnemy = enemyDisp.GetComponent<EnemyDisp>();
        //actualPlayer = playerDisp.GetComponent<PlayerScript>();
        actualDeck = Deck.GetComponent<DeckScript>();

        EndBattleMenuTitle.text = Data.GameOverTitle;
        if (Data.gameState == States.Paused) EndBattleMenu.SetActive(true);
        else EndBattleMenu.SetActive(false);
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
        if (Data.gameState == States.EnemyTurn) Invoke("EnemyAction",UnityEngine.Random.Range(1.5f,3.5f));
    }

    void ActionPick()
    {
        ActionDescription = "";
        ActionDescriptionBuffer = "";
        UpdateUI(2);
        Data.PassTurn();
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
                LogText.text = $"<color=blue>{Data.Player.Name}</color>\n uses \n<color=white>{ActionName}</color>\n\n{ActionDescription} {ActionDescriptionBuffer}";
                break;
            case 1:
                LogText.text = $"<color=red>{actualEnemy.actualName}</color>\n uses \n<color=white>{ActionName}</color>\n\n{ActionDescription} {ActionDescriptionBuffer}";
                break;
            case 2:
                LogText.text = $"{Data.Player.Name}\n picks a card";
                break;
            case 3:
                LogText.text = "";
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Fonction lisant le nom de la carte joué, effectuant l'action qui lui est assigné
    /// </summary>
    /// <param name="card"> Carte joué par le joueur </param>
    void ReadCard(StatsCard card)
    {
        ActionName = card.Name;
        switch (card.Name)
        {
        //Fire Cards
            case "Fire I":
                DamageEnemy(actualEnemy,5,Elements.Fire,card.Cout);
                break;

        //Water Cards
            case "Water I":
                DamageEnemy(actualEnemy, 5, Elements.Water,card.Cout);
                break;

        //Thunder Cards
            case "Thunder I":
                DamageEnemy(actualEnemy, 5, Elements.Thunder,card.Cout);
                break;

        //Wind Cards
            case "Wind I":
                DamageEnemy(actualEnemy, 5, Elements.Wind,card.Cout);
                break;

        //Shadow Cards
            case "Blood Sacrifice I":
                DamageEnemy(actualEnemy, 5, Elements.Shadow, -5/*Ajoute 5 de mana*/);
                break;

        //Light Cards
            //Pick Cards
            case "Rewind I":
                UseCard(card.Cout);
                actualDeck.TakeCard(2);
                break;

           //Heal Cards
           case "Heal I":
               UseCard(card.Cout);
               Data.Player.Hp += 5;
               break;


            default:
                break;
        }
        UpdateUI(0);
        if(actualEnemy.actualHp > 0) Data.PassTurn(); 
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
        Data.Player.Mana -= cout;
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
        Data.Player.Mana -= cout;
        Shake(1);
    }

    /// <summary>
    /// Foncion permetant d'utilisé une carte et d'utiliser son mana (utile pour la lecture)
    /// </summary>
    /// <param name="cout">Cout de la carte en Mana</param>
    void UseCard(int cout)
    {
        Data.Player.Mana -= cout;
    }

    /// <summary>
    /// Fonction lisant le nom de l'ennemi actuel 
    /// effectuant l'une des actions que l'ennemi peut effectuer
    /// </summary>
    void EnemyAction()
    {
        LogText.color = Color.black;
        int selector;
        enemyDisp.transform.DOPath(new Vector3[2] {new Vector3(0, 1, 0), new Vector3(0,2,0)}, 0.2f);
        switch (actualEnemy.actualName)
        {
            //Spiders
            case "Baby Spider":
                selector = Data.rng.Next(0, 100) + 1;
                if (selector < 90)
                {
                    ActionName = "Basic Attack";
                    Data.Player.Hp -= Data.rng.Next(2,4);
                    Shake(0);
                }
                else 
                {
                    ActionName = "Web Spit";
                    Data.Player.Mana -= 5;
                    Shake(0);
                }
                break;

            case "Spider":
                selector = Data.rng.Next(0, 100) + 1;
                if (selector <= 70)
                {
                    ActionName = "Basic Attack";
                    Data.Player.Hp -= Data.rng.Next(2, 4);
                    Shake(0);
                }
                else if(selector > 70 && selector < 90)
                {
                    ActionName = "Web Spit";
                    ActionDescription = $"{actualEnemy.actualName} stole 5 mana";
                    Data.Player.Mana -= 5;
                    Shake(0);
                }
                else
                {
                    ActionName = "Webby Bite";
                    Data.Player.Hp -= Data.rng.Next(3, 6);
                    Shake(0);
                }
                break;
            case "Queen Spider":
                selector = Data.rng.Next(0, 100) + 1;
                if (selector <= 70)
                {
                    ActionName = "Basic Attack";
                    Data.Player.Hp -= Data.rng.Next(2, 4);
                    Shake(0);
                }
                else if (selector > 70 && selector <= 90)
                {
                    ActionName = "Web Spit";
                    ActionDescription = $"{actualEnemy.actualName} stole 5 mana";
                    Data.Player.Mana -= 5;
                    Shake(0);
                }
                else
                {
                    ActionName = "Queen Bite";
                    ActionDescriptionBuffer = "and 5 mana was stolen";
                    Data.Player.Hp -= Data.Player.Hp / 4;
                    Data.Player.Mana -= 5;
                    Shake(0);
                }
                break;

            //Golem
            case "Small Stone Golem":
                selector = Data.rng.Next(0, 100) + 1;
                if (selector < 90)
                {
                    ActionName = "Basic Attack";
                    Data.Player.Hp -= 4;
                    Shake(0);
                }
                else
                {
                    ActionName = "Rebuilding";
                    actualEnemy.actualHp += 10;
                }
                break;


            case "Stone Golem": 
                selector = Data.rng.Next(0, 100) + 1;
                if (selector <= 70)
                {
                    ActionName = "Basic Attack";
                    Data.Player.Hp -= Data.rng.Next(4,6);
                    Shake(0);
                }
                else if (selector > 70 && selector < 90)
                {
                    ActionName = "Rebuilding";
                    actualEnemy.actualHp += 10;
                }
                else
                {
                    ActionName = "Stunning Blow";
                    ActionDescription = $"{actualEnemy.actualName} destroyed one card in your hand";
                    actualDeck.DestroyCard(1);
                    Shake(0);
                }

                break;

            default:
                break;
        }
        Data.PassTurn();
        UpdateUI(1);
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

    public void ResetClick()
    {
        Data.SavePlayer();
        actualEnemy.Refresh();
        actualDeck.Refresh();
        UpdateUI(3);
        Data.gameState = States.PlayerTurn;   
    }

    public void MenuClick()
    {
        Data.SavePlayer();
        SceneManager.LoadScene("Menu"); // Charge le menu principal     
    }

    #endregion
}
