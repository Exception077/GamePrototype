/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Item.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-19
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[System.Serializable]
enum ItemStateEnum
{
    NORMAL, 
}
[System.Serializable]
public class Item : MonoBehaviour {
    // 物品名
    public string Name;
    [TextArea(5, 10)]
    // 物品描述
    public string Description;
    // 相关信息
    public string AboutInfo;
    // 物品图标
    public Image Icon;
    // 是否可丢弃
    public bool Descarded;
    // 是否可使用
    public bool IsTool;
    public string OperateName;
    // 是否无限使用
    public bool Unlimited;
    // 使用时间
    public float CureTime;
    // 冷却
    public float CoolDown;
    public float CurrentCoolDown;
    public float DeltaCoolDown;
    public int UseCount;
    // 拥有者
    public GameCharacter Owner;
    // 当获得物品时触发
    public virtual void onObtain() {}

    // 当删除物品时触发
    public virtual void onRemove() {}

    // 当持有物品时触发
    public virtual void onHold() {}

    // 当使用物品时触发
    public virtual bool onUse() {
        return false;
    }
    
    // 当获取物品相关信息时触发
    public virtual void onAbout() {

    }
}