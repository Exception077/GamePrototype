// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotControl : MonoBehaviour {
    public List<QuickSlotItem> QSItemList = new List<QuickSlotItem>();
    public QuickSlotItem CurrentQSItem;

    public void clear() {
        foreach(QuickSlotItem qsi in QSItemList) {
            //qsi.MyItem = null;
        }
    }
}
