using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class EquipmentGrid : ItemGrid {
    public Equipment Equipment;
    public EquipType EquipType;
    public string ESID;
    [SerializeField]
    GameObject HighlightComponent;
    [SerializeField]
    Sprite DefaultIcon;
    [SerializeField]
    ItemManager IM;
    bool Ready = false;
    [SerializeField]
    MouseInfo MI;
    [SerializeField]
    EquipmentSystem ES;
    bool MouseIn = false;
    // Use this for initialization
    void Start() {
        foreach (ItemGrid ig in IM.ItemGridList) {
            if (ig.MyItem.QSID == ESID) {
                initGrid(ig);
            }
        }
        if (MyItem != null) {
            IM.removeItem(IM.findItemGrid(MyItem), ItemCount, true);
            IM.Owner.ItemList.Add(MyItem);
        }
    }

    // Update is called once per frame
    void Update() {
        if (DS.CurrentItemGrid != null && DS.CurrentItemGrid != this) {
            if (DS.CurrentItemGrid.MyItem != null
                && DS.CurrentItemGrid.MyItem.EquipType == EquipType)
                HighlightComponent.SetActive(true);
        } else {
            HighlightComponent.SetActive(false);
        }
        if (MouseIn && Input.GetMouseButtonUp(0) && DS.CurrentItemGrid != null && DS.CurrentItemGrid.MyItem.EquipType == EquipType) {
            if (MyItem != null && DS.CurrentItemGrid.MyItem != MyItem) {
                // 替换
                Item tempitem = MyItem;
                initGrid(DS.CurrentItemGrid);
                if (DS.CurrentItemGrid.GetType() == typeof(QuickSlotItem)) {
                    DS.CurrentItemGrid.initItemGird(tempitem,1);
                } else {
                    IM.removeItem(DS.CurrentItemGrid, 1, true);
                    IM.addItem(tempitem, 1, true);
                }
            } else if(MyItem == null) {
                // 添加
                initGrid(DS.CurrentItemGrid);
                if (DS.CurrentItemGrid.GetType() == typeof(QuickSlotItem)) {
                    DS.CurrentItemGrid.onRemoveItem(1);
                }
                else {
                    IM.removeItem(DS.CurrentItemGrid, 1, true);
                }
            }
            
            IM.Owner.ItemList.Add(MyItem);
        }
        if (MouseIn && Input.GetMouseButtonDown(1) && Equipment!= null) {
            MyItem.QSID = "";
            release();
        }
    }

    private void OnMouseDown() {
        print("?");    
    }

    public override void release() {
        ES.release(Equipment);
        IM.Owner.EquipmentList.Remove(Equipment);
        IM.addItem(this.Equipment, 1, true);
        Equipment = null;
        MyItem = null;
        MyIcon.sprite = DefaultIcon;
    }

    private void OnMouseOver() {
        Ready = true;
        MouseIn = true;
        if (Equipment == null || MyItem == null) {
            return;
        }
        if(DS.CurrentItemGrid == null)
            MI.showUI(Equipment.Name, Equipment.Description);
    }

    private void OnMouseDrag() {
        if (Timer >= StayTime) {
            if (DS.CurrentItemGrid == null) {
                DS.show(this, "");
            }
        }
        else {
            Timer += Time.deltaTime;
        }
    }

    private void OnMouseExit() {
        if (Ready == true) {
            MI.hideUI();
            Ready = false;
        }
        MouseIn = false;
        Timer = 0;
    }

    private void OnMouseUp() {
        Timer = 0;
    }

    private void OnDisable() {
        MI.hideUI();
    }

    public override bool onRemoveItem(int count) {
        print("???");
        return true;
    }

    void initGrid(ItemGrid itemGrid) {
        Equipment = (Equipment)itemGrid.MyItem;
        MyItem = Equipment;
        MyIcon.sprite = itemGrid.MyItem.Icon.sprite;
        ItemCount = itemGrid.ItemCount;
        // 调用装备效果
        ES.equip(Equipment);
        itemGrid.MyItem.QSID = ESID;
    }
}
