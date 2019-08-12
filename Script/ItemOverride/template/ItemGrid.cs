/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     ItemGrid.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-19
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class ItemGrid : MonoBehaviour {
    public ItemManager Manager;
    public GameObject LoadObj;
    public Image BackgroundImage;
    [Header("Reference")]
    public Item MyItem;
    public Image MyIcon;
    public int ItemCount;
    [Header("UISettings")]
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemCountText;
    public TextMeshProUGUI CDText;
    public GameObject CDMask;
    public GameObject CDComponent;
    public bool Selected = false;
    public Color SelectedColor;
    public Color UnselectedColor;
    public Collider2D Box;
    [Header("DragSystem")]
    public DragSystem DS;
    Vector2 OriginalSize;
    [SerializeField]
    float StayTime;
    float Timer = 0;

    private void Start() {
        OriginalSize = CDMask.transform.localScale;
    }

    private void Update() {
        if(MyItem != null) {
            MyItem.onHold();
            if(Manager.CurrentItemGrid == this) {
                Vector3 mousePos = new Vector3
                    (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Box.bounds.center.z);
                if (Box.bounds.Contains(mousePos) && Input.GetMouseButtonDown(1)) {
                    MyItem.onAbout();
                }
            }
            if (MyItem.CoolDown != 0) {
                if(MyItem.CurrentCoolDown == MyItem.CoolDown) {
                    CDComponent.SetActive(false);
                    CDText.text = "0%";
                    CDMask.transform.localScale = new Vector2(OriginalSize.x, 0);
                } else {
                    CDComponent.SetActive(true);
                    int percent = int.Parse(string.Format("{0:0}", MyItem.CurrentCoolDown / MyItem.CoolDown * 100f));
                    CDMask.transform.localScale = new Vector2(OriginalSize.x, OriginalSize.y * (1 - percent * 0.01f));
                    if (percent < 80 && percent % 5 == 0) {
                        CDText.text = string.Format("{0:0%}", MyItem.CurrentCoolDown / MyItem.CoolDown);
                    } else if (percent >= 80) {
                        CDText.text = string.Format("{0:0%}", MyItem.CurrentCoolDown / MyItem.CoolDown);
                    }
                }
            } else {
                CDComponent.SetActive(false);
            }
        }
    }

    private void OnMouseDown() {
        Timer = 0;
        if (Manager.CurrentItemGrid != this) {
            Manager.clearInfo();
            Manager.CurrentItemGrid = Manager.findItemGrid(MyItem);
            Manager.updateInfo();
            if (Manager.CurrentItemGrid.MyItem.Descarded == true) {
                Manager.RemoveButton.SetActive(true);
            }
            onSelectItem();
        }  
    }

    private void OnMouseDrag() {
        if(Timer >= StayTime) {
            if (DS.CurrentItem == null) {
                DS.show(this, "Drag to QuickSlot");
            }
        } else {
            Timer += Time.deltaTime;
        }
    }

    private void OnMouseExit() {
        Timer = 0;
    }

    private void OnMouseUp() {
        Timer = 0;
    }

    // 在UI中更新物品信息
    public void updateItemInfo(Item item, int totalCount) {
        if(item == null) {
            return;
        } else {
            MyItem = item;
            MyIcon.sprite = item.Icon.sprite;
            ItemCount = totalCount;
            ItemCountText.text = ItemCount.ToString();
        }        
    }
    // 添加物品
    public void onAddItem(Item item, int count) {
        updateItemInfo(item, ItemCount + count);
        item.Owner = Manager.Owner;
        item.onObtain();
        if (Manager.Owner.ItemList.Contains(item) == false) {
            Manager.Owner.ItemList.Add(item);
        }
    }
    // 使用物品
    public void onUseItem(Item item, int count = 1) {
        MyItem.onUse();
    }
    // 从物品格中删除物品
    public bool onRemoveItem(int count) {
        if (MyItem.Descarded == false) {
            return false;
        } else if (count > ItemCount) {
            return false;
        } else if (ItemCount - count == 0) {
            MyItem.onRemove();
            MyItem.Owner.ItemList.Remove(MyItem);
            Manager.ItemGridList.Remove(this);
            Manager.CurrentItemGridCount--;
            Destroy(LoadObj);
        } else {
            updateItemInfo(MyItem, ItemCount - count);
            MyItem.onRemove();
        }
        return true;
    }
    // 选中物品
    public void onSelectItem() {
        Selected = true;
        BackgroundImage.color = SelectedColor;
    }
    // 不再选中
    public void onCancelSelect() {
        Selected = false;
        BackgroundImage.color = UnselectedColor;
    }
}