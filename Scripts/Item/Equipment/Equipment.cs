using UnityEngine;
using System.Collections.Generic;


public class Equipment : Item {
    public int TotalDurability;
    public int CurrentDurability;
    public bool Enabled = true;

    /// <summary>
    /// 置入装备槽时调用
    /// </summary>
    public virtual void goEquip() {
        Owner.EquipmentList.Add(this);
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
        Owner.EquipmentList.Remove(this);
    }

    /// <summary>
    /// 受到伤害时调用
    /// </summary>
    /// <param name="damage"></param>
    public virtual void onHurt(ref float damage) {
    }
}
