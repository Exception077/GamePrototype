/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Request.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-03-24
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
[System.Serializable]
public enum RequirementType{
    CHAT, KILL, SEARCH
}
[System.Serializable]
public class Requirement {
    [SerializeField]
    RequirementType Type;
    [SerializeField]
    string ChatTargetName;
    [SerializeField]
    string KillTargetName;
    [SerializeField]
    int TotalKillCount;
    [SerializeField]
    int CurrentKillCount;
    [SerializeField]
    string ItemName;
    [SerializeField]
    int TotalSearchCount;
    [SerializeField]
    int CurrentSearchCount;
    [SerializeField]

    public bool check() {
        switch (Type) {
            case RequirementType.CHAT:
                break;
            case RequirementType.KILL:
                CurrentKillCount = 0;
                foreach(GameCharacter g in GameCharacterManager.Instance.BodyList) {
                    if(g.CharacterName == KillTargetName) {
                        CurrentKillCount++;
                    }
                }
                if(CurrentKillCount >= TotalKillCount) {
                    return true;
                } else {
                    break;
                }
            case RequirementType.SEARCH:
                CurrentSearchCount = 0;
                foreach(ItemGrid ig in GameCharacterManager.Instance.PlayerList[0].Bag.ItemGridList) {
                    if(ig.MyItem.Name == ItemName) {
                        CurrentSearchCount = ig.ItemCount;
                        break;
                    }
                }
                if(CurrentSearchCount >= TotalSearchCount) {
                    return true; 
                } else {
                    break;
                }
        }
        return false;
    }
}