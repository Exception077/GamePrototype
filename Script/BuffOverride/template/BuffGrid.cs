/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     BuffGrid.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-07
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class BuffGrid : MonoBehaviour {
    public Image Icon;
    public Buff BUFF;
    public MouseInfo MI;
    public BuffManager BM;
    bool Ready = false;

    public void setInfo(Buff buff) {
        BUFF = buff;
        Icon.sprite = BUFF.Icon;
    }

    private void Update() {
        if(BUFF.AutoFade == true && BUFF.Timer <= 0) {
            BM.removeBuff(BUFF);
            MI.hideUI();
            Destroy(gameObject);
        }
    }

    private void OnMouseOver() {
        Ready = true;
        if (BUFF == null) {
            return;
        }
        print("show");
        MI.showUI(BUFF.Name, BUFF.Description, BUFF.AutoFade == true ? string.Format("({0:N1}s)", BUFF.Timer) : "unlimited");
    }

    private void OnMouseExit() {
        if(Ready == true) {
            MI.hideUI();
            Ready = false;
        }
    }

    public void onRemove() {
        Destroy(gameObject);
    }
}