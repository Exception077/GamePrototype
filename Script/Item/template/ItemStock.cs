/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     ItemStock.cs
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
public class ItemStock : MonoBehaviour {
    public List<Item> ItemList = new List<Item>();

    public static ItemStock MyInstance;
    private static object syncRoot = new Object();
    public static ItemStock Instance {
        get {
            if (MyInstance == null) {
                lock (syncRoot) {
                    if (MyInstance == null) {
                        ItemStock[] MyInstances = FindObjectsOfType<ItemStock>();
                        if (MyInstances != null) {
                            for (var i = 0; i < MyInstances.Length; i++) {
                                Destroy(MyInstances[i].gameObject);
                            }
                        }
                        GameObject go = new GameObject("ItemStock");
                        MyInstance = go.AddComponent<ItemStock>();
                        DontDestroyOnLoad(go);
                    }
                }
            }
            return MyInstance;
        }
    }

    public Item getItemByID(string id) {
        Item item = new Item();
        foreach (Item i in ItemList) {
            if (i.ID == id) {
                item = GameObject.Instantiate(i, transform);
            }
        }
        return item;
    }

    public bool containsItem(string id) {
        foreach(Item i in ItemList) {
            if(i.ID == id) {
                return true;
            }
        }
        return false;
    }

    public void removeItem(string name) {
        foreach(Item i in ItemList) {
            if(i.Name == name) {
                ItemList.Remove(i);
                break;
            }
        }
    }

    public void clear() {
        ItemList.Clear();
    }
}