/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     MessageBoard.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-03-22
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class MessageBoard : MonoBehaviour {
    public List<MessageItem> MessageList = new List<MessageItem>();
    public GameObject Grids;
    public GameObject MessageGrid;
    public GameObject Scroll;

    public static MessageBoard MyInstance;
    private static object syncRoot = new Object();
    public static MessageBoard Instance {
        get {
            if (MyInstance == null) {
                lock (syncRoot) {
                    if (MyInstance == null) {
                        MessageBoard[] MyInstances = FindObjectsOfType<MessageBoard>();
                        if (MyInstances != null) {
                            for (var i = 0; i < MyInstances.Length; i++) {
                                Destroy(MyInstances[i].gameObject);
                            }
                        }
                        GameObject go = new GameObject("MessageBoardManager");
                        MyInstance = go.AddComponent<MessageBoard>();
                        DontDestroyOnLoad(go);
                    }
                }
            }
            MyInstance.Grids = GameObject.Find("MessageItemGrids");
            MyInstance.MessageGrid = GameObject.Find("MessageItemGrid");
            MyInstance.Scroll = GameObject.Find("MessageBoardScroll");
            return MyInstance;
        }
    }

    public void Update() {
        try {
            if (MessageList.Count == 0) {
                Scroll.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
            }
            else {
                Scroll.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.01f);
            }
        } catch(System.Exception e) {
            //Debug.Log(e + " " + e.StackTrace);
        }
        
    }

    public void generateMessage(string content, bool stay = false) {
        GameObject temp = GameObject.Instantiate(MessageGrid, Grids.transform);
        temp.GetComponent<MessageItem>().updateInfo(content);
        MessageList.Add(temp.GetComponent<MessageItem>());
        if (stay != true) {
            temp.GetComponent<MessageItem>().delete();
        }
    }

    public void remove(MessageItem mi) {
        MessageList.Remove(mi);
    }

    public void clear() {
        for(int i = 0; i < MessageList.Count; i++) {
            MessageList[i].delete();
        }
        MessageList.Clear();
        Scroll.SetActive(false);
    }
}