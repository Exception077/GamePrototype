/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     BuffOperator.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-07
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using Fungus;

[CommandInfo("Buff",
             "BuffOperator",
             "Add or remove buff from the specific GameCharacter Object")]
public class BuffOperator : Command {
    public GameCharacter Target;
    public Buff BUFF;
    public OperateMode OperateType;

    public override void OnEnter() {
        switch (OperateType) {
            case OperateMode.ADD:
                Target.gameObject.GetComponent<BuffManager>().addBuff(BUFF);
                break;
            case OperateMode.REMOVE:
                Target.gameObject.GetComponent<BuffManager>().removeBuff(BUFF);
                break;
        }
        Continue();
    }

    public override string GetSummary() {
        try {
            switch (OperateType) {
                case OperateMode.ADD:
                    return "ADD:" + BUFF.Name + " (to " + Target.CharacterName + ")";
                case OperateMode.REMOVE:
                    return "Remove:" + BUFF.Name + " (from " + Target.CharacterName + ")";
                default:
                    return "...";
            }
        }
        catch {
            return "Error:BUFF or character is null";
        }
    }

    public override Color GetButtonColor() {
        return new Color32(200, 0, 175, 255);
    }
}