using UnityEngine;
using System.Collections;
using Fungus;

public class TestTools : MonoBehaviour {
    [SerializeField]
    GameObject ToolsComponent;

    public void enableComponent() {
        ToolsComponent.SetActive(!ToolsComponent.activeSelf);
    }

    public void resetDeath() {
        PlayerPrefs.SetInt("DeadCount", 0);
        MessageBoard.Instance.generateMessage("<color=#008000><B>死亡次数已重置!</B></color>");
    }

    public void enableFlowchart(string messageName) {
        Flowchart.BroadcastFungusMessage(messageName);
    }
}
