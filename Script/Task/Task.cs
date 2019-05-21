/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Task.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-03-24
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public enum TaskState {
    IDLE, ACCEPTED, FINISHED
}
[System.Serializable]
public class Task {
    public TaskState CurrentState;
    public string Name;
    public string Description;
    public string RewardName;
    public int RewardCount;
    public List<Requirement> RequirementList;

    public bool checkProcess() {
        for(int i = 0; i < RequirementList.Count; i++) {
            if (!RequirementList[i].check()) {
                return false;
            }
        }
        CurrentState = TaskState.FINISHED;
        return true;
    }
}