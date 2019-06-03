/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     CharacterManager.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-06-02
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {
    public GameCharacter Character;

    public void saveData() {
        Character.Operator.DataAssets.Path = Application.dataPath + "/Save/" + Character.CharacterName + "Data.txt";
        Character.saveData();
    }

    public void loadDta() {
        if(Character.GetType() == typeof(Player)) {
            print("This function is only designed for NPC entity.");
            return;
        }
        Character.Operator.DataAssets.Path = Application.dataPath + "/Save/" + Character.CharacterName + "Data.txt";
        Character.loadData();
    }
}