using Assets.Script;
using Assets.Script.Enums;
using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

class CardScript : MonoBehaviour
{
    /// <summary>
    /// Données de la cartes
    /// </summary>
    public StatsCard actualCard;

    /// <summary>
    /// GameObject du "CardReader"
    /// </summary>
    private GameObject CardReader;

    /// <summary>
    /// Définit si l'utilisateur peut posser la carte sur le "CardReader"
    /// </summary>
    private bool canLand;

    /// <summary>
    /// Dernière position de la carte avant que l'utilisateur ne la déplace
    /// </summary>
    private Vector3 lastPos;

    public TextMeshProUGUI DescText;

    private void Start()
    {

    }


    void OnMouseEnter()
    {
        if (Data.gameState != States.Paused)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            DescText.text = Data.CardToString(actualCard);
        }
    }

    void OnMouseExit()
    {
        if (Data.gameState != States.Paused)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            DescText.text = "";
        }
    }

    void OnMouseDown()
    {
        lastPos = transform.position;
    }

    void OnMouseUp()
    {
        //IF Statements qui permet d'utiliser la carte (broadcast "Action" cf CardReader.cs)
        //ou de replacer la carte a sa lastPos
        if (canLand && PlayerDisp.actualMana >= actualCard.Cout && Data.gameState == States.PlayerTurn)
        {
            CardReader.BroadcastMessage("ActionCard", actualCard);
            Destroy(gameObject);
        }
        else if(canLand && PlayerDisp.actualMana < actualCard.Cout || Data.gameState != States.PlayerTurn)
        {
            transform.position = lastPos;
        }
        DescText.text = "";
    }

    private void OnMouseDrag()
    {


        //Permet de déplacer la carte en la glissant si c'est au tour du joueur
        if (Data.gameState != States.Paused) 
        {
            //Permet de déplacer la carte en la glissant
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.x = Mathf.Clamp(point.x, -3.5f, 3.5f);
            point.y = Mathf.Clamp(point.y, -4f, -0.5f);
            point.z = transform.position.z;
            transform.position = point;
        }
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = actualCard.Sprite;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CardHub")
        {
            canLand = true;
            CardReader = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "CardHub")
        {
            canLand = false;
            CardReader = null;
        }
    }

}