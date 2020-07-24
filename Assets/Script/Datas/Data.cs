using Assets.Script.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Script
{
    static class Data
    {
        /// <summary>
        /// Random number generator to use in the whole project
        /// </summary>
        public static System.Random rng = new System.Random();

        //Variables de jeu

        public static PlayerScript Player;

        /// <summary>
        /// Etat actuel du jeu
        /// </summary>
        public static States gameState = States.PlayerTurn;

        public static string GameOverTitle = "";

        public static bool PlayerExist { get; private set; }

        public static string savefilePath = Path.Combine(Path.GetFullPath("./"), "Save/player.json");

        /// <summary>
        /// Passe le tour du joueur vers l'ennemi et vice versa
        /// </summary>
        public static void PassTurn()
        {
            if (gameState == States.PlayerTurn) gameState = States.EnemyTurn;
            else gameState = States.PlayerTurn;
        }

        #region Deck and Cards

        /// <summary>
        /// Used to shuffle a List
        /// </summary>
        /// <typeparam name="T">Element of the list</typeparam>
        /// <param name="list">List to shuffle</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string CardDetailsToString(StatsCard card)
        {
            return $"<b><u>{card.Name} :</b></u>\n\n<color=blue><i>{card.Cout} Mana</i></color>\n\n{card.Description}";
        }

        public static List<StatsCard> PlayerStringDeckToCardDeck()
        {
            List<StatsCard> cardDeck = new List<StatsCard>();
            StatsCard[] allCard = Resources.LoadAll<StatsCard>("Scriptable Object/Card");

            foreach (string stringCard in Player.Deck)
            {
                foreach (StatsCard SOcard in allCard)
                {
                    if (stringCard == SOcard.Name) cardDeck.Add(SOcard);
                }
            }

            return cardDeck;
        }

        public static List<StatsCard> StringDeckToCardDeck(List<string> StringDeck)
        {
            List<StatsCard> cardDeck = new List<StatsCard>();
            StatsCard[] allCard = Resources.LoadAll<StatsCard>("Scriptable Object/Card");

            foreach (string stringCard in StringDeck)
            {
                foreach (StatsCard SOcard in allCard)
                {
                    if (stringCard == SOcard.Name) cardDeck.Add(SOcard);
                }
            }

            return cardDeck;
        }

        public static StatsCard SingleStringToCard(string cardName)
        {
            StatsCard[] allCard = Resources.LoadAll<StatsCard>("Scriptable Object/Card");
            foreach (StatsCard SOcard in allCard)
            {
                if (cardName == SOcard.Name) return SOcard;
            }
            return null;
        }

        #endregion

        #region Files

        public static void StartupPlayerFunc()
        {
            if (File.Exists(savefilePath))
            {
                Player = JsonUtility.FromJson<PlayerScript>(File.ReadAllText(savefilePath));
            }
            else
            {
                PlayerScript tmp = new PlayerScript(20, 20, "basename");
                File.WriteAllText(savefilePath, JsonUtility.ToJson(tmp));
                Player = JsonUtility.FromJson<PlayerScript>(File.ReadAllText(savefilePath));

            }
            foreach (string item in Player.Deck)
            {
                if (item == "") Player.Deck.Remove(item);
            }
            foreach (string item in Player.CardInventory)
            {
                if (item == "") Player.CardInventory.Remove(item);
            }
            PlayerExist = true;
        }

        public static void SavePlayer()
        {
            if (PlayerExist)
            {
                File.WriteAllText(savefilePath, JsonUtility.ToJson(Player));
            }
        }

        #endregion

        #region Misc

        

        #endregion
    }
}
