/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Trap.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-15
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Trap : MonoBehaviour {
    public Collider2D Range;
    public Rigidbody2D Rbody2D;
    public LayerMask CharacterLayer;
    public List<GameCharacter> CharacterList = new List<GameCharacter>();
    public float DamageNumber;
    public bool Death;
	// Use this for initialization
	void Start () {
        Rbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        effect();
	}

    public virtual bool checkRange(GameCharacter character) {
        if(Physics2D.OverlapArea(Range.bounds.min,Range.bounds.max,CharacterLayer).gameObject == character.LoadObj) {
            return true;
        } else {
            return false;
        }
    }
    // 陷阱生效
    public virtual void effect() {
        for(int i = 0;i < GameCharacterManager.Instance.NPCList.Count; i++) {
            if (checkRange(GameCharacterManager.Instance.NPCList[i])) {
                hit(GameCharacterManager.Instance.NPCList[i], Death);
            }
        }
        for (int i = 0; i < GameCharacterManager.Instance.EnemyList.Count; i++) {
            if (checkRange(GameCharacterManager.Instance.EnemyList[i])) {
                hit(GameCharacterManager.Instance.NPCList[i], Death);
            }
        }
        for (int i = 0; i < GameCharacterManager.Instance.PlayerList.Count; i++) {
            if (checkRange(GameCharacterManager.Instance.PlayerList[i])) {
                hit(GameCharacterManager.Instance.NPCList[i], Death);
            }
        }
    }
    // 监听角色
    public void listen() {
        for(int i = 0; i < CharacterList.Count; i++) {
            if (checkRange(CharacterList[i]) == false) {
                // 角色离开陷阱范围后将角色移出监听列表
                CharacterList.RemoveAt(i);
            }
        }
    }
    // 造成伤害
    public void dealDamage(GameCharacter character ,bool death = false) {
        if(death == false) {
            // 造成固定伤害
            character.CurrentHealth -= DamageNumber;
            character.checkAlive();
        } else {
            // 即死
            character.CurrentHealth = 0;
            character.checkAlive();
        }
    }
    // 对角色造成伤害
    public void hit(GameCharacter character, bool death = false) {
        if(CharacterList.Contains(character) == false) {
            dealDamage(character, death);
            CharacterList.Add(character);
        }
    }
}