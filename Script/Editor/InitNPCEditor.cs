/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     InitNPCEditor.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-06-02
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(InitNPC))]
public class InitNPCEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        InitNPC myScript = (InitNPC)target;
        if (GUILayout.Button("ADD ITEM")) {
            myScript.addItem();
        }
    }
}