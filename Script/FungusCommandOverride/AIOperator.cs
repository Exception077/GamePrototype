/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     AIOperator.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-19
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using Fungus;

[CommandInfo("AI",
             "AIOperator",
             "Reset AI status")]
public class AIOperator : Command {
    public NPC m_NPC;
    public AIMode AIType;

    public override void OnEnter() {
        m_NPC.MyAI.Mode = AIType;
        Continue();
    }

    public override string GetSummary() {
        try {
            return "Reset AIMode as " + AIType + "(" + m_NPC.CharacterName + ")";
        }
        catch {
            return "Error:NPC gameobject is null";
        }
    }

    public override Color GetButtonColor() {
        return new Color32(0, 191, 255, 255);
    }
}