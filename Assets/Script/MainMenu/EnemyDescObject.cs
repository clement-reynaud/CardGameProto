using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDescObject : MonoBehaviour
{

    public Image EneymyDescDisp;
    public Sprite Sprite;

    private void Update()
    {
        EneymyDescDisp.sprite = Sprite;
    }
}
