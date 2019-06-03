/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     DataForNPC.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-06-02
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class DataForNPC : Data {
    public List<ItemLoadIndex> ItemReference = new List<ItemLoadIndex>();
    public float CurrentHealth;
    public float TotalHealth;
    public float CurrentEnergy;
    public float TotalEnergy;
    public int Coins;

    public void addItemIndex(string name, int count) {
        if (ItemReference.Contains(findItemIndex(name)) == true) {
            findItemIndex(name).Count += count;
        }
        else {
            ItemReference.Add(new ItemLoadIndex(name, count));
        }
    }

    public ItemLoadIndex findItemIndex(string name) {
        return ItemReference.Find(delegate (ItemLoadIndex i) {
            return i.Name == name;
        });
    }
}