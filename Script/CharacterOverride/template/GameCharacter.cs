/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     GameCharacter.cs
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
[System.Serializable]
public enum StateEnum
{
    IDLE, WALK, RUN, SPEEL, CROUCH, JUMP, DEAD, ALIVE, ATTACK, HURT, DASH, ROLL, HEALING
}
public enum AttackEnum
{
    NORMAL_ATTACK, JUMP_ATTACK, SPEEL_ATTACK
}
public enum DamageEnum
{
    LIGHT_ATTACK, HEAVY_ATTACK, STRIKE_FLY
}
[System.Serializable]
public class GameCharacter : MonoBehaviour {
    [Header("GameCharacter info")]
    public string CharacterName;
    public float CurrentHealth;
    public float TotalHealth;
    public float CurrentEnergy;
    public float TotalEnergy;
    public float CurrentHungerValue;
    public float TotalHungerValue;
    public float EnergyCure;
    public GameObject LoadObj;
    public Rigidbody2D Rbody2D;
    [Header("Property")]
    public int Coins;
    public List<Item> ItemList;
    public List<ItemLoadIndex> ItemReference;
    public ItemManager Bag;
    public BuffStock BuffStock;
    [Header("Attack settings")]
    public float BasicDamage;
    public Collider2D AttackRange;
    public LayerMask EnemyLayer;
    public float CoolDown;
    public float CurrentCoolDown;
    [Header("Move settings")]
    public float CurrentSpeed;
    public float WalkingSpeed;
    public float RunningSpeed;
    public float RunningCost = 0;
    [Header("State")]
    public bool OnGround;
    public bool Eternal;
    public bool Locked;
    public bool Alive;
    public StateEnum CurrentState;
    public List<Buff> BuffList;
    public BuffManager BM;
    [Header("Impression")]
    public Relation CharacterRelation;
    [Header("Data manager")]
    public DataOperator Operator;
    // 检测是否存活
    public virtual bool checkAlive() {
        Alive = CurrentHealth > 0;
        return Alive;
    }
    // 死亡，不再复活
    public virtual void goToDead() {
        LoadObj.SetActive(false);
    }
    //死亡，一定时间后复活
    public virtual void goToDead(Vector2 pos, float time) {
        LoadObj.SetActive(false);
        LoadObj.transform.position = pos;
        Invoke("revive", time);
    }
    //复活，默认满血
    public virtual void revive() {
        if(Alive == false) {
            LoadObj.SetActive(true);
            Alive = true;
            CurrentHealth = TotalHealth;
        }
    }
    // 单次攻击
    public virtual bool hit(GameCharacter character, DamageEnum damageType, float damage) {
        if (checkHit(character)) {
            character.getHurt(damageType, damage);
            character.checkAlive();
            return true;
        } else {
            return false;
        }
    }
    // 受到攻击
    public virtual void getHurt(DamageEnum damageType, float damage) {
        if (!Eternal) {
            CurrentHealth -= damage;
        }
    }
    // 检测攻击结果
    public virtual bool checkHit(GameCharacter character) {
        return true;
    }

    public virtual void saveData() { }

    public virtual void loadData() { }

    public virtual void healing(float time) {

    }
    /// <summary>
    /// 对话
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="stayTime">持续时间</param>
    public virtual void startChat(string content, float stayTime) {
    }
    /// <summary>
    /// 结束对话
    /// </summary>
    protected virtual void finishChat() {

    }
}