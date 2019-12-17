using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhausted : Buff
{
    [SerializeField]
    float EnergyOnHold;
    [SerializeField]
    float EnergyOnChanged;
    [SerializeField]
    float Damage;

    public override void onAbtain() {
        base.onAbtain();
        Description = "衰竭，当消耗体力值时受到伤害<color=red>(" + Damage + "Damage/Energy)</color>";
        EnergyOnHold = Target.CurrentEnergy;
        EnergyOnChanged = EnergyOnHold;
    }

    public override void onKeep() {
        base.onKeep();
        EnergyOnChanged = Target.CurrentEnergy;
        if (EnergyOnChanged < EnergyOnHold) {
            if (Target.Eternal) {
                return;
            }
            Target.CurrentHealth -= Damage * (EnergyOnHold - EnergyOnChanged);
            //Target.getHurt(DamageDegree.LIGHT_ATTACK, Damage * (EnergyOnHold - EnergyOnChanged), DamageType.MAGIC);
            EnergyOnHold = Target.CurrentEnergy;
        } else {
            EnergyOnHold = Target.CurrentEnergy;
        }
    }

    public override string getStatus() {
        return "Damage#" + Damage;
    }

    public override void setStatus(string state) {
        if (state == null) {
            return;
        }
        Damage = float.Parse(state.Substring(state.IndexOf("#") + 1));
    }
}
