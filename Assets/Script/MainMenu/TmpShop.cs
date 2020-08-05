using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TmpShop : MonoBehaviour
{
    public TextMeshProUGUI healthPotionCount;
    public TextMeshProUGUI manaPotionCount;
    public TextMeshProUGUI ErrorMessage;

    private void Update()
    {
        healthPotionCount.text = $"x{GameData.Player.HPPots}";
        manaPotionCount.text = $"x{GameData.Player.ManaPots}";
    }

    public void HealthClick()
    {
        if(GameData.Player.Golds >= 5)
        {
            GameData.Player.HPPots++;
            GameData.Player.Golds -= 5;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(NotEnoughGoldMessage());
        }
    }

    public void ManaClick()
    {
        if (GameData.Player.Golds >= 5)
        {
            GameData.Player.ManaPots++;
            GameData.Player.Golds -= 5;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(NotEnoughGoldMessage());
        }
    }

    IEnumerator NotEnoughGoldMessage()
    {
        ErrorMessage.text = "Not Enough Gold";

        yield return new WaitForSeconds(2f);

        ErrorMessage.text = "";

    }
}
