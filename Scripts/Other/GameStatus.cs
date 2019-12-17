/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     GameStatus.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-11
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using TMPro;
public class GameStatus : MonoBehaviour {
    public TextMeshProUGUI ResolvingPower;
    public TextMeshProUGUI FPSText;
    private float m_LastUpdateShowTime = 0f;    //上一次更新帧率的时间;
    private float m_UpdateShowDeltaTime = 0.1f;//更新帧率的时间间隔;
    private int m_FrameUpdate = 0;//帧数;
    private float m_FPS = 0;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start() {
        m_LastUpdateShowTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update() {
        string rtext = Screen.width.ToString() + "*" + Screen.height.ToString();
        ResolvingPower.text = rtext;
        m_FrameUpdate++;
        if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime) {
            m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
            m_FrameUpdate = 0;
            m_LastUpdateShowTime = Time.realtimeSinceStartup;
        }
        FPSText.text = string.Format("FPS:{0:0}", m_FPS);
    }
}