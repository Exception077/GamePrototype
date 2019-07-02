/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     ItemPoint.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-20
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
public class ItemPoint : MonoBehaviour {
    public ItemManager Manager;
    public Item MyItem;
    public int count;
    public LayerMask TargetLayer;
    public Collider2D Range;
    public bool Obtain;
    public GameObject Signal;
    public GameObject Tips;

    private void Start() {
       
    }

    private void Update() {
        Signal.SetActive(!Obtain);
        if(Physics2D.OverlapArea(Range.bounds.min, Range.bounds.max, TargetLayer) && !Obtain) {
            Tips.SetActive(true);
            if (Input.GetKeyDown(KeyCodeList.Instance.Interactive)) {
                Manager.addItem(MyItem, count);
                Obtain = true;
            }
        } else {
            Tips.SetActive(false);
        }
    }
}