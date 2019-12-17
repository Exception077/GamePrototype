/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     ScreenSetting.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-11
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;

public class ScreenSetting : MonoBehaviour {
    public int W;
    public int H;
    public bool FullScreen;
	// Use this for initialization
	void Start () {
        Screen.SetResolution(W, H, FullScreen);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}