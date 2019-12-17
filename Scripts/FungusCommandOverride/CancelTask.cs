// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Task",
             "CancelTask",
             "Cancel a task and set its state IDEL")]
public class CancelTask : Command
{
    public TaskManager MyTaskManager;
    public int TaskIndex;

    public override void OnEnter() {
        MyTaskManager.cancelTask(MyTaskManager.TaskList[TaskIndex]);
        Debug.Log("Cancel task:" + MyTaskManager.TaskList[TaskIndex].Name);
        Continue();
    }

    public override string GetSummary() {
        try {
            return MyTaskManager.TaskList[TaskIndex].Name;
        }
        catch {
            return "...";
        }
    }

    public override Color GetButtonColor() {
        return new Color32(128, 128, 128, 255);
    }
}
