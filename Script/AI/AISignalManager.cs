/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     AISignalManager.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-18
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class AISignalManager : MonoBehaviour {
    public List<AISignal> SignalList;

    public bool caught() {
        foreach(AISignal signal in SignalList) {
            if (signal.Caught) {
                return true;
            }
        }
        return false;
    }   
}