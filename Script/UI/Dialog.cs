/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Dialog.cs
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
public class Dialog : MonoBehaviour {
    public string DialogName;
    public int Level;

    private void Start() {
        if (PlayerPrefs.HasKey("DialogPosX" + DialogName)) {
            transform.localPosition = new Vector3(PlayerPrefs.GetFloat("DialogPosX" + DialogName), PlayerPrefs.GetFloat("DialogPosY" + DialogName), PlayerPrefs.GetFloat("DialogPosZ" + DialogName));
        }
    }

    private void Update() {
        PlayerPrefs.SetFloat("DialogPosX" + DialogName, transform.localPosition.x);
        PlayerPrefs.SetFloat("DialogPosY" + DialogName, transform.localPosition.y);
        PlayerPrefs.SetFloat("DialogPosZ" + DialogName, transform.localPosition.z);
    }

    public virtual void openDialog() {
        if (gameObject.GetComponent<Canvas>().enabled == true) {
            // 若已经打开背包，则将其关闭
            closeDialog();
        }
        else {
            // 打开背包
            gameObject.GetComponent<Canvas>().enabled = true;
        }
    }

    public virtual void closeDialog() {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void returnGame() {
        Time.timeScale = 1;
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}