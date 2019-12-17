/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     MouseInfo.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-07
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

public class MouseInfo : MonoBehaviour {
    public InfoDialog InfoUI;
    public Vector3 MousePos;
    
    public void showUI(string title, string content, string others = "") {
        InfoUI.gameObject.SetActive(true);
        InfoUI.setInfo(title, content, others);
    }

    public void hideUI() {
        try {
            if(InfoUI.gameObject.activeSelf)
                InfoUI.gameObject.SetActive(false);
        } catch(Exception e) {

        }
    }
	
	// Update is called once per frame
	void Update () {
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(new Vector3(MousePos.x, MousePos.y, 0));
        //Camera.main.ScreenToWorldPoint(MousePos);
        InfoUI.gameObject.transform.position = new Vector3(MousePos.x, MousePos.y, 0);
    }
}