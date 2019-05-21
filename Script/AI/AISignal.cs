/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     AISignal.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-18
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System;
using System.Collections;

public class AISignal : MonoBehaviour {
    [Header("Range")]
    [SerializeField]Collider2D ViewRange;
    [Header("Layer")]
    [SerializeField]LayerMask TargetLayer;
    [Header("Switch")]
    [SerializeField]bool SIGNAL_ON = true;
    public Transform Flag;
    [Header("Status")]
    [SerializeField]GameCharacter Target;
    public bool Caught;
    public bool RightSide;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (SIGNAL_ON == false || Target == null)
            return;
        Caught = checkView(Target);
	}
    /// <summary>
    /// 设置检测对象
    /// </summary>
    /// <param name="gc"></param>
    public void setTarget(GameCharacter gc) {
        Target = gc;
    }
    /// <summary>
    /// 判断检测对象是否在范围内
    /// </summary>
    /// <param name="gc"></param>
    /// <returns></returns>
    bool checkView(GameCharacter gc) {
        if (gc == null) {
            return false;
        }
        else {
            Bounds bounds = ViewRange.bounds;
            Collider2D[] results = new Collider2D[20];
            try {
                ContactFilter2D tempFilter = new ContactFilter2D();
                tempFilter.SetLayerMask(TargetLayer);
                Physics2D.OverlapArea(bounds.min, bounds.max, tempFilter, results);
                if (Physics2D.OverlapArea(bounds.min, bounds.max, TargetLayer)) {
                    for (int i = 0; i < 20; i++) {
                        if (results[i].gameObject == gc.LoadObj) {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e) {
                Debug.Log(e.Message);
                return false;
            }
            return false;
        }
    }
}