/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     InfoDialog.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-07
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class InfoDialog : MonoBehaviour {
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Content;
    public TextMeshProUGUI Others;
    public TextMeshProUGUI ContentText;
    public Image Background;

    private void Update() {
        //ContentText.text = Content.text;
    }

    public void setInfo(string title, string content, string others = "") {
        Title.text = title;
        Content.text = content;
        LayoutRebuilder.ForceRebuildLayoutImmediate(Background.rectTransform);
        ContentText.text = content;
        Others.text = others;
    }
}