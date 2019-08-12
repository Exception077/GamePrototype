/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     ItemLoadIndex.cs
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
[System.Serializable]
public class ItemLoadIndex{
    public string Name;
    public int Count;
    public string State;

     public ItemLoadIndex(string name, int count,string state) {
        Name = name;
        Count = count;
        State = state;
    }
}