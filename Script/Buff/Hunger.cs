using UnityEngine;
using System.Collections;

public class Hunger : Slow {
    [SerializeField]
    float HungerHurt;
    [SerializeField]
    TimeController TC;
    int timeOnHold;

    public override void onAbtain() {
        base.onAbtain();
        Description = "����������ֵ�����½�����Ҫ�����ʳ<color=red>(" + HungerHurt + "Damage/Min)";
        timeOnHold = TC.Minutes;
    }

    public override void onKeep() {
        base.onKeep();
        if (TC.Minutes - timeOnHold >= 1) {
            Target.CurrentHealth -= HungerHurt;
            timeOnHold = TC.Minutes;
        }
        if(Target.CurrentHungerValue > 0) {
            onRemove();
        }
    }

    public override void onRemove() {
        base.onRemove();
    }
}
