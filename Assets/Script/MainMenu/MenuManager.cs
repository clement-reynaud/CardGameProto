using Assets.Script;
using Assets.Script.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        GameData.StartupPlayerFunc();
    }
    public void StartButtonClick()
    {
        transform.Find("LevelSelectPanel").gameObject.SetActive(true);
    }
    
    public void ClickOutside()
    {
        transform.Find("LevelSelectPanel").gameObject.SetActive(false);
    }

    public void DeckBuilderClick()
    {
        SceneManager.LoadScene("DeckBuilder");
    }

    public void CardCrafterClick()
    {
        SceneManager.LoadScene("CardCrafter");
    }

}
