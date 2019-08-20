/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Useless.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-09
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;

public class Useless : Buff {

    public override void onAbtain() {
        base.onAbtain();
        //Target.Eternal = true;
    }

    public override void onRemove() {
        base.onRemove();
        //Target.Eternal = false;
    }
}