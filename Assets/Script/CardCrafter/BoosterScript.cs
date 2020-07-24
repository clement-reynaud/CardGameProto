using Assets.Script;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoosterScript : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Booster booster;

    public GameObject cardCrafted;

    private StatsCard[] allCard;
    private List<StatsCard> lootTable = new List<StatsCard>();


    private void Start()
    {
        allCard = Resources.LoadAll<StatsCard>("Scriptable Object/Card");
    }

    public void LootButton() 
    {
        lootTable.Clear();
        foreach(StatsCard card in booster.commonCards)
        {
            for (int i = 0; i < booster.commonWeight; i++)
            {
                lootTable.Add(card);
            }
        }
        foreach (StatsCard card in booster.uncommonCards)
        {
            for (int i = 0; i < booster.uncommonWeight; i++)
            {
                lootTable.Add(card);
            }
        }
        foreach (StatsCard card in booster.rareCards)
        {
            for (int i = 0; i < booster.rareWeight; i++)
            {
                lootTable.Add(card);
            }
        }
        foreach (StatsCard card in booster.mythicCards)
        {
            for (int i = 0; i < booster.mythicWeight; i++)
            {
                lootTable.Add(card);
            }
        }
        foreach (StatsCard card in booster.legendaryCard)
        {
            for (int i = 0; i < booster.legendaryWeight; i++)
            {
                lootTable.Add(card);
            }
        }
        foreach (StatsCard card in booster.uniqueCard)
        {
            for (int i = 0; i < booster.uncommonWeight; i++)
            {
                lootTable.Add(card);
            }
        }
        Debug.Log(lootTable.Count);
        Loot(5);
    }

    /*private void Loot()
    {
        instantiateInCircle();
        int drwnb = Data.rng.Next(lootTable.Count);
        Text.text = lootTable[drwnb].Name;
    }*/

    public void Loot(int howMany)
    {
        float angle = 360f / howMany;

        for (int i = 0; i < howMany; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            
            obj.transform.localPosition = new Vector3(0, 160);
            obj.transform.RotateAround(Vector3.zero, transform.position, angle * i);

            GameObject newcard = Instantiate(cardCrafted,transform.position,new Quaternion(0,0,0,0),transform);
            int drwnb = Data.rng.Next(lootTable.Count);
            newcard.GetComponent<CardCrafted>().card = lootTable[drwnb];
            newcard.transform.DOMove(obj.transform.position, 0.5f);
            Destroy(obj);
        }
    }
}
