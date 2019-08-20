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

public class MouseInfo : MonoBehaviour {
    public InfoDialog InfoUI;
    public Vector3 MousePos;
    
    public void showUI(string title, string content, string others = "") {
        InfoUI.gameObject.SetActive(true);
        InfoUI.setInfo(title, content, others);
    }

    public void hideUI() {
        InfoUI.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        MousePos = Input.mousePosition;
        //Camera.main.ScreenToWorldPoint(MousePos);
        InfoUI.gameObject.transform.position = MousePos;
	}
}