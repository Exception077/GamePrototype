/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     AsideManager.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-03-23
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class AsideManager : MonoBehaviour {
    public GameObject AsideUI;
    public Text ContentText;
    public int MaxContentLength;
    public float MaxStayTime;
    private string ContentCache;

    /// <summary>
    /// 显示一条信息，如果内容超出最大单次显示字数，将被自动切割
    /// </summary>
    /// <param name="content">内容</param>
    public void showContent(string content) {
        if(content.Length <= MaxContentLength) {
            AsideUI.SetActive(true);
            ContentText.text = content;
            Invoke("clear", MaxStayTime);
        } else {
            showMultipleContent(content);
        }
    }
    /// <summary>
    /// 从信息列表逐条显示内容，注意，该方法不会对内容进行切割，调用前请确保列表中每条消息内容长度合适
    /// </summary>
    /// <param name="contentList">信息列表</param>
    public void showContent(List<string> contentList) {
        for(int i = 0; i < contentList.Count; i++) {
            ContentCache = contentList[i];
            Invoke("showChache", MaxStayTime * i);
        }
        Invoke("clear", MaxStayTime * contentList.Count);
    }

    private void showMultipleContent(string content) {
        List<string> contentList = new List<string>();
        while(content.Length > MaxContentLength) {
            contentList.Add(content = content.Substring(0, MaxContentLength));
        }
        if(content.Length > 0) {
            contentList.Add(content);
        }
        for(int i = 0; i < contentList.Count; i++) {
            ContentCache = contentList[i];
            Invoke("showChache", MaxStayTime * i);
        }
        Invoke("clear", MaxStayTime * contentList.Count);
    } 

    private void showChache() {
        ContentText.text = ContentCache;
    }

    private void clear() {
        ContentCache = "";
        AsideUI.SetActive(false);
    }
}