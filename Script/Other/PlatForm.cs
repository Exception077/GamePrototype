/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     PlatForm.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-03-20
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
public class PlatForm : MonoBehaviour {
    public LayerMask PlayerLayer;
    public int DefualtLayer;
    public int GroundLayer;
    public Rigidbody2D Rbody2D;
    public Collider2D OutRange;
    private void FixedUpdate() {
        if (Physics2D.OverlapArea(OutRange.bounds.min, OutRange.bounds.max, PlayerLayer)) {
            gameObject.layer = DefualtLayer;
        } else {
            gameObject.layer = GroundLayer;
        }
    }
}