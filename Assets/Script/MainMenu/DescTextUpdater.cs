using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI LevelTitle;
    public TextMeshProUGUI Reward;

    // Update is called once per frame
    void Update()
    {
        LevelTitle.text = LevelSelectObject.LevelTitle;
        Reward.text = LevelSelectObject.SoulsReward;
    }
}
