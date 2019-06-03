/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     CharacterEditor.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-06-02
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
[CustomEditor(typeof(CharacterManager))]
public class CharacterEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        CharacterManager myScript = (CharacterManager)target;
        // 角色名
        EditorGUILayout.LabelField("Name:",myScript.Character.CharacterName);
        // 绘制血条
        GUI.color = Color.green;
        Rect progressRect0 = GUILayoutUtility.GetRect(50, 17);
        EditorGUI.ProgressBar(progressRect0, myScript.Character.CurrentHealth/myScript.Character.TotalHealth, "HEALTH:" + myScript.Character.CurrentHealth + "/" + myScript.Character.TotalHealth);
        // 绘制精力条
        GUI.color = new Color(255, 250, 250, 255);
        Rect progressRect1 = GUILayoutUtility.GetRect(50, 17);
        EditorGUI.ProgressBar(progressRect1, myScript.Character.CurrentEnergy / myScript.Character.TotalEnergy, "ENERGY:" + myScript.Character.CurrentEnergy + "/" + myScript.Character.TotalEnergy);
        GUI.color = Color.white;
        // 空行
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Tools");
        if (GUILayout.Button("GENERATE DATA")) {
            myScript.saveData();
            Debug.Log("Generate data:" + myScript.Character.Operator.DataAssets.Path);
        }
        if(GUILayout.Button("LOAD DATA")) {
            myScript.loadDta();
        }
    }
}