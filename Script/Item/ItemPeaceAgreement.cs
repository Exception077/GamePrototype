/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     ItemPeaceAgreement.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-20
 *Update:
 *Description:   当角色获取该物品时，无法攻击NPC，当移除物品后恢复伤害判定
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
[System.Serializable]
public class ItemPeaceAgreement : Item {
    public LayerMask NewLayer;
    public LayerMask OldLayer;

    public override void onObtain() {
        base.onObtain();
        Player p = new Player();
        if(Owner.GetType() == p.GetType()) {
            OldLayer = Owner.EnemyLayer;
            Owner.EnemyLayer = NewLayer;
        }
    }

    public override void onRemove() {
        base.onRemove();
        Player p = new Player();
        if (Owner.GetType() == p.GetType()) {
            Owner.EnemyLayer = OldLayer;
        }
    }

    public override void onHold() {
        base.onHold();
        Player p = new Player();
        if (Owner.GetType() == p.GetType()) {
            Owner.EnemyLayer = NewLayer;
        }
    }

    public override void onAbout() {
        base.onAbout();
        Owner.startChat(AboutInfo, 1 + AboutInfo.Length / 10f);
    }
}