using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardCrafterManager : MonoBehaviour
{
    public BoosterScript LootButton;
    public Transform LootButtonBG;

    private Booster[] allBooster;
    private List<Booster> activeBooster = new List<Booster>();
    int index = 0;

    private void Start()
    {
        allBooster = Resources.LoadAll<Booster>("Scriptable Object/Booster");
        foreach (Booster item in allBooster)
        {
            if (item.isActivated)
            {
                activeBooster.Add(item);
            }
        }
        BoosterScript.booster = activeBooster[index];
        Debug.Log(BoosterScript.booster.name);
    }

    public void LeftArrow()
    {
        StopAllCoroutines();
        StartCoroutine(Swipe("left"));

        if (index > 0)
        {
            index--;
        }
        else
        {
            index = activeBooster.Count - 1;
        }
        BoosterScript.booster = activeBooster[index];
        BoosterScript.ClearLootingProcess();

        //LootButton.BroadcastMessage("RefreshDesc");
    }

    public void RightArrow()
    {
        StopAllCoroutines();
        StartCoroutine(Swipe("right"));

        if (index < activeBooster.Count - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        BoosterScript.booster = activeBooster[index];
        BoosterScript.ClearLootingProcess();
        //LootButton.BroadcastMessage("RefreshDesc");
    }

    private IEnumerator Swipe(string direction)
    {
        if (direction == "right") 
        { 
            LootButtonBG.DOLocalMoveX((Screen.width/2) + 200, 0.1f);

            yield return new WaitForSeconds(.1f);

            LootButtonBG.localPosition = new Vector3(-(Screen.width/2) - 200, 0);

            yield return new WaitForSeconds(.1f);

            LootButtonBG.DOLocalMoveX(0, 0.1f); ;
        }
        else if(direction == "left")
        {
            LootButtonBG.DOLocalMoveX(-(Screen.width / 2) - 200, 0.1f);

            yield return new WaitForSeconds(.1f);

            LootButtonBG.localPosition = new Vector3((Screen.width / 2) + 200, 0);

            yield return new WaitForSeconds(.1f);

            LootButtonBG.DOLocalMoveX(0, 0.1f); ;
        }
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("Menu");
    }


    public void DeckButton()
    {
        SceneManager.LoadScene("DeckBuilder");
    }
}
