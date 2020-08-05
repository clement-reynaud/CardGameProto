using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulsDipslayScript : MonoBehaviour
{
    public TextMeshProUGUI SoulsNumber;

    // Update is called once per frame
    void Update()
    {
        SoulsNumber.text = $"{GameData.Player.Souls}";
    }
}
