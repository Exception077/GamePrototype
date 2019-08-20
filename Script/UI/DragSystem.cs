/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     DragSystem.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-11
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragSystem : MonoBehaviour {
    public DragComponent Component;
    public ItemGrid CurrentItemGrid;
    public List<GameObject> HighLightList;
    Vector3 MousePos;

    public void show(ItemGrid ig, string info) {
        if (ig.MyItem == null) {
            return;
        }
        CurrentItemGrid = ig;
        Component.setInfo(ig.MyItem.Icon.sprite, info);
        Component.gameObject.SetActive(true);
        foreach(GameObject go in HighLightList) {
            go.SetActive(true);
        }
    }

    public void hide() {
        foreach (GameObject go in HighLightList) {
            go.SetActive(false);
        }
        CurrentItemGrid = null;
        Component.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        MousePos = Input.mousePosition;
        Component.gameObject.transform.position = MousePos;
        if (Input.GetMouseButtonUp(0)) {
            hide();
        }
    }
}