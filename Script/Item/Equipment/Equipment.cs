using UnityEngine;
using System.Collections.Generic;


public class Equipment : Item {
    public int TotalDurability;
    public int CurrentDurability;

    /// <summary>
    /// 置入装备槽时调用
    /// </summary>
    public virtual void goEquip() {

    }

    /// <summary>
    /// 在装备槽中调用
    /// </summary>
    public virtual void onEquip() {

    }

    /// <summary>
    /// 解除装备时调用
    /// </summary>
    public virtual void onRelease() {

    }
}
