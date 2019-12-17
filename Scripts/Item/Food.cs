using UnityEngine;
using System.Collections.Generic;

public class Food : Item {
    [SerializeField]
    float HungerValue;
    [SerializeField]
    [Range(0.1f,1f)]
    float FreshValue = 1;
    [SerializeField]
    TimeController TC;
    [SerializeField]
    float RotDuration;
    [SerializeField]
    [Range(0,1f)]
    float RotValue;
    float Timer;
    string str;

    public override void onObtain() {
        base.onObtain();
        Timer = TC.Minutes;
        CurrentCoolDown = CoolDown;
        str = Description;
    }

    public override bool onUse() {
        if (Owner.OnGround == false) {
            return false;
        }
        else if (CurrentCoolDown < CoolDown) {
            return false;
        }
        CurrentCoolDown = 0;
        if (Owner.CurrentHungerValue >= Owner.TotalHungerValue) {
            return false;
        }
        if (FreshValue <= 0) {
            Owner.CurrentHealth -= 50;
        }
        float cure = HungerValue * FreshValue;
        if (Owner.CurrentHungerValue + cure >= Owner.TotalHungerValue) {
            Owner.CurrentHungerValue = Owner.TotalHungerValue;
        } else {
            Owner.CurrentHungerValue += cure;
        }
        return base.onUse();
    }

    public override void onHold() {
        if (FreshValue <= 0) {
            FreshValue = 0;
            Name = "腐烂的野果";
            RotValue = 0;
            RotDuration = 1440;
            Description = "<color=red>看起来不像是可以充饥的样子。</color>";
        } else if(TC.Minutes - Timer >= RotDuration) {
            FreshValue -= RotValue;
            Timer = TC.Minutes;
            Description = str + string.Format("\n(Fresh:{0:P0})", FreshValue);
        }
        // CD
        if (CurrentCoolDown < CoolDown) {
            CurrentCoolDown += DeltaCoolDown * Time.deltaTime;
        } else {
            CurrentCoolDown = CoolDown;
        }
        base.onHold();
    }

    public override void setStatus(string state) {
        if (state == null) {
            return;
        }
        List<string> strs = new List<string>();
        while (state.Contains("#")) {
            if (state.Contains("|")) {
                strs.Add(state.Substring(state.IndexOf('#') + 1, state.IndexOf('|') - state.IndexOf('#') - 1));
                state = state.Substring(state.IndexOf('|') + 1);
            }
            else {
                strs.Add(state.Substring(state.IndexOf('#') + 1));
                state = "";
            }
        }
        QSID = strs[0];
        UseCount = int.Parse(strs[1]);
        CurrentCoolDown = int.Parse(strs[2]);
        FreshValue = float.Parse(strs[3]);
    }

    public override string getStatus() {
        return base.getStatus() + "|Fresh#" + FreshValue;
    }
}
