/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     ItemManager.cs
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
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
struct ItemCache
{
    public string OperateName;
    public float CureTime;
    public float CurrentCureTime;
}

[System.Serializable]
public class ItemManager : MonoBehaviour {
    public ItemGrid GridPrefab;
    public ItemGrid CurrentItemGrid;
    public List<ItemGrid> ItemGridList = new List<ItemGrid>();
    public int CurrentItemGridCount = 0;
    [Space]
    public TextMeshProUGUI CurrentItemName;
    public TextMeshProUGUI CurrentItemDescription;
    public GameObject Grids;
    public GameObject RemoveButton;
    public GameObject UseButton;
    public TextMeshProUGUI UseButtonName;
    public GameObject ContentUI;
    public GameCharacter Owner;
    public UIManager UIController;
    [Space]
    public bool Available = true;
    public GameObject HealingLineComponent;
    public Image HealingLine;
    public TextMeshProUGUI OperateName;
    [SerializeField]
    Vector2 TotalLine;
    [SerializeField]
    ItemCache MyItemCache;
    [Header("Mode")]
    public bool LoadMode;

    private void Start() {
        TotalLine = HealingLine.rectTransform.sizeDelta;
    }

    public void Update() {
        if(CurrentItemGrid == null) {
            clearInfo();
        } else {
            RemoveButton.SetActive(CurrentItemGrid.MyItem.Descarded);
            UseButton.SetActive(CurrentItemGrid.MyItem.IsTool && Available);
            UseButtonName.text = CurrentItemGrid.MyItem.OperateName;
        }
        HealingLineComponent.SetActive(!Available);
        if (Available == false) {
            if (MyItemCache.CurrentCureTime >= MyItemCache.CureTime) {
                Available = true;
            }
            else {
                MyItemCache.CurrentCureTime += Time.deltaTime;
                OperateName.text = MyItemCache.OperateName;
                HealingLine.rectTransform.sizeDelta = new Vector2(TotalLine.x * MyItemCache.CurrentCureTime / MyItemCache.CureTime, TotalLine.y);
            }
        }
    }

    // 对当前选中的物品进行删除操作
    public void removeCurrentItemGrid() {
        removeItem(CurrentItemGrid, 1);
    }
    // 使用当前选中的物品
    public void useCurrentItemGird() {
        useItem(CurrentItemGrid, 1);
    }
    // 创建一个格子
    public GameObject createGrid(ItemGrid grid) {
        return GameObject.Instantiate(grid.LoadObj, Grids.transform);
    }
    // 判断物品是否已存在
    public bool itemExist(Item item) {
        for(int i = 0; i < ItemGridList.Count; i++) {
            if(ItemGridList[i].MyItem == item) {
                return true;
            }
        }
        return false;
    }
    // 查找物品格子
    public ItemGrid findItemGrid(Item item) {
        return ItemGridList.Find(delegate (ItemGrid ig)
        {
            return ig.MyItem == item;
        });
    }
    // 添加物品
    public void addItem(Item item, int count) {
        if(itemExist(item) == true) {
            findItemGrid(item).onAddItem(item, count);
        } else {
            ItemGridList.Add(createGrid(GridPrefab).GetComponent<ItemGrid>());
            ItemGridList[CurrentItemGridCount].onAddItem(item, count);
            CurrentItemGridCount++;          
        }
        if (!LoadMode) {
            MessageBoard.Instance.generateMessage("获得：<color=#00FF00>" + item.Name + "</color> × " + count);
        }
    }
    // 删除物品
    public void removeItem(ItemGrid grid, int count) {
        MessageBoard.Instance.generateMessage("删除：<color=#00FF00>" + grid.MyItem.Name + "</color> × " + count);
        grid.onRemoveItem(count);
        if(grid.ItemCount <= 0) {
            clearInfo();
        }
    }
    // 使用物品
    public void useItem(ItemGrid grid, int count) {
        if(grid.MyItem.UseCount < count) {
            return;
        } else if(grid.MyItem.CurrentCoolDown < grid.MyItem.CoolDown) {
            MessageBoard.Instance.generateMessage("<color=#FF0000>" + grid.MyItem.Name + "</color>冷却中...");
        } else if(grid.MyItem.Unlimited == true) {
            if (grid.MyItem.onUse()) {
                MyItemCache.OperateName = grid.MyItem.OperateName;
                MyItemCache.CureTime = grid.MyItem.CureTime;
                MyItemCache.CurrentCureTime = 0;
                Available = !(grid.MyItem.CureTime > 0);
            }
            return;
        } else {
            MyItemCache.CurrentCureTime = 0;
            MyItemCache.OperateName = grid.MyItem.OperateName;
            grid.MyItem.UseCount -= count;
            for (int i = 0; i < count; i++) {
                if (grid.MyItem.onUse()) {
                    MyItemCache.CureTime += grid.MyItem.CureTime;
                    Available = !(grid.MyItem.CureTime > 0);
                }
            }
        }
        if(grid.MyItem.UseCount == 0) {
            removeItem(grid, 1);
        }
    }
    // 清空背包
    public void clearItem() {
        for(int i = 0; i < ItemGridList.Count; i++) {
            ItemGridList[i].onRemoveItem(ItemGridList[i].ItemCount);
        }
        ItemGridList.Clear();
    }
    // 交易物品
    public void dealWith(GameCharacter target, Item item) {
        target.ItemList.Add(item);
        Owner.ItemList.Remove(item);
    }
    // 更新UI信息
    public void updateInfo() {
        ContentUI.SetActive(true);
        CurrentItemName.text = CurrentItemGrid.MyItem.Name;
        CurrentItemDescription.text = CurrentItemGrid.MyItem.Description;
    }
    // 清除UI信息
    public void clearInfo() {
        for(int i = 0; i < ItemGridList.Count; i++) {
            if(ItemGridList[i].Selected == true) {
                ItemGridList[i].onCancelSelect();
            }
        }
        CurrentItemName.text = "";
        CurrentItemDescription.text = "";
        CurrentItemGrid = null;
        ContentUI.SetActive(false);
    }
}