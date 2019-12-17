// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SaveFlowchart : MonoBehaviour
{
    public void save() {
        Flowchart.BroadcastFungusMessage("SaveData");
    }
}
