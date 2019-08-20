/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Buff.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-07
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buff : MonoBehaviour {
    public string Name;
    [TextArea(5,10)]
    public string Description;
    public Sprite Icon;
    public GameCharacter Target;
    public bool AutoFade;
    public float Duration;
    public float Timer;
    protected bool Active = true;

    /// <summary>
    /// 当获得BUFF时调用
    /// </summary>
    public virtual void onAbtain() {
        // 当BUFF类型为为限时BUFF时，开始计时
        if(AutoFade == true) {
            Timer = Duration;
        }
    }

    /// <summary>
    /// 当持有BUFF时调用
    /// </summary>
    public virtual void onKeep() {
        if(Active == false) {
            return;
        }
        if (AutoFade == true) {
            Timer -= Time.deltaTime;
            if(Timer <= 0) {
                Active = false;
            }
        }
    }

    /// <summary>
    /// 当清除BUFF时调用
    /// </summary>
    public virtual void onRemove() {

    }
}