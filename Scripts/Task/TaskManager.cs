/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     TaskManager.cs
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
using System.Collections.Generic;
using Fungus;

public class TaskManager : MonoBehaviour {
    public ItemManager MyItemManager;
    [Header("Task List")]
    public List<Task> TaskList;
    public Flowchart MyFlowchart;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    foreach(Task t in TaskList) {
            if (t.CurrentState == TaskState.ACCEPTED && t.checkProcess()) {
                //finishTask(t);
                foreach(VariableBase<bool> var in MyFlowchart.Variables) {
                    if(var.Key == "Finish" + t.Name) {
                        var.Apply(SetOperator.Assign, true);
                    }
                }
            }
        }
	}

    public void acceptTast(Task task) {
        task.CurrentState = TaskState.ACCEPTED;
    }

    public void finishTask(Task task) {
        MessageBoard.Instance.generateMessage("完成任务：<color=#00FF00>" + task.Name + "</color>");
        MyItemManager.addItem(ItemStock.Instance.getItemByID(task.RewardName), task.RewardCount);
        task.CurrentState = TaskState.FINISHED;
    }

    public void cancelTask(Task task) {
        task.CurrentState = TaskState.IDLE;
    }
}