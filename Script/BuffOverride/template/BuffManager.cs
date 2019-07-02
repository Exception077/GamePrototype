/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     BuffManager.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-07
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour {
    [SerializeField]
    List<BuffGrid> BGList;
    public GameCharacter Target;
    public BuffGrid BuffGridPrefab;
    public Transform BuffGrids;

	// Update is called once per frame
	void Update () {
	    for(int i = 0; i < Target.BuffList.Count; i++) {
            Target.BuffList[i].onKeep();
        }
	}

    /// <summary>
    /// 为目标添加BUFF
    /// </summary>
    /// <param name="buff">BUFF对象</param>
    public void addBuff(Buff buff) {
        buff.Target = Target;
        buff.onAbtain();
        Target.BuffList.Add(buff);
        BuffGrid bg = GameObject.Instantiate(BuffGridPrefab.gameObject, BuffGrids).GetComponent<BuffGrid>();
        bg.setInfo(buff);
        bg.gameObject.SetActive(true);
        BGList.Add(bg);
    }

    /// <summary>
    /// 清除指定的BUFF
    /// </summary>
    /// <param name="buff">BUFF对象</param>
    public void removeBuff(Buff buff) {
        foreach(BuffGrid bg in BGList) {
            if(bg.BUFF.Name == buff.Name) {
                BGList.Remove(bg);
                break;
            }
        }
        foreach(Buff b in Target.BuffList) {
            if(b.Name == buff.Name) {
                b.onRemove();
                Target.BuffList.Remove(b);
                return;
            }
        }
    }
}