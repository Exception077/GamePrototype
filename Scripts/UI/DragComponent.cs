/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     DragComponent.cs
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
using TMPro;

public class DragComponent : MonoBehaviour {
    public Image Icon;
    public TextMeshProUGUI InfoText;
    [SerializeField]

    public void setInfo(Sprite icon, string info) {
        Icon.sprite = icon;
        InfoText.text = info;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}