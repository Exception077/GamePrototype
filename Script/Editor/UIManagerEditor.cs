/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     UIManagerEditor.cs
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
[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        UIManager myScript = (UIManager)target;
        if(GUILayout.Button("OPEN ALL DIALOG")) {
            foreach(Dialog d in myScript.DialogList) {
                myScript.openDialog(d);
            }
        }
        if(GUILayout.Button("CLOSE ALL DIALOG")) {
            foreach(Dialog d in myScript.DialogList) {
                myScript.closeDialog(d);
            }
        }
    }
}