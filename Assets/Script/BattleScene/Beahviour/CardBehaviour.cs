using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CardReader : MonoBehaviour
{


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
                DamageEnemy(actualEnemy, GameData.Player.Mag, Elements.Fire, card.Cout);
                break;

            //Water Cards
            case "Water I":
                DamageEnemy(actualEnemy, GameData.Player.Mag, Elements.Water, card.Cout);
                break;

            //Thunder Cards
            case "Thunder I":
                DamageEnemy(actualEnemy, GameData.Player.Mag, Elements.Thunder, card.Cout);
                break;

            //Wind Cards
            case "Wind I":
                DamageEnemy(actualEnemy, GameData.Player.Mag, Elements.Wind, card.Cout);
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
                GameData.Player.Hp += GameData.Player.Wis;
                break;


            default:
                break;
        }
        UpdateUI(0);
        if (actualEnemy.actualHp > 0) GameData.PassTurn();
    }
}
