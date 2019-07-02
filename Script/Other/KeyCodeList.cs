/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Key.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2018-07-21
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
public class KeyCodeList
{

    private static KeyCodeList MyInstance;
    public KeyCode Attack = KeyCode.X;
    public KeyCode Speel = KeyCode.C;
    public KeyCode Roll = KeyCode.C;
    public KeyCode Interactive = KeyCode.F;
    public KeyCode Playback = KeyCode.R;
    public KeyCode Jump = KeyCode.Space;
    public KeyCode Dash = KeyCode.D;
    public KeyCode ReadyToAttack = KeyCode.Q;
    public KeyCode Left = KeyCode.LeftArrow;
    public KeyCode Right = KeyCode.RightArrow;
    public KeyCode Bag = KeyCode.B;
    public KeyCode Menu = KeyCode.Escape;

    public static KeyCodeList Instance
    {
        get
        {
            if (MyInstance == null)
            {
                MyInstance = new KeyCodeList();
            }
            return MyInstance;
        }
    }
}