/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     MessageItem.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-03-22
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class MessageItem : MonoBehaviour {
    public TextMeshProUGUI ContentText;
    public string Content;
    public float MaxTime;

    public void updateInfo(string content) {
        Content = content;
        ContentText.text = Content;
    }

    public void delete() {
        Invoke("disappear", MaxTime);
    }

    private void disappear() {
        MessageBoard.Instance.remove(this);
        Destroy(gameObject);
    }
}