﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour {

    //value send to screen
    public static int[] type = { 0, 0, 0, 0 };
    public static int coin=18;

    private bool sellSucess;
    
    //----------------------------------------------------------------------//
    //inventory system value
    public GameObject panel;
    public GameObject slotPrefab;

    private GameObject UnitSlotPanel;
    private GameObject TrapSlotPanel;
    private static List<GameObject> slot = new List<GameObject>();
    private int sold;

    private void Start()
    {
        UnitSlotPanel = panel.transform.Find("unit").gameObject;
        TrapSlotPanel = panel.transform.Find("trap").gameObject;
    }

    private void updateUnit()
    {
        print(GameManager.Instance.MainTypeUnit.Count-1);
        Sprite img;
        int index = GameManager.Instance.MainTypeUnit.Count - 1;
        if (GameManager.Instance.MainTypeUnit[index] == 0)
        {
            slot.Add(Instantiate(slotPrefab.gameObject));
            slot[index].transform.SetParent(UnitSlotPanel.transform);
            img = Resources.Load<Sprite>(unit_database.units.Attacker[GameManager.Instance.SubTypeUnit[index]].SpritePath_img);
            slot[index].GetComponent<Image>().sprite = img;

            //set value to object slot
            slot[index].GetComponent<slot2>().referentIndex = index;
            slot[index].GetComponent<slot2>().mainIndex = 0;
            slot[index].GetComponent<slot2>().UnitCoin = sold;
        }
        else if (GameManager.Instance.MainTypeUnit[index] == 1)
        {
            slot.Add(Instantiate(slotPrefab.gameObject));
            slot[index].transform.SetParent(UnitSlotPanel.transform);
            img = Resources.Load<Sprite>(unit_database.units.Supporter[GameManager.Instance.SubTypeUnit[index]].SpritePath_img);
            slot[index].GetComponent<Image>().sprite = img;

            //set value to object slot
            slot[index].GetComponent<slot2>().referentIndex = index;
            slot[index].GetComponent<slot2>().mainIndex = 1;
            slot[index].GetComponent<slot2>().UnitCoin = sold;
        }
        else if (GameManager.Instance.MainTypeUnit[index] == 2)
        {
            slot.Add(Instantiate(slotPrefab.gameObject));
            slot[index].transform.SetParent(UnitSlotPanel.transform);
            img = Resources.Load<Sprite>(unit_database.units.Sturture[GameManager.Instance.SubTypeUnit[index]].SpritePath_img);
            slot[index].GetComponent<Image>().sprite = img;

            //set value to object slot
            slot[index].GetComponent<slot2>().referentIndex = index;
            slot[index].GetComponent<slot2>().mainIndex = 2;
            slot[index].GetComponent<slot2>().UnitCoin = sold;
        }
        else if (GameManager.Instance.MainTypeUnit[index] == 3)
        {
            slot.Add(Instantiate(slotPrefab.gameObject));
            slot[index].transform.SetParent(TrapSlotPanel.transform);
            img = Resources.Load<Sprite>(unit_database.units.Trap[GameManager.Instance.SubTypeUnit[index]].SpritePath_img);
            slot[index].GetComponent<Image>().sprite = img;

            //set value to object slot
            slot[index].GetComponent<slot2>().referentIndex = index;
            slot[index].GetComponent<slot2>().mainIndex = 3;
            slot[index].GetComponent<slot2>().UnitCoin = sold;
        }
    }

    public static void deleteUnit(int x)
    {
        if (ChangeToScene.ready == 1 || ChangeToScene.readyState == 2) { return; }
        coin += slot[x].GetComponent<slot2>().UnitCoin;
        type[slot[x].GetComponent<slot2>().mainIndex]--;

        int temp = x;
        Destroy(slot[x]);
        slot.RemoveAt(x);
        GameManager.Instance.MainTypeUnit.RemoveAt(x);
        GameManager.Instance.SubTypeUnit.RemoveAt(x);
        for(int i = temp;i<slot.Count;i++)
        {
            slot[i].GetComponent<slot2>().referentIndex--;
        }
        print(GameManager.Instance.MainTypeUnit.Count);
    }

    //--------------------------------------------------------
    //update value form button
    public void SetCoinOnClick(int sellcoin)
    {
        if (ChangeToScene.ready == 1 || ChangeToScene.readyState == 2) { return; }
        //use in this screen
        if (coin - sellcoin >= 0)
        {
            coin -= sellcoin;
            sellSucess = true;
            sold = sellcoin;
        }
        else
        {
            sellSucess = false;
        }
    }

    public void SetTrapOnclick()
    {
        if (ChangeToScene.ready == 1) { return; }
        if (type[3] + 1 <= 5)
        {
            sellSucess = true;
        }
        else
        {
            sellSucess = false;
        }
    }

    public void SetTypeOnClick(int Type)
    {
        if (ChangeToScene.ready == 1 || ChangeToScene.readyState == 2) { return; }
        if (sellSucess)
        {
            type[Type]++;
            //send to map screen as referent index unit_database
            GameManager.Instance.MainTypeUnit.Add(Type);//contain index [0-3] only!!!
        }
    }

    public void SetIndexInType(int subType)
    {
        if (ChangeToScene.ready == 1 || ChangeToScene.readyState == 2) { return; }
        if (sellSucess)
        {
            //send to map screen as referent index each aerry in unit_database
            GameManager.Instance.SubTypeUnit.Add(subType);
            updateUnit();
        }
    }

    public void ReOnClick()
    {
        if (ChangeToScene.ready == 1 || ChangeToScene.readyState == 2) { return; }
        foreach (GameObject obj in slot)
        {
            Destroy(obj);
        }
        GameManager.Instance.MainTypeUnit.Clear();
        GameManager.Instance.SubTypeUnit.Clear();
        type[0] = 0;
        type[1] = 0;
        type[2] = 0;
        type[3] = 0;
        coin = 18;
    }
}
