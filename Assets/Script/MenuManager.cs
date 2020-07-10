using Assets.Script;
using Assets.Script.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void StartButtonClick()
    {
        Data.gameState = States.PlayerTurn;
        SceneManager.LoadScene("BattleScene");
    }

}
