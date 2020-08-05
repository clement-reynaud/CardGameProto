using Assets.Script;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CardReader : MonoBehaviour
{

    private int flexVariable1E;
    private int flexVariable2E;
    private int flexVariable3E;
    private int flexVariable4E;

    /// <summary>
    /// Fonction lisant le nom de l'ennemi actuel 
    /// effectuant l'une des actions que l'ennemi peut effectuer
    /// </summary>
    void EnemyAction()
    {
        LogText.color = Color.black;
        int selector;
        enemyDisp.transform.DOPath(new Vector3[2] { new Vector3(0, 1, 0), new Vector3(0, 2, 0) }, 0.2f);
        switch (actualEnemy.actualName)
        {
            //Spiders
            case "Baby Spider":
                selector = GameData.rng.Next(0, 100) + 1;
                if (selector < 90)
                {
                    ActionName = "Basic Attack";
                    GameData.Player.Hp -= GameData.rng.Next(2, 4);
                    Shake(0);
                }
                else
                {
                    ActionName = "Web Spit";
                    GameData.Player.Mana -= 5;
                    Shake(0);
                }
                break;

            case "Spider":
                selector = GameData.rng.Next(0, 100) + 1;
                if (selector <= 70)
                {
                    ActionName = "Basic Attack";
                    GameData.Player.Hp -= GameData.rng.Next(2, 4);
                    Shake(0);
                }
                else if (selector > 70 && selector < 90)
                {
                    ActionName = "Web Spit";
                    ActionDescription = $"{actualEnemy.actualName} stole 5 mana";
                    GameData.Player.Mana -= 5;
                    Shake(0);
                }
                else
                {
                    ActionName = "Webby Bite";
                    GameData.Player.Hp -= GameData.rng.Next(3, 6);
                    Shake(0);
                }
                break;

            case "Queen Spider":
                selector = GameData.rng.Next(0, 100) + 1;
                if (selector <= 70)
                {
                    ActionName = "Basic Attack";
                    GameData.Player.Hp -= GameData.rng.Next(2, 4);
                    Shake(0);
                }
                else if (selector > 70 && selector <= 90)
                {
                    ActionName = "Web Spit";
                    ActionDescription = $"{actualEnemy.actualName} stole 5 mana";
                    GameData.Player.Mana -= 5;
                    Shake(0);
                }
                else
                {
                    ActionName = "Queen Bite";
                    ActionDescriptionBuffer = "and 5 mana was stolen";
                    GameData.Player.Hp -= GameData.Player.Hp / 4;
                    GameData.Player.Mana -= 5;
                    Shake(0);
                }
                break;




            //Golem
            case "Small Stone Golem":
                selector = GameData.rng.Next(0, 100) + 1;
                if (selector < 90)
                {
                    ActionName = "Basic Attack";
                    GameData.Player.Hp -= 4;
                    Shake(0);
                }
                else
                {
                    ActionName = "Rebuilding";
                    actualEnemy.actualHp += 10;
                }
                break;


            case "Stone Golem":
                selector = GameData.rng.Next(0, 100) + 1;
                if (selector <= 70)
                {
                    ActionName = "Basic Attack";
                    GameData.Player.Hp -= GameData.rng.Next(4, 6);
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


            //Slime
            case "Water Slime":
                ActionName = "Basic Attack";
                GameData.Player.Hp -= 4;
                Shake(0);
                break;


            //Fish Folks
            case "Naga":
                selector = GameData.rng.Next(0, 100) + 1;
                if (selector <= 70)
                {
                    ActionName = "Basic Attack";
                    GameData.Player.Hp -= GameData.rng.Next(4, 6);
                    Shake(0);
                }
                else if (selector > 80)
                {
                    ActionName = "Bloody Water";
                    GameData.Player.Hp -= 6;
                    actualEnemy.actualHp += 6;
                    Shake(0);
                }
                break;

            case "Warior Naga":
                //Warrior Naga uses FlexVariable1E comme boost de dmg
                selector = GameData.rng.Next(0, 100) + 1;
                if (selector <= 70)
                {
                    ActionName = "Basic Attack";
                    GameData.Player.Hp -= GameData.rng.Next(5, 8) + (flexVariable1E * 2);
                    Shake(0);
                }
                else if(selector > 80 || selector <= 95)
                {
                    ActionName = "Bloody Water";
                    GameData.Player.Hp -= 8 + (flexVariable1E * 2);
                    actualEnemy.actualHp += 8;
                    Shake(0);
                }
                else
                {
                    ActionName = "Blessing of the Sea";
                    ActionDescription = $"{actualEnemy.actualName} prayed to the sea\n his next attack will deal more damage";
                    flexVariable1E++;
                }
                break;
            default:
                break;
        }
        GameData.PassTurn();
        UpdateUI(1);
    }
}
