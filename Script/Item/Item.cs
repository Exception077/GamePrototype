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
using System.Collections.Generic;
using UnityEngine.UI;
[System.Serializable]
enum ItemStateEnum
{
    NORMAL, 
}
public enum EquipType {
    NONE, WEAPON, HEAD, BODY, FEET, ORNAMENT
}
[System.Serializable]
public class Item : MonoBehaviour {
    // 唯一标识码
    public string ID = "";
    public string QSID = "";
    // 物品名
    public string Name;
    // 类型
    public EquipType EquipType;
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

    // 解析状态
    public virtual void setStatus(string state) {
        if (state == null) {
            return;
        }
        List<string> strs = new List<string>();
        while (state.Contains("#")) {
            if (state.Contains("|")) {
                strs.Add(state.Substring(state.IndexOf('#') + 1, state.IndexOf('|') - state.IndexOf('#') - 1));
                state = state.Substring(state.IndexOf('|') + 1);
            } else {
                strs.Add(state.Substring(state.IndexOf('#') + 1));
                state = "";
            } 
        }
        QSID = strs[0];
        UseCount = int.Parse(strs[1]);
        CurrentCoolDown = int.Parse(strs[2]);
    }

    // 生成状态字串
    public virtual string getStatus() {
        string state = "QSID#" + QSID + "|UseCount#" + UseCount.ToString() + "|CurCD#" + CurrentCoolDown;
        return state;
    }
}