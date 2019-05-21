/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Slow.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-07
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;

public class Slow : Buff {
    [SerializeField]
    [Range(0, 1.0f)]
    float SlowPower;

    public override void onAbtain() {
        base.onAbtain();
        Target.WalkingSpeed *= SlowPower;
        Target.RunningSpeed *= SlowPower;
    }

    public override void onKeep() {
        base.onKeep();
    }

    public override void onRemove() {
        base.onRemove();
        Target.WalkingSpeed /= SlowPower;
        Target.RunningSpeed /= SlowPower;
    }
}