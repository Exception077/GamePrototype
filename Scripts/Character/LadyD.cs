/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     LadyD.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-16
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class LadyD : NPC {
    public Collider2D MisstepRange;
    public LayerMask TargetLayer;
    bool IsJumping;
    // Use this for initialization
    void Start () {
        Operator.DataAssets.Path = Application.dataPath + "/Save/" + CharacterName + "Data.txt";
        loadData();
        #region init
        Locked = false;
        IsFacingRight = true;
        CurrentSpeed = WalkingSpeed;
        InitPos = transform.position;
        Anim = GetComponent<Animator>();
        Rbody2D = GetComponent<Rigidbody2D>();
        Alive = true;
        //CurrentHealth = TotalHealth;
        CurrentCoolDown = CoolDown;
        GameCharacterManager.Instance.removeNPC(this);
        GameCharacterManager.Instance.addNPC(this);
        #endregion
    }
	// Update is called once per frame
	void Update () {
        checkAlive();
        checkGround();
        checkSpeed();
        Anim.SetFloat("VSpeed", Rbody2D.velocity.y);
        if (OnGround) {
            IsJumping = false;
        }
        if (Alive == false) {
            goToDead();
        }
        if(CurrentCoolDown < CoolDown) {
            CurrentCoolDown += Time.deltaTime;
        } else if(CurrentCoolDown > CoolDown) {
            CurrentCoolDown = CoolDown;
        }
        // go to flip
        #region go to flip
        if(!Locked) {
            int right = IsFacingRight ? 1 : -1;
            if (right * Rbody2D.velocity.x < 0) {
                flip();
            }
        }
        #endregion
    }

    public override void saveData() {
        DataForNPC npcdata = new DataForNPC();
        npcdata.Path = Operator.DataAssets.Path;
        npcdata.CurrentHealth = CurrentHealth;
        npcdata.TotalHealth = TotalHealth;
        npcdata.CurrentEnergy = CurrentEnergy;
        npcdata.TotalEnergy = TotalEnergy;
        npcdata.Coins = Coins;
        // 清除物品索引列表
        npcdata.ItemReference.Clear();
        // 更新物品索引列表
        for (int i = 0; i < ItemList.Count; i++) {
            npcdata.addItemIndex(ItemList[i].ID, Bag.findItemGrid(ItemList[i]).ItemCount, ItemList[i].getStatus());
        }
        // 更新DataAssets
        Operator.DataAssets = npcdata;
        Operator.saveData();
        print("SaveData|Path:" + Operator.DataAssets.Path);
#if UNITY_EDITOR
#else
        try {
            MessageBoard.Instance.generateMessage("Save Data...");
        } catch(Exception e) {
            print(e.Message);
        }
#endif
    }

    public override void loadData() {
        // 声明临时变量存放数据
        Data data = new Data();
        DataForNPC npcdata = new DataForNPC();
        // 从DataAssets获取数据至playerdata
        Operator.loadData(out data);
        npcdata = (DataForNPC)data;
        // 同步数据至人物
        TotalHealth = npcdata.TotalHealth;
        CurrentHealth = npcdata.CurrentHealth;
        TotalEnergy = npcdata.TotalEnergy;
        CurrentEnergy = npcdata.CurrentEnergy;
        Coins = npcdata.Coins;
        ItemReference = npcdata.ItemReference;
        if(CurrentHealth <= 0) {
            LoadObj.SetActive(false);
        }
        // 清空背包
        Bag.clearItem();
        // 更新背包
        Bag.LoadMode = true;
        for (int i = 0; i < npcdata.ItemReference.Count; i++) {
            Bag.addItem(ItemStock.Instance.getItemByID(npcdata.ItemReference[i].ID), npcdata.ItemReference[i].Count);
        }
        Bag.LoadMode = false;
        Operator.DataAssets.Path = Application.dataPath + "/Save/" + CharacterName + "Data.txt";
    }

    // 更新状态
    public override void updateState(StateEnum state) {
        base.updateState(state);
        CurrentState = state;
        switch (state) {
            case StateEnum.WALK:
                break;
            case StateEnum.JUMP:
                break;
            default:
                break;
        }
    }
    // 受伤
    public override void getHurt(DamageDegree damageDegree, float damage, DamageType damageType) {
        base.getHurt(damageDegree, damage, damageType);
        Locked = true;
        Invoke("cure", 0.5f);
        MyAI.setState(AIState.HURT);
    }
    private void cure() {
        Locked = false;
    }
    // 更新朝向
    public override void updateFacing(bool on) {
        if(on == true) {
            Anim.SetBool("flip", true);
        } else {
            Anim.SetBool("flip", false);
        }
    }
    // 停止移动
    public override void stop() {
        Rbody2D.velocity = new Vector2(0, 0);
    }
    // 向某个方向移动
    public override void move(bool right, float speed) {
        base.move(right, speed);
        CurrentSpeed = speed;
        int deriction = 0;
        if (right == true) {
            deriction = 1;
        } else {
            deriction = -1;
        }
        if (IsJumping) {
            Rbody2D.velocity = new Vector2(deriction * CurrentSpeed, Rbody2D.velocity.y);
            CurrentState = StateEnum.JUMP;
        } else if (isMisstep() == true && CurrentState != StateEnum.HURT) {
            Rbody2D.velocity = new Vector2(0, 0);
            print("misstep");
        } else if (OnGround && CurrentState != StateEnum.HURT) {
            Rbody2D.velocity = new Vector2(deriction * CurrentSpeed, Rbody2D.velocity.y);
            CurrentState = StateEnum.WALK;
        }
    }
    // 随机移动一段时间
    public override void move(float time, float speed) {
        base.move(time,speed);
        if(!Locked) {
            if (WalkingDuration <= time) {
                if (WalkingDuration - time % 1 == 0) {
                    Deriction = getRandomDeriction();
                }
                move(Deriction, speed);
                WalkingDuration += Time.deltaTime;
            } else {
                Rbody2D.velocity = new Vector2(0, 0);
            }
        }
        
    }
    // 向某一方向移动
    public override void move(bool right, float speed, float time) {
        base.move(right, speed, time);
        if (!Locked) {
            if (WalkingDuration <= time) {
                move(right, speed);
                WalkingDuration += Time.deltaTime;
            } else {
                Rbody2D.velocity = new Vector2(0, 0);
            }
        }
    }
    // 获取随机方向
    public bool getRandomDeriction() {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int d = UnityEngine.Random.Range(0, 2);
        if (d == 0) {
            return false;
        } else {
            return true;
        }
    }
    // 跳跃
    public override void jump() {
        base.jump();
        if (OnGround == true) {
            OnGround = false;
            Rbody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Force);
            print("LadyD jump:" + Physics2D.gravity.y);
            IsJumping = true;
        } 
    }
    // 攻击
    public override bool hit(GameCharacter character, DamageDegree damageDegree, float damage, DamageType type) {
        return base.hit(character, damageDegree, damage, type);
        character.Locked = true;
    }
    // 攻击检测
    public override bool checkHit(GameCharacter character) {
        Bounds bounds = AttackRange.bounds;
        Collider2D[] results = new Collider2D[20];
        ContactFilter2D tempFilter = new ContactFilter2D();
        tempFilter.SetLayerMask(TargetLayer);
        Physics2D.OverlapArea(bounds.min, bounds.max, tempFilter, results);
        if (Physics2D.OverlapArea(bounds.min, bounds.max, TargetLayer)) {
            return true;
            for (int i = 0; i < 20; i++) {
                if (results[i].gameObject == character.LoadObj) {
                    Debug.Log("inRange");
                    return true;
                }
            }
        }
        return false;
    }
    // 进行攻击
    // 准备攻击
    public override void readyToAttack(float stayTime) {
        base.readyToAttack(stayTime);
        if (!Locked) {
            startChat("<color=#FF0000>Ready ro attack</color>", stayTime);
            Locked = true;
            Invoke("attackCb", stayTime);
            //print("attack");
        } 
    }
    private void attackCb() {
        attack(Target);
        setEffect(Target);
    }
    // 限定类型、伤害数值
    public override void attack(GameCharacter target, DamageDegree damageType, float damage) {
        hit(target, damageType, damage, MyDamageType);
    }
    // 随机攻击（普通攻击）
    public override void attack(GameCharacter target) {
        if(CurrentCoolDown == CoolDown) {
            if (checkHit(target)) {
                hit(target, getDamageType(), BasicDamage, MyDamageType);
            }
            stop();
            Locked = true;
            Invoke("cure", 1.0f);
            Anim.Play("ladyDNormalAttack");
            CurrentCoolDown = 0;
            Target = target;
            Invoke("finishAttack", 0.5f);
        } 
    }
    // 结束攻击动作
    private void finishAttack() {
        Anim.SetBool("attack", false);
        Locked = false;
        Target.Locked = false;
    }
    // 获得伤害类型
    public override DamageDegree getDamageType() {
        // 该角色只会造成一种类型的伤害
        return DamageDegree.HEAVY_ATTACK;
    }
    // 添加攻击效果
    // 限定效果
    public override void setEffect(GameCharacter target, AttackEnum skill) {
        switch (skill) {
            case AttackEnum.NORMAL_ATTACK:
                if (IsFacingRight == true) {
                    target.Rbody2D.AddForce(new Vector2(5f, 0), ForceMode2D.Impulse);
                    
                }
                else {
                    target.Rbody2D.AddForce(new Vector2(-5f, 0), ForceMode2D.Impulse);
                }
                break;
        }
    }
    // 随机效果
    public override void setEffect(GameCharacter target) {
        switch (getAttackType()) {
            case AttackEnum.NORMAL_ATTACK:
                CurrentState = StateEnum.ATTACK;
                break;
            default:
                break;
        }
    }
    // 获得攻击效果
    public override AttackEnum getAttackType() {
        // 该角色只会进行普通攻击
        return AttackEnum.NORMAL_ATTACK;
    }
    // 检测是否会踩空，如果会，则返回true，否则返回false
    public bool isMisstep() {
        //if (Physics2D.OverlapArea(MisstepRange.bounds.min, MisstepRange.bounds.max, GroundLayer)) {
        //    return false;
        //} else {
        //    return true;
        //}
        return false;
    }
}