/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     TradeManager.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-31
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;

public struct Goods{
    [SerializeField]
    public Item ItemObj;
    public int Price;
}
public class TradeManager : MonoBehaviour {

    /// <summary>
    /// 交易
    /// </summary>
    /// <param name="seller">卖家</param>
    /// <param name="buyer">买家</param>
    /// <param name="goods">货物</param>
    /// <param name="count">数量</param>
    /// <returns></returns>
	public bool dealWith(GameCharacter seller, GameCharacter buyer, Goods goods, int count) {
        if (buyer.Coins >= goods.Price * count && seller.Bag.findItemGrid(goods.ItemObj).ItemCount >= count) {
            // 钱足够&&货物足够
            // 买家购入
            buyer.Bag.addItem(goods.ItemObj, count);
            buyer.Coins -= goods.Price * count;
            // 卖家售出
            seller.Bag.removeItem(seller.Bag.findItemGrid(goods.ItemObj), count);
            seller.Coins += goods.Price * count;
            return true;
        } else {
            return false;
        }
    }
}