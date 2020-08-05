using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldsDipslayScript : MonoBehaviour
{
    public TextMeshProUGUI GoldsNumber;

    // Update is called once per frame
    void Update()
    {
        GoldsNumber.text = $"{GameData.Player.Golds}";
    }
}
