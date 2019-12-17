/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     LoadScene.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-03-10
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LoadScene : MonoBehaviour {
    public string SceneName;
    public void loadScene() {
        Time.timeScale = 1;
        PlayerPrefs.SetString("NextStage", SceneName);
        SceneManager.LoadScene("progress");
        ItemStock.Instance.clear();
    }
}