/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     NPC.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-15
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class NPC : GameCharacter {
    [Space]
    public Animator Anim;
    // 初始位置
    public Vector2 InitPos;
    // 地面检测变量
    [Header("Ground check settings")]
    public float GroundCheckRadius;
    public Transform GroundCheck;
    public LayerMask GroundLayer;
    // 运动控制变量
    [Header("Move settings")]
    public float JumpForce;
    protected float WalkingDuration;
    protected bool Deriction;   
    public bool IsFacingRight;
    // NPC简介
    [Header("Npc description")]
    public string Description;
    public GameCharacter Target;
    [Header("AI")]
    public AI MyAI;
    /// <summary>
    /// 死亡
    /// </summary>
    public override void goToDead() {
        Alive = false;
        Anim.SetBool("dead", true);
        CurrentState = StateEnum.DEAD;
        GameCharacterManager.Instance.removeNPC(this); 
        Invoke("removeObj", 3.0f);
    }
    /// <summary>
    /// 销毁对象
    /// </summary>
    public void removeObj() {
        LoadObj.SetActive(false);
        GameCharacterManager.Instance.killCharacter(this);
        GameCharacterManager.Instance.removeNPC(this);
    }
    /// <summary>
    /// 复活
    /// </summary>
    public override void revive() {
        transform.position = InitPos;
        CurrentHealth = TotalHealth;
        Alive = true;
        CurrentState = StateEnum.IDLE;
        LoadObj.SetActive(true);
        if(GameCharacterManager.Instance.NPCList.Contains(this) == false) {
            GameCharacterManager.Instance.addNPC(this);
        }
    }
    /// <summary>
    /// 受到攻击
    /// </summary>
    /// <param name="damageType">伤害类型</param>
    /// <param name="damage">伤害数值</param>
    public override void getHurt(DamageEnum damageType, float damage) {
        base.getHurt(damageType, damage);
        CurrentState = StateEnum.HURT;
        Anim.SetBool("hurt", true);
        switch (damageType) {
            case DamageEnum.LIGHT_ATTACK:
                Anim.SetBool("lightHurt", true);
                Invoke("finishLightHurt", 0.5f);
                break;
            case DamageEnum.HEAVY_ATTACK:
                Anim.SetBool("heavyHurt", true);
                Invoke("finishHeavyHurt", 0.5f);
                break;
            case DamageEnum.STRIKE_FLY:
                Anim.SetBool("strikeFly", true);
                Invoke("finishStrikeFly", 0.5f);
                break;
            default:
                break;
        }
    }
    private void finishLightHurt() {
        Rbody2D.velocity = new Vector2(0, 0);
        Anim.SetBool("lightHurt", false);
        Anim.SetBool("hurt", false);
        CurrentState = StateEnum.IDLE;
    }
    private void finishHeavyHurt() {
        Rbody2D.velocity = new Vector2(0, 0);
        Anim.SetBool("heavyHurt", false);
        Anim.SetBool("hurt", false);
        CurrentState = StateEnum.IDLE;
    }
    private void finishStrikeFly() {
        Rbody2D.velocity = new Vector2(0, 0);
        Anim.SetBool("strikeFly", false);
        Anim.SetBool("hurt", false);
        CurrentState = StateEnum.IDLE;
    }
    /// <summary>
    /// 更新状态
    /// </summary>
    /// <param name="state">状态枚举</param>
    public virtual void updateState(StateEnum state) {

    }
    /// <summary>
    /// 地面检测
    /// </summary>
    /// <returns>返回检测结果</returns>
    public virtual bool checkGround() {
        OnGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
        Anim.SetBool("onGround", OnGround);
        return OnGround;
    }
    /// <summary>
    /// 水平翻转
    /// </summary>
    public virtual void flip() {
        IsFacingRight = !IsFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    /// <summary>
    /// 向Animator同步速度参数
    /// </summary>
    public virtual void checkSpeed() {
        Anim.SetFloat("speed", Mathf.Abs(Rbody2D.velocity.x));
        Anim.SetFloat("vSpeed", Rbody2D.velocity.y);
    }
    /// <summary>
    /// 更新朝向
    /// </summary>
    /// <param name="on"></param>
    public virtual void updateFacing(bool on) { }
    /// <summary>
    /// 向某一方向运动
    /// </summary>
    /// <param name="right"></param>
    public virtual void move(bool right, float speed) { }
    /// <summary>
    /// 在一定时间内运动
    /// </summary>
    /// <param name="time">运动持续时间</param>
    public virtual void move(float time, float speed) { }
    /// <summary>
    /// 在一定时间内向某一方向运动
    /// </summary>
    /// <param name="right">朝向</param>
    /// <param name="time">时间</param>
    public virtual void move(bool right, float speed, float time) { }
    public virtual void stop() {

    }
    public virtual void jump() {

    }
    /// <summary>
    /// 检测攻击结果
    /// </summary>
    /// <param name="target">目标</param>
    /// <returns>若为true则攻击生效</returns>
    public override bool checkHit(GameCharacter target) { return true; }
    // 进行攻击
    // 准备攻击
    public virtual void readyToAttack(float stayTime) { }
    /// <summary>
    /// 限定类型、伤害数值
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="damageType">伤害类型</param>
    /// <param name="damage">伤害数值</param>
    public virtual void attack(GameCharacter target, DamageEnum damageType, float damage) { }
    /// <summary>
    /// 随机攻击
    /// </summary>
    /// <param name="target">目标</param>
    public virtual void attack(GameCharacter target) { }
    /// <summary>
    /// 获得伤害类型
    /// </summary>
    /// <returns>伤害类型枚举</returns>
    public virtual DamageEnum getDamageType() { return DamageEnum.LIGHT_ATTACK; }
    // 添加攻击效果
    /// <summary>
    /// 限定效果
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="skill">攻击方式</param>
    public virtual void setEffect(GameCharacter target, AttackEnum skill) { }
    /// <summary>
    /// 随机效果
    /// </summary>
    /// <param name="target">目标</param>
    public virtual void setEffect(GameCharacter target) { }
    /// <summary>
    /// 获得攻击效果
    /// </summary>
    /// <returns>攻击类型枚举</returns>
    public virtual AttackEnum getAttackType() { return AttackEnum.NORMAL_ATTACK; }
}