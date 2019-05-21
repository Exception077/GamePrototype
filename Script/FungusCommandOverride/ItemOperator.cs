/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     ItemOperator.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-05
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using Fungus;

public enum OperateMode
{
    ADD, REMOVE
}

[CommandInfo("Item",
             "ItemOperator",
             "Add or remove item from the specific GameCharacter Object")]
public class ItemOperator : Command {
    public Item TargetItem;
    public int Count = 1;
    public ItemManager Manager;
    public GameCharacter TargetCharacter;
    public OperateMode OperateType;

    public override void OnEnter() {
        switch (OperateType) {
            case OperateMode.ADD:
                if(TargetCharacter.GetType() == typeof(Player)) {
                    Manager.addItem(TargetItem, Count);
                } else {
                    TargetCharacter.ItemList.Add(TargetItem);
                }
                break;
            case OperateMode.REMOVE:
                if (TargetCharacter.GetType() == typeof(Player)) {
                    Manager.removeItem(Manager.findItemGrid(TargetItem), Count);
                }
                else {
                    TargetCharacter.ItemList.Remove(TargetItem);
                }
                break;
        }
        
        Continue();
    }

    public override string GetSummary() {
        try {
            switch (OperateType) {
                case OperateMode.ADD:
                    return "ADD:" + TargetItem.Name + " (to " + TargetCharacter.CharacterName + ")";
                case OperateMode.REMOVE:
                    return "Remove:" + TargetItem.Name + " (from " + TargetCharacter.CharacterName + ")";
                default:
                    return "...";
            }
        }
        catch {
            return "Error:Item or character is null";
        }
    }

    public override Color GetButtonColor() {
        return new Color32(255, 255, 100, 255);
    }
}