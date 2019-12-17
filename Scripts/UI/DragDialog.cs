/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     DragDialog.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-31
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;

public class DragDialog : MonoBehaviour {
    Vector3 MousePos;
    Vector2 Offset;
    [SerializeField]
    GameObject Component;

    private void OnMouseDrag() {
        Component.transform.position = new Vector3(MousePos.x - Offset.x, MousePos.y - Offset.y, Component.transform.position.z);
    }

    private void OnMouseDown() {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Offset = MousePos - Component.transform.position;
    }

    private void OnMouseOver() {
        // Offset = MousePos - Component.transform.position;
    }

    // Update is called once per frame
    void Update () {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}