/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     DataForPlayer.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-21
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class DataForPlayer : Data{
    public List<ItemLoadIndex> ItemReference = new List<ItemLoadIndex>();
    public List<BuffLoadIndex> BuffReference = new List<BuffLoadIndex>();
    public float CurrentHealth;
    public float TotalHealth;
    public float CurrentEnergy;
    public float TotalEnergy;
    public float CurrentHungerValue;
    public float TotalHungerValue;
    public int Coins;

    public void addItemIndex(string name, int count, string state) {
        if (ItemReference.Contains(findItemIndex(name)) == true) {
            findItemIndex(name).Count += count;
        } else {
            ItemReference.Add(new ItemLoadIndex(name, count, state));
        }
    }

    public ItemLoadIndex findItemIndex(string name) {
        return ItemReference.Find(delegate (ItemLoadIndex i) {
            return i.ID == name;
        });
    }
}