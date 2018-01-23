﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour {

    public static int damageType=0;
    public static int supportType=0;
    public static int objectType=0;
    public static int trapType=0;
    public static int coin=18;
    public static int countTrap=0;

    private bool sellSucess;
    
    //----------------------------------------------------------------------//
    //inventory
    public GameObject panel;
    public GameObject slotPrefab;

    private GameObject slotPanel;
    private List<GameObject> slot = new List<GameObject>();
    private int slotCount = 0;

    private void Start()
    {
        slotPanel = panel.transform.Find("content").gameObject;
    }

    private void updateUnit()
    {
        //slot.Add(Instantiate(slotPrefab.gameObject));
        //slot[slotCount].transform.SetParent(slotPanel.transform);
        Sprite img;
        if (GameManager.mainTypeUnit[slotCount] == 0)
        {
            slot.Add(Instantiate(slotPrefab.gameObject));
            img = Resources.Load<Sprite>(unit_database.units.Attacker[GameManager.subTypeUnit[slotCount]].SpritePath_img);
            slot[slotCount].GetComponent<Image>().sprite = img;
            slot[slotCount].transform.SetParent(slotPanel.transform);
        }
        else if (GameManager.mainTypeUnit[slotCount] == 1)
        {
            slot.Add(Instantiate(slotPrefab.gameObject));
            img = Resources.Load<Sprite>(unit_database.units.Supporter[GameManager.subTypeUnit[slotCount]].SpritePath_img);
            slot[slotCount].GetComponent<Image>().sprite = img;
            slot[slotCount].transform.SetParent(slotPanel.transform);
        }
        else if (GameManager.mainTypeUnit[slotCount] == 2)
        {
            slot.Add(Instantiate(slotPrefab.gameObject));
            img = Resources.Load<Sprite>(unit_database.units.Sturture[GameManager.subTypeUnit[slotCount]].SpritePath_img);
            slot[slotCount].GetComponent<Image>().sprite = img;
            slot[slotCount].transform.SetParent(slotPanel.transform);
        }
        else if (GameManager.mainTypeUnit[slotCount] == 3)
        {
            slot.Add(Instantiate(slotPrefab.gameObject));
            img = Resources.Load<Sprite>(unit_database.units.Trap[GameManager.subTypeUnit[slotCount]].SpritePath_img);
            slot[slotCount].GetComponent<Image>().sprite = img;
            slot[slotCount].transform.SetParent(slotPanel.transform);
        }
        slotCount++;
    }



    //--------------------------------------------------------
    //update value form button
    public void SetCoinOnClick(int sellcoin)
    {
        //use in this screen
        if(coin - sellcoin >= 0)
        {
            coin -= sellcoin;
            sellSucess = true;
        }
        else
        {
            sellSucess = false;
        }
    }

    public void SetTrapOnclick()
    {
        if(countTrap + 1 <= 5)
        {
            countTrap++;
            sellSucess = true;
        }
        else
        {
            sellSucess = false;
        }
    }

    public void SetTypeOnClick(int Type)
    {
        if (sellSucess)
        {
            //use in this screen
            if (Type == 0)
            {
                damageType++;
            }
            else if (Type == 1)
            {
                supportType++;
            }
            else if (Type == 2)
            {
                objectType++;
            }
            else if (Type == 3)
            {
                trapType++;
            }

            //send to map screen as referent index unit_database
            GameManager.mainTypeUnit.Add(Type);//contain index [0-3] only!!!
        }
    }

    public void SetIndexInType(int subType)
    {
        if (sellSucess)
        {
            //send to map screen as referent index each aerry in unit_database
            GameManager.subTypeUnit.Add(subType);
            updateUnit();
        }
    }
}
