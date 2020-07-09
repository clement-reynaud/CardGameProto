using Assets.Script.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script
{
    static class Data
    {
        /// <summary>
        /// Random number generator to use in the whole project
        /// </summary>
        public static System.Random rng = new System.Random();

        //Variables de jeu

        /// <summary>
        /// Etat actuel du jeu
        /// </summary>
        public static States gameState = States.PlayerTurn;

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

        /// <summary>
        /// Passe le tour du joueur vers l'ennemi et vice versa
        /// </summary>
        public static void PassTurn()
        {
            if (gameState == States.PlayerTurn) gameState = States.EnemyTurn;
            else gameState = States.PlayerTurn;
        }

        public static string CardToString(StatsCard card)
        {
            return $"<b><u>{card.Name} :</b></u>\n\n<color=blue><i>{card.Cout} Mana</i></color>\n\n{card.Description}";
        }
    }
}
