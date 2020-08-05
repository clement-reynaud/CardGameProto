using Assets.Script;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterScript : MonoBehaviour
{
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI ErrorText;
    private string BoosterDescText;

    public Image LootButtonBG;
    public static Booster booster;

    public static bool x10;

    public GameObject cardCrafted;

    private StatsCard[] allCard;
    private static List<StatsCard> lootTable = new List<StatsCard>();
    private static List<GameObject> toDestroy = new List<GameObject>();


    private void Start()
    {
        allCard = Resources.LoadAll<StatsCard>("Scriptable Object/Card");
        DescText.text = $"<u>{booster.Name}</u> : {booster.OpeningPrice} Souls\n";
    }

    private void Update()
    {
        DescText.text = $"<u>{booster.Name}</u> : {booster.OpeningPrice} Souls\n";
        if (x10)
        { 
            LootButtonBG.color = Color.yellow;
        }
        else
        {
            LootButtonBG.color = Color.white;
        }
    }

    public void RefreshDesc()
    {
        ErrorText.text = "";
    }

    public void LootButton() 
    {
        ClearLootingProcess();
        if (GameData.Player.Souls >= booster.OpeningPrice && !x10 || GameData.Player.Souls >= (booster.OpeningPrice * 10) - booster.OpeningPrice && x10) 
        { 
            foreach(StatsCard card in booster.commonCards)
            {
                for (int i = 0; i < booster.commonWeight; i++)
                {
                    card.rarity = Rarity.COMMON;
                    lootTable.Add(card);
                }
            }
            foreach (StatsCard card in booster.uncommonCards)
            {
                for (int i = 0; i < booster.uncommonWeight; i++)
                {
                    card.rarity = Rarity.UNCOMMON;
                    lootTable.Add(card);
                }
            }
            foreach (StatsCard card in booster.rareCards)
            {
                for (int i = 0; i < booster.rareWeight; i++)
                {
                    card.rarity = Rarity.RARE;
                    lootTable.Add(card);
                }
            }
            foreach (StatsCard card in booster.mythicCards)
            {
                for (int i = 0; i < booster.mythicWeight; i++)
                {
                    card.rarity = Rarity.MYTHIC;
                    lootTable.Add(card);
                }
            }
            foreach (StatsCard card in booster.legendaryCard)
            {
                for (int i = 0; i < booster.legendaryWeight; i++)
                {
                    card.rarity = Rarity.LEGENDARY;
                    lootTable.Add(card);
                }
            }
            foreach (StatsCard card in booster.uniqueCard)
            {
                for (int i = 0; i < booster.uncommonWeight; i++)
                {
                    card.rarity = Rarity.UNIQUE;
                    lootTable.Add(card);
                }
            }
            Loot(booster.cardDrawed);
            GameData.SavePlayer();
        }
        else
        {
            ErrorText.text = $"<color=red>Not enough Souls to open this Booster.</color>";
        }
    }

    public void Loot(int howMany)
    {
        float angle = 360f / howMany;

        if (!x10) 
        { 
            for (int i = 0; i < howMany; i++)
            {
                GameObject obj = new GameObject();
                obj.transform.SetParent(transform);
            
                obj.transform.localPosition = new Vector3(0, 160);
                obj.transform.RotateAround(Vector3.zero, transform.position, angle * i);

                GameObject newcard = Instantiate(cardCrafted,transform.position,new Quaternion(0,0,0,0),transform.parent);
                toDestroy.Add(newcard);

                int drwnb = GameData.rng.Next(lootTable.Count);
                newcard.GetComponent<CardCrafted>().card = lootTable[drwnb];

                GameData.Player.CardInventory.Add(lootTable[drwnb].Name);
                newcard.transform.DOMove(obj.transform.position, 0.5f);

                OutlineByRarity(newcard);

                Destroy(obj);
            }
            GameData.Player.Souls -= booster.OpeningPrice;
        }
        /* CEST VRAIMENT DE LA MERDE LA
        else
        {
            howMany *= 10;
            for (int i = 0; i < howMany; i++)
            {
                GameObject obj = new GameObject();
                obj.transform.SetParent(transform);

                obj.transform.localPosition = new Vector3(0, 160);
                obj.transform.RotateAround(Vector3.zero, transform.position, angle * i);

                GameObject newcard = Instantiate(cardCrafted, transform.position, new Quaternion(0, 0, 0, 0), transform.parent);
                toDestroy.Add(newcard);
                int drwnb = GameData.rng.Next(lootTable.Count);
                newcard.GetComponent<CardCrafted>().card = lootTable[drwnb];
                GameData.Player.CardInventory.Add(lootTable[drwnb].Name);
                newcard.transform.DOMove(obj.transform.position, 0.5f);
                OutlineByRarity(newcard);
                Destroy(obj);                
            }
            GameData.Player.Souls -= (booster.OpeningPrice * 10) - booster.OpeningPrice;
        }*/
    }

    private void OutlineByRarity(GameObject newcard)
    {
        Image newcardImage = newcard.GetComponent<Image>();
        switch (newcard.GetComponent<CardCrafted>().card.rarity)
        {
            case Rarity.NONE:
                break;
            case Rarity.COMMON:
                newcardImage.color = Color.gray;
                break;
            case Rarity.UNCOMMON:
                newcardImage.color = Color.green;
                break;
            case Rarity.RARE:
                newcardImage.color = Color.blue;
                break;
            case Rarity.MYTHIC:
                newcardImage.color = Color.magenta;
                break;
            case Rarity.LEGENDARY:
                newcardImage.color = Color.yellow;
                break;
            case Rarity.UNIQUE:
                newcardImage.color = Color.red;
                break;
            default:
                break;
        }
        newcardImage.color = new Color(newcardImage.color.r, newcardImage.color.g, newcardImage.color.b, 0);
    }

    public static void ClearLootingProcess()
    {
        foreach (GameObject item in toDestroy)
        {
            Destroy(item);
        }
        foreach (StatsCard item in lootTable)
        {
            item.rarity = Rarity.NONE;
        }
        lootTable.Clear();
    }

    public void x10button() 
    {
        x10 = true;
    }

    public void x1button() 
    {
        x10 = false;
    }

    public void test()
    {
        GameData.Player.Souls += 10;
    }
}
