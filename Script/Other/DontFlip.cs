/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     DontFlip.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-03-23
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
public class DontFlip : MonoBehaviour {
    public Transform ParentTransform;
    public RectTransform MyTransform;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(ParentTransform.localScale.x < 0) {
            MyTransform.localScale = new Vector3(-1, 1, 1);
        } else {
            MyTransform.localScale = new Vector3(1, 1, 1);
        }
        
	}
}