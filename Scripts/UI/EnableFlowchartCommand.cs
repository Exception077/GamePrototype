// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class EnableFlowchartCommand : MonoBehaviour
{
    public string MessageName;

    public void enableMessage() {
        Flowchart.BroadcastFungusMessage(MessageName);
    }
}
