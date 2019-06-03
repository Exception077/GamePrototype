/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     InitNPC.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-06-02
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System;

public class InitNPC : MonoBehaviour {
    [SerializeField]
    List<Item> ItemList;
    [SerializeField]
    GameCharacter Character;
    
    public void addItem() {
        try {
            foreach (Item i in ItemList) {
                Character.ItemList.Add(i);
            }
        } catch(Exception e) {
            print(e.Message);
        }
        
        print("add item to " + Character.CharacterName);
    }
}