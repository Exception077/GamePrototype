/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     UIManager.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-20
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UIManager : MonoBehaviour {
    public List<Dialog> DialogList = new List<Dialog>();
    public ItemManager Bag;

    private void Start() {
        closeDialog();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCodeList.Instance.Menu)) {
            // 关闭所有菜单或打开主菜单
            if(getActiveCount() > 0) {
                closeDialog();
                Time.timeScale = 1;
            } else {
                // 打开菜单
                Time.timeScale = 0;
                closeDialog();
                openDialog(findDialog("Menu"));
            }
        }
        if (Input.GetKeyDown(KeyCodeList.Instance.Bag)) {
            findDialog("Bag").openDialog();
        }
        if (Input.GetKeyDown(KeyCodeList.Instance.Equipment)) {
            findDialog("Equipment").openDialog();
        }
    }

    public Dialog findDialog(string name) {
        return DialogList.Find(delegate (Dialog d) {
            return d.DialogName == name;
        });
    }

    public int getActiveCount() {
        int count = 0;
        for (int i = 0; i < DialogList.Count; i++) {
            if(DialogList[i].gameObject.GetComponent<Canvas>().enabled == true) {
                count++;
            }
        }
        return count;
    }

    public void openDialog(Dialog dialog) {
        dialog.gameObject.GetComponent<Canvas>().enabled = true;
    }

    public void closeDialog(Dialog dialog) {
        dialog.gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void closeDialog() {
        for(int i = 0; i < DialogList.Count; i++) {
            closeDialog(DialogList[i]);
        }
    }

    public void openMenuDialog() {
        // 打开菜单
        Time.timeScale = 0;
        closeDialog();
        openDialog(findDialog("Menu"));
    }
}