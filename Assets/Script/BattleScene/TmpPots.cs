using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TmpPots : MonoBehaviour
{
    public TextMeshProUGUI manaPotsCount;
    public TextMeshProUGUI healthPotsCount;
    public Button hppots;
    public Button manapots;

    private void Update()
    {
        healthPotsCount.text = $"x{GameData.Player.HPPots}";
        manaPotsCount.text = $"x{GameData.Player.ManaPots}";
        if (GameData.Player.HPPots == 0) hppots.interactable = false;
        if (GameData.Player.ManaPots == 0) manapots.interactable = false;
    }

    public void HealthConso()
    {
        GameData.Player.HPPots--;
        GameData.Player.hp += 10;
    }

    public void ManaConso()
    {
        GameData.Player.ManaPots--;
        GameData.Player.mana += 10;
    }
}
