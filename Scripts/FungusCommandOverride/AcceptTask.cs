// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Task",
             "AcceptTask", 
             "Get a task and set its state ACCEPTED")]
public class AcceptTask : Command {
    public TaskManager MyTaskManager;
    public int TaskIndex;

    public override void OnEnter() {
        MyTaskManager.acceptTast(MyTaskManager.TaskList[TaskIndex]);
        Debug.Log("Accept task:" + MyTaskManager.TaskList[TaskIndex].Name);
        Continue();
    }

    public override string GetSummary() {
        try {
            return MyTaskManager.TaskList[TaskIndex].Name;
        } catch {
            return "Error:Task is null";
        }
    }

    public override Color GetButtonColor() {
        return new Color32(173, 255, 47, 255);
    }
}
