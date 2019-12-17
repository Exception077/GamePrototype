
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuickSlotItem : ItemGrid
{
    [Header("Reference")]
    //public ItemGrid MyItemGrid;
    public ItemManager Bag;
    public QuickSlotControl QSController;
    [Header("GridInfo")]
    public TextMeshProUGUI CountText;
    [SerializeField]
    Sprite DefaultIcon;
    public KeyCode HotKey;
    bool MouseIn = false;
    [Header("Data")]
    [SerializeField]string QSID;
    [SerializeField]
    MouseInfo MI;
    bool Ready = false;

    private void Start() {
        foreach (ItemGrid ig in Bag.ItemGridList) {
            if (ig.MyItem.QSID == QSID) {
                initItemGird(ig.MyItem, ig.ItemCount);
            }
        }
        if (MyItem != null) {
            Bag.removeItem(Bag.findItemGrid(MyItem), ItemCount, true);
            Bag.Owner.ItemList.Add(MyItem);
        }
        OriginalSize = CDMask.transform.localScale;
    }

    private void Update() {
        if (MouseIn && Input.GetMouseButtonUp(0) && DS.CurrentItemGrid != null) {
            if (DS.CurrentItemGrid.GetType() == typeof(QuickSlotItem)) {
                // 快捷栏内转移物品
                if (DS.CurrentItemGrid != MyItem) {
                    Item tempitem = MyItem;
                    int tempcount = ItemCount;
                    initItemGird(DS.CurrentItemGrid.MyItem, DS.CurrentItemGrid.ItemCount);
                    DS.CurrentItemGrid.initItemGird(tempitem, tempcount);
                } else {

                }
                DS.CurrentItemGrid = null;
            } else if (DS.CurrentItemGrid.GetType() == typeof(EquipmentGrid)) {
                // 装备栏->快捷栏
                if (MyItem != null) {
                    Bag.addItem(MyItem, ItemCount, true);
                }
                initItemGird(DS.CurrentItemGrid.MyItem, DS.CurrentItemGrid.ItemCount);
                DS.CurrentItemGrid.release();
                Bag.removeItem(Bag.findItemGrid(MyItem), 1, true);
                DS.CurrentItemGrid = null;
                Bag.Owner.ItemList.Add(MyItem);
            } else {
                // 物品栏->快捷栏
                if (MyItem != null) {
                    Bag.addItem(MyItem, ItemCount, true);
                }
                initItemGird(DS.CurrentItemGrid.MyItem, DS.CurrentItemGrid.ItemCount);
                Bag.removeItem(DS.CurrentItemGrid, DS.CurrentItemGrid.ItemCount, true);
                DS.CurrentItemGrid = null;
                Bag.Owner.ItemList.Add(MyItem);
            }
            
            
        }
        if (MouseIn && Input.GetMouseButtonDown(1) && MyItem != null) { 
            MyItem.QSID = "";
            Bag.addItem(MyItem, ItemCount, true);
            MyIcon.sprite = DefaultIcon;
            CountComponent.SetActive(false);
            CDComponent.SetActive(false);
            MyItem = null;
        }
        #region initItemGird
        if (MyItem == null) {
            MyIcon.sprite = DefaultIcon;
            CountComponent.SetActive(false);
            CDComponent.SetActive(false);
        } else {
            MyItem.onHold();
            MyIcon.enabled = true;
            CountComponent.SetActive(true);
            //MyIcon.sprite = MyItemGrid.MyIcon.sprite;
            CountText.text = ItemCount.ToString();
            if (Input.GetKeyDown(HotKey)) {
                Bag.useItem(this, 1);
            }
            #region CDCheck
            try {
                if (MyItem.CoolDown != 0) {
                    if (MyItem.CurrentCoolDown == MyItem.CoolDown) {
                        CDComponent.SetActive(false);
                        CDText.text = "0%";
                    }
                    else {
                        CDComponent.SetActive(true);
                        int percent = int.Parse(string.Format("{0:0}", MyItem.CurrentCoolDown / MyItem.CoolDown * 100f));
                        CDMask.transform.localScale = new Vector2(OriginalSize.x, OriginalSize.y * (1 - percent * 0.01f));
                        if (percent < 80 && percent % 5 == 0) {
                            CDText.text = string.Format("{0:0%}", MyItem.CurrentCoolDown / MyItem.CoolDown);
                        }
                        else if (percent >= 80) {
                            CDText.text = string.Format("{0:0%}", MyItem.CurrentCoolDown / MyItem.CoolDown);
                        }
                    }
                } else {
                    CDComponent.SetActive(false);
                }
            } catch (Exception e) {
                print(e.StackTrace);
            }
            #endregion
        }
        #endregion

    }

    private void OnMouseDown() {
        print("??");
    }

    private void OnMouseOver() {
        Ready = true;
        MouseIn = true;
        if (MyItem == null) {
            return;
        }
        if(DS.CurrentItemGrid == null)
            MI.showUI(MyItem.Name, MyItem.Description);
    }

    private void OnMouseExit() {
        if (Ready == true) {
            MI.hideUI();
            Ready = false;
        }
        MouseIn = false;
        Timer = 0;
    }

    private void OnMouseDrag() {
        if (Timer >= StayTime) {
            if (MyItem != null) {
                DS.show(this, "");
            }
        } else {
            Timer += Time.deltaTime;
        }
    }

    private void OnMouseUp() {
        Timer = 0;
    }

    public override void initItemGird(Item item, int count) {
        if (item == null) {
            MyIcon.sprite = DefaultIcon;
            CountComponent.SetActive(false);
            CDComponent.SetActive(false);
            MyItem = null;
            return;
        }
        item.QSID = QSID;
        MyItem = item;
        MyIcon.sprite = item.Icon.sprite;
        ItemCount = count;
    }

    public override bool onRemoveItem(int count) {
        if (MyItem.Descarded == false) {
            return false;
        }
        else if (count > ItemCount) {
            return false;
        }
        else if (ItemCount - count == 0) {
            MyItem.onRemove();
            MyItem.Owner.ItemList.Remove(MyItem);

            MyIcon.sprite = DefaultIcon;
            CountComponent.SetActive(false);
            CDComponent.SetActive(false);
            MyItem = null;
        } else {
            updateItemInfo(MyItem, ItemCount - count);
            MyItem.onRemove();
        }
        return true;
    }
}
