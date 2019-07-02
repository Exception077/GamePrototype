/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Link2Stock.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-22
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;

public class Link2Stock : MonoBehaviour {
    [SerializeField]
    Item MyItem;

	// Use this for initialization
	void Start () {
        link();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void link() {
        if (ItemStock.Instance.containsItem(MyItem.Name)) {
            ItemStock.Instance.removeItem(MyItem.Name);
        }
        ItemStock.Instance.ItemList.Add(MyItem);
    }
}