// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Task",
             "FinishTask",
             "Finish a task and set its state FINISHED")]
public class FinishTask : Command
{
    public TaskManager MyTaskManager;
    public int TaskIndex;

    public override void OnEnter() {
        MyTaskManager.finishTask(MyTaskManager.TaskList[TaskIndex]);
        Debug.Log("Finish task:" + MyTaskManager.TaskList[TaskIndex].Name);
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
        return new Color32(124, 252, 0, 255);
    }
}
