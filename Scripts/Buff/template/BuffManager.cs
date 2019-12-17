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
        if (buff == null) {
            return;
        }
        foreach (Buff b in Target.BuffList) {
            if (b.ID == buff.ID) {
                removeBuff(b);
                break;
            }
        }
        buff.Target = Target;
        buff.onAbtain();
        Target.BuffList.Add(buff);
        BuffGrid bg = GameObject.Instantiate(BuffGridPrefab.gameObject, BuffGrids).GetComponent<BuffGrid>();
        bg.setInfo(buff);
        bg.gameObject.SetActive(true);
        BGList.Add(bg);
    }

    public bool containsBuff(string id) {
        foreach (BuffGrid b in BGList) {
            if (b.BUFF.ID == id) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 清除指定的BUFF
    /// </summary>
    /// <param name="buff">BUFF对象</param>
    public void removeBuff(Buff buff) {
        foreach(BuffGrid bg in BGList) {
            if(bg.BUFF.ID == buff.ID) {
                BGList.Remove(bg);
                bg.onRemove();
                break;
            }
        }
        foreach(Buff b in Target.BuffList) {
            if(b.ID == buff.ID) {
                b.onRemove();
                Target.BuffList.Remove(b);
                return;
            }
        }
    }

    public void clearBuff() {
        foreach (BuffGrid bg in BGList) {
            bg.BUFF.onRemove();
            bg.onRemove();
        }
        BGList.Clear();
        Target.BuffList.Clear();
    }
}