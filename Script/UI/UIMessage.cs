/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     UIMessage.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2018-09-16
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UIMessage : MonoBehaviour {
    public Text mapMessage;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void setMapMessageInfo(string message)
    {
        mapMessage.text = message;
    }
}