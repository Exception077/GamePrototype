using UnityEngine;
using System.Collections;

// 基础型防具
public class ETemplateB0 : Equipment {
    [SerializeField]
    protected int PhysicDefenceValue;
    [SerializeField]
    protected int MagicDefenceValue;
    [SerializeField]
    protected int ShieldValue;
    string tempname;
    bool tempenabled;

    private void Start() {
        EquipType = EquipType.BODY;
        tempname = Name;
    }

    public override void goEquip() {
        base.goEquip();
        tempenabled = CurrentDurability > 0;
        Owner.PhysicDefence += (int)(PhysicDefenceValue * (Enabled ? 1.0 : 0.1));
        Owner.MagicDefence += (int)(MagicDefenceValue * (Enabled ? 1.0 : 0.1));
        Owner.TotalShieldValue += ShieldValue * (Enabled ? 1.0f : 0.1f);
        Owner.CurrentShieldValue += ShieldValue * (Enabled ? 1.0f : 0.1f);
    }

    public override void onEquip() {
        base.onEquip();
        if (CurrentDurability <= 0) {
            Name = tempname + "<color=red>(corrupted)</color>";
            Owner.PhysicDefence -= (int)(PhysicDefenceValue * (tempenabled ? 0.9 : 0));
            Owner.MagicDefence -= (int)(MagicDefenceValue * (tempenabled ? 0.9 : 0));
            //Owner.TotalShieldValue -= Mathf.Round(ShieldValue * (tempenabled ? 0.9f : 0f));
            //Owner.CurrentShieldValue -= Mathf.Round(ShieldValue * (tempenabled ? 0.9f : 0f));
            tempenabled = false;
            Enabled = false;
        } else {
            Name = tempname;
            Owner.PhysicDefence = PhysicDefenceValue;
            Owner.MagicDefence = MagicDefenceValue;
            //Owner.TotalShieldValue = ShieldValue;
            //Owner.CurrentShieldValue = ShieldValue;
            tempenabled = true;
            Enabled = true;
        }  
    }

    public override void onRelease() {
        print("ET");
        Owner.PhysicDefence -= (int)(PhysicDefenceValue * (Enabled ? 1.0f : 0.1f));
        Owner.MagicDefence -= (int)(MagicDefenceValue * (Enabled ? 1.0f : 0.1f));
        Owner.TotalShieldValue -= Mathf.Round(ShieldValue * (Enabled ? 1.0f : 0.1f));
        Owner.CurrentShieldValue -= Mathf.Round(ShieldValue * (Enabled ? 1.0f : 0.1f));
    }
}
