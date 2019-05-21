/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     playerController.cs
 *Author:       Gx
 *Version:      1.1
 *UnityVersion：2017.3.1f1
 *Date:         2018-07-15
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


[System.Serializable]
public class Player : GameCharacter {
    bool LockFlip;
    bool OnWall;
    bool IsFacingRight;
    bool IsCrouching;
    bool IsSpeeling;
    bool IsJumping;
    bool IsRunning;
    bool IsDashing;
    bool IsRolling;
    bool IsHitting;
    bool IsHealing;
    bool IsDead;
    float T1;
    float T2;
    float Direction;
    float NormalGravityScale;
    Animator MoveAnim;
    [Header("About dead")]
    public EnableDeadUI DeadUI;
    [Header("Data manager")]
    public DataOperator Operator;
    public ItemManager Bag;
    [Header("Check trigger")]
    public Transform GroundCheck;
    public Transform WallCheck;
    public Transform FeetCheck;
    public Transform HeadCheck;
    public float GroundCheckRadius;
    public LayerMask GroundLayer;
    [Header("Jump settings")]
    public int ExtraJumpCount;
    public int MaxExtraJumpCount;
    public float MinJumpForce;
    public float CurrentJumpForce;
    public float ExtraJumpMultiple;
    public float DeltaJumpForcePercent;
    public float MaxJumpDuration;
    public float CurrentJumpDuration;
    [Header("Dash settings")]
    public float DashingMultiple;
    public float DashingCost;
    [Header("Roll settings")]
    public float RollingTime;
    public float BufferTime;
    public float RollongMultiple;
    public float RollingCost;
    [Header("UI settings")]
    public GameObject ChatComponent;
    public TextMeshProUGUI ChatText;
    private void Start()
    {
        Operator.DataAssets.Path = Application.dataPath + "/Save/PlayerData.txt";
        Invoke("loadData", 0.1f); 
        //loadData();
        GameCharacterManager.Instance.removePlayer(this);
        GameCharacterManager.Instance.addPlayer(this);
        // init
        #region init
        MoveAnim = GetComponent<Animator>();
        Rbody2D = GetComponent<Rigidbody2D>();

        IsFacingRight = true;
        Locked = false;
        IsCrouching = false;
        LockFlip = false;
        OnGround = true;
        Alive = true;
        IsJumping = false;
        IsRunning = false;
        IsDashing = false;
        IsHitting = false;

        Direction = 0;
        ExtraJumpCount = MaxExtraJumpCount;
        CurrentJumpForce = MinJumpForce;
        CurrentCoolDown = CoolDown;
        CurrentJumpDuration = 0;
        NormalGravityScale = Rbody2D.gravityScale;
        #endregion
    }
    private void Update()
    {
        if(Time.timeScale == 0) {
            return;
        }
        // check alive
        #region check alive
        if (Alive == false) {
            Locked = true;
            Rbody2D.velocity = new Vector2(0, Rbody2D.velocity.y);
            if (!IsDead) {
                MoveAnim.SetBool("dead", true);
                goToDead(-1f);
            }
        }
        #endregion
        // go to crouch
        #region go to crouch
        if ((Input.GetKeyDown(KeyCode.DownArrow)) && OnGround == true) {
            IsCrouching = true;
        } else if (Input.GetKeyUp(KeyCode.DownArrow)) {
            IsCrouching = false;
        }
        if (IsCrouching == true) {
            updateState(StateEnum.CROUCH);
        } else if (CurrentState == StateEnum.CROUCH) {
            finishCrouch();
        }
        #endregion
        // operation
        #region operation
        if (Locked == false && IsRolling == false && IsCrouching == false) {
            // go to jump
            #region    go to jump
            if (Input.GetKeyDown(KeyCodeList.Instance.Jump) && IsHealing == false && IsSpeeling == false) {
                if (OnGround == true && OnWall == false) {
                    IsJumping = true;
                    OnGround = false;
                    updateState(StateEnum.JUMP, Direction, CurrentSpeed, CurrentJumpForce * (1 + (CurrentSpeed - WalkingSpeed) * 0.1f));
                } else if (ExtraJumpCount > 0 && IsSpeeling == false) {
                    // extra jump
                    ExtraJumpCount--;
                    Rbody2D.velocity = new Vector2(0, 0);
                    updateState(StateEnum.JUMP, Direction, CurrentSpeed, CurrentJumpForce * ExtraJumpMultiple);
                }
            }
            #endregion 
            // jump higher
            #region  jump higher
            if (Input.GetKey(KeyCodeList.Instance.Jump) && IsJumping == true) {
                if (CurrentJumpDuration > 0) {
                    Rbody2D.velocity += Vector2.up * CurrentJumpForce * DeltaJumpForcePercent;
                    CurrentJumpDuration -= Time.deltaTime;
                }
                else {
                    IsJumping = false;
                }
            }
            if (Input.GetKeyUp(KeyCodeList.Instance.Jump)) {
                IsJumping = false;
            }
            #endregion
            // go to attack
            #region  go to attack
            if (CurrentCoolDown < CoolDown) {
                // 更新冷却
                CurrentCoolDown += Time.deltaTime;
            }
            if (CurrentCoolDown >= CoolDown && !IsRolling && !IsHealing) {
                if (Input.GetKeyDown(KeyCodeList.Instance.Attack) && OnGround == true) {
                    if (IsDashing) {
                        finishDash();
                    }
                    // 重置冷却时间
                    CurrentCoolDown = 0;
                    updateState(StateEnum.ATTACK, Direction, CurrentSpeed, CurrentJumpForce, true, AttackEnum.NORMAL_ATTACK);
                }
                if (Input.GetKeyDown(KeyCodeList.Instance.Attack) && OnGround == false) {
                    // 重置冷却时间
                    CurrentCoolDown = 0;
                    updateState(StateEnum.ATTACK, Direction, CurrentSpeed, CurrentJumpForce, true, AttackEnum.JUMP_ATTACK); 
                }
                if (Input.GetKeyDown(KeyCodeList.Instance.Attack) && OnWall == true) {
                    // 重置冷却时间
                    CurrentCoolDown = 0;
                    updateState(StateEnum.ATTACK, Direction, CurrentSpeed, CurrentJumpForce, true, AttackEnum.SPEEL_ATTACK);
                }
            }
            #endregion
            //roll
            #region roll
            if(CurrentState != StateEnum.ATTACK && !IsRolling && !IsHealing 
                && checkGround() && CurrentEnergy >= RollingCost
                && Input.GetKeyDown(KeyCodeList.Instance.Roll)) {
                if (IsDashing) {
                    finishDash();
                } else {
                    CurrentEnergy -= RollingCost;
                }
                IsRolling = true;
                Invoke("finishRoll", RollingTime);
            }
            #endregion
            // dash
            #region dash
            if (IsRunning && Input.GetKey(KeyCodeList.Instance.Dash) && checkGround() && !IsHealing) {
                if (CurrentEnergy > 0) {
                    IsDashing = true;
                    CurrentEnergy -= Time.deltaTime * DashingCost;
                } else {
                    finishDash();
                }
            } else {
                finishDash();   
            }
            #endregion
        }
        #endregion
        // cure energy
        #region cure energy
        if(!IsDashing && !IsRolling) {
            CurrentEnergy += Time.deltaTime * EnergyCure;
            if (CurrentEnergy > TotalEnergy) {
                CurrentEnergy = TotalEnergy;
            } else if (CurrentEnergy < 0) {
                CurrentEnergy = 0;
            } 
        }
        #endregion
    }
    private void FixedUpdate() {
        if(Time.timeScale == 0) {
            return;
        }
        // if player is alive;
        checkAlive();
        if (Direction == 0 && IsRunning) {
            IsRunning = false;
        }
        // if player is on the ground
        checkGround();
        // listen horizontal key down
        Direction = Input.GetAxis("Horizontal");
        // update vertical speed
        MoveAnim.SetFloat("VSpeed", Rbody2D.velocity.y);
        // if player is on the wall
        #region  if player is on the wall
        if (checkWall() == true) {
            if (IsFacingRight && Input.GetKey(KeyCodeList.Instance.Right) || !IsFacingRight && Input.GetKey(KeyCodeList.Instance.Left)) {
                // ready to speel
                if (Input.GetKey(KeyCodeList.Instance.Jump) && Math.Abs(Rbody2D.velocity.y) > 0.5f) {
                    IsSpeeling = true;
                    MoveAnim.SetBool("speel", true);
                    // init jump count
                    ExtraJumpCount = MaxExtraJumpCount;
                    // sleep on the wall
                    updateState(StateEnum.SPEEL);
                }
            }
        } else {
            // out of the wall
            MoveAnim.SetBool("speel", false);
            IsSpeeling = false;
            Rbody2D.gravityScale = NormalGravityScale;
        }
        #endregion
        // if not locked
        #region if not locked
        if (Locked == false) {
            // jump on the wall 
            #region jump on the wall
            if (IsSpeeling) {
                if ((Input.GetAxis("Horizontal") < 0 && IsFacingRight == true) || (Input.GetAxis("Horizontal") > 0 && IsFacingRight == false)) {
                    // jump from the wall
                    Rbody2D.gravityScale = NormalGravityScale;
                    flip();
                    Rbody2D.velocity = new Vector2(0, 0);
                    MoveAnim.SetBool("speel", false);
                    IsSpeeling = false;
                    if (Input.GetKey(KeyCode.DownArrow)) {
                        //updateState(StateEnum.JUMP, Direction, CurrentSpeed * 2.0f, -NormalGravityScale, false);
                    } else {
                        IsRunning = true;
                        updateState(StateEnum.JUMP, Direction, CurrentSpeed * 2.0f, CurrentJumpForce * ExtraJumpMultiple, true);
                    }
                }
            }
            #endregion
            // double click key to run
            #region double click key to run
            if (Input.GetKeyDown(KeyCodeList.Instance.Left) && checkGround() && !IsHealing) {
                T2 = Time.realtimeSinceStartup;
                if (T2 - T1 < 0.75f) {
                    if (Input.GetKey(KeyCodeList.Instance.Left)) {
                        IsRunning = true;
                    }
                } else {
                    IsRunning = false;
                }
                T1 = T2;
            } else if (Input.GetKeyDown(KeyCodeList.Instance.Right) && checkGround()) {
                T2 = Time.realtimeSinceStartup;
                if (T2 - T1 < 0.35f) {
                    if (Input.GetKey(KeyCodeList.Instance.Right)) {
                        IsRunning = true;
                    }
                } else {
                    IsRunning = false;
                }
                T1 = T2;
            }
            #endregion
            // move control
            #region move control
            if (IsCrouching) {

            } else if (IsHealing) {
                CurrentSpeed = WalkingSpeed * 0.3f;
                updateState(StateEnum.WALK, Direction, CurrentSpeed);
            } else if (IsHitting) {
                updateState(StateEnum.WALK, Direction, CurrentSpeed);
            } else if (IsRolling) {
                updateState(StateEnum.ROLL, IsFacingRight ? 1 : -1);
            } else if (IsRunning && !IsDashing) {
                CurrentSpeed = RunningSpeed;
                updateState(StateEnum.RUN, Direction, CurrentSpeed);
            } else if (IsRunning && IsDashing) {
                updateState(StateEnum.DASH, Direction, RunningSpeed * DashingMultiple);
            } else {
                CurrentSpeed = WalkingSpeed;
                updateState(StateEnum.WALK, Direction, CurrentSpeed);
            }
            #endregion
            // go to flip
            #region  go to flip
            if (Alive) {
                if (Direction > 0 && IsFacingRight == false && LockFlip == false) {
                    flip();
                }
                else if (Direction < 0 && IsFacingRight == true && LockFlip == false) {
                    flip();
                }
            }
            #endregion
        }
        #endregion
    }
    /// <summary>
    /// 保存数据
    /// </summary>
    public void saveData() {
        // 更新DataAssets
        Operator.DataAssets.CurrentHealth = CurrentHealth;
        Operator.DataAssets.TotalHealth = TotalHealth;
        Operator.DataAssets.CurrentEnergy = CurrentEnergy;
        Operator.DataAssets.TotalEnergy = TotalEnergy;
        // 清除物品索引列表
        Operator.DataAssets.ItemIndexList.Clear();
        // 更新物品索引列表
        for (int i = 0; i < ItemList.Count; i++) {
            Operator.DataAssets.addItemIndex(ItemList[i].Name, Bag.findItemGrid(ItemList[i]).ItemCount);
        }
        // 更新数据
        Operator.saveData();
        print("SaveData|Path:" + Operator.DataAssets.Path);
        try {
            GameObject.Find("Aside").GetComponent<AsideManager>().showContent("SaveData|Path:" + Operator.DataAssets.Path);
        } catch (Exception e) {
            Debug.Log(e.Message);
        }
    }
    /// <summary>
    /// 载入数据
    /// </summary>
    public void loadData() {
        // 从文件更新DataAssets
        Operator.DataAssets = (DataForPlayer)Operator.loadData();
        print("LoadData|Path:" + Operator.DataAssets.Path);
        // 从DataAssets载入数据
        TotalHealth = Operator.DataAssets.TotalHealth;
        CurrentHealth = Operator.DataAssets.CurrentHealth;
        TotalEnergy = Operator.DataAssets.TotalEnergy;
        CurrentEnergy = Operator.DataAssets.CurrentEnergy; 
        // 清空背包
        Bag.clearItem();
        // 更新背包
        Bag.LoadMode = true;
        for (int i = 0; i < Operator.DataAssets.ItemIndexList.Count; i++) {
            Bag.addItem(ItemStock.Instance.getItemByName(Operator.DataAssets.ItemIndexList[i].Name), Operator.DataAssets.ItemIndexList[i].Count);
        }
        Bag.LoadMode = false;
        Operator.DataAssets.Path = Application.dataPath + "/Save/PlayerData.txt";
    }
    /// <summary>
    /// 水平翻转
    /// </summary>
    private void flip() {
        IsFacingRight = !IsFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    /// <summary>
    /// 检测地面
    /// </summary>
    /// <returns></returns>
    private bool checkGround() {
        OnGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
        MoveAnim.SetBool("grounded", OnGround);
        if (OnGround == true) {
            ExtraJumpCount = MaxExtraJumpCount;
            CurrentJumpDuration = 0;
            CurrentJumpDuration = MaxJumpDuration;
        }        
        return OnGround;
    }
    /// <summary>
    /// 检测墙壁
    /// </summary>
    /// <returns></returns>
    private bool checkWall() {
        OnWall= Physics2D.OverlapCircle(WallCheck.position, GroundCheckRadius, GroundLayer) 
            && Physics2D.OverlapCircle(HeadCheck.position, GroundCheckRadius, GroundLayer)
            && Physics2D.OverlapCircle(FeetCheck.position, GroundCheckRadius, GroundLayer)
            && (Rbody2D.velocity.y != 0);
        return OnWall;
    }
    /// <summary>
    /// 死亡
    /// </summary>
    /// <param name="time">延迟时间,若为负值则不复活</param>
    public void goToDead(float time) {
        GameCharacterManager.Instance.killCharacter(this);
        Locked = true;
        IsDead = true;
        PlayerPrefs.SetInt("DeadCount", PlayerPrefs.GetInt("DeadCount") + 1);
        Rbody2D.velocity = new Vector2(0, 0);
        Rbody2D.gravityScale = 5.0f;
        MoveAnim.SetBool("dead", true);
        Eternal = true;
        if(time >= 0) {
            Invoke("revive", time);
        } else {
            DeadUI.gameObject.SetActive(true);
            DeadUI.enableFuncton();
        }
    }
    /// <summary>
    /// 复活
    /// </summary>
    public override void revive() {
        if(Time.timeScale != 1) {
            Time.timeScale = 1;
        }
        GameCharacterManager.Instance.reviveCharacter(this);
        IsDead = false;
        Rbody2D.gravityScale = NormalGravityScale;
        LoadObj.transform.position = new Vector2(PlayerPrefs.GetFloat("recordX"), PlayerPrefs.GetFloat("recordY"));
        CurrentHealth = TotalHealth;
        CurrentEnergy = TotalEnergy;
        Alive = true;
        Eternal = false;
        Locked = false;
        MoveAnim.SetBool("dead", false);
        if(GameCharacterManager.Instance.containPlayer(this) == false) {
            GameCharacterManager.Instance.addPlayer(this);
        }
        CurrentState = StateEnum.IDLE;
    }
    /// <summary>
    /// 攻击结果检测
    /// </summary>
    /// <param name="character">角色</param>
    /// <returns></returns>
    private new bool checkHit(GameCharacter character) {
        Bounds bounds = AttackRange.bounds;
        Collider2D[] results = new Collider2D[20];
        try {
            ContactFilter2D tempFilter = new ContactFilter2D();
            tempFilter.SetLayerMask(EnemyLayer);
            Physics2D.OverlapArea(bounds.min, bounds.max, tempFilter, results);
            if (Physics2D.OverlapArea(bounds.min, bounds.max, EnemyLayer)) {
                for (int i = 0; i < 20; i++) {
                    if (results[i].gameObject == character.LoadObj) {
                        return true;
                    }
                }
            }
        } catch (Exception e) {
            Debug.Log(e.Message);
            return false;
        }
        return false;
    }
    /// <summary>
    /// 执行攻击行为
    /// </summary>
    /// <param name="skill">攻击模式</param>
    /// <param name="damageType">伤害类型</param>
    /// <param name="damage">伤害数值</param>
    private void dealDamage(AttackEnum skill, DamageEnum damageType, float damage) {
        // 攻击NPC
        for(int i = 0; i < GameCharacterManager.Instance.NPCList.Count; i++) {
            if (checkHit(GameCharacterManager.Instance.NPCList[i])) {
                hit(GameCharacterManager.Instance.NPCList[i], damageType, damage);
                setAttackEffect(skill, GameCharacterManager.Instance.NPCList[i]);
                GameCharacterManager.Instance.NPCList[i].MyAI.setTarget(this);
                GameCharacterManager.Instance.NPCList[i].Target = this;
            }
        }
        // 攻击敌人
        for(int i = 0; i < GameCharacterManager.Instance.EnemyList.Count; i++) {
            if (checkHit(GameCharacterManager.Instance.EnemyList[i])) {
                hit(GameCharacterManager.Instance.EnemyList[i], damageType, damage);
                setAttackEffect(skill, GameCharacterManager.Instance.EnemyList[i]);
            }
        }
    }
    /// <summary>
    /// 设置攻击效果
    /// </summary>
    /// <param name="skill">攻击模式</param>
    /// <param name="target">目标</param>
    private void setAttackEffect(AttackEnum skill, GameCharacter target) {
        switch (skill) {
            case AttackEnum.NORMAL_ATTACK:
                if (IsFacingRight == true) {
                    target.Rbody2D.AddForce(new Vector2(1.5f, 0),ForceMode2D.Impulse);
                } else {
                    target.Rbody2D.AddForce(new Vector2(-1.5f, 0), ForceMode2D.Impulse);
                }
                break;
            case AttackEnum.JUMP_ATTACK:
                if (IsFacingRight == true) {
                    target.Rbody2D.AddForce(new Vector2(2.5f, -2.5f), ForceMode2D.Impulse);
                }
                else {
                    target.Rbody2D.AddForce(new Vector2(-2.5f, -2.5f), ForceMode2D.Impulse);
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 受到攻击
    /// </summary>
    /// <param name="damageType">伤害类型</param>
    /// <param name="damage">伤害数值</param>
    public override void getHurt(DamageEnum damageType, float damage) {
        base.getHurt(damageType, damage);
        if (Eternal) {
            return;
        }
        MoveAnim.SetBool("hurt", true);
        CurrentState = StateEnum.HURT;
        switch (damageType) {
            case DamageEnum.LIGHT_ATTACK:
                MoveAnim.SetBool("lightHurt", true);
                Locked = true;
                Invoke("finishLightHurt", 0.5f);
                break;
            case DamageEnum.HEAVY_ATTACK:
                MoveAnim.SetBool("heavyHurt", true);
                Locked = true;
                Invoke("finishHeavyHurt", 0.5f);
                break;
            case DamageEnum.STRIKE_FLY:
                MoveAnim.SetBool("strikeFly", true);
                Locked = true;
                Invoke("finishStrikeFly", 0.5f);
                break;
            default:
                break;
        }
    }
    private void finishLightHurt() {
        Rbody2D.velocity = new Vector2(0, 0);
        MoveAnim.SetBool("lightHurt", false);
        MoveAnim.SetBool("hurt", false);
        Locked = !checkAlive();
        CurrentState = StateEnum.IDLE;
    }
    private void finishHeavyHurt() {
        Rbody2D.velocity = new Vector2(0, 0);
        MoveAnim.SetBool("heavyHurt", false);
        MoveAnim.SetBool("hurt", false);
        Locked = !checkAlive();
        CurrentState = StateEnum.IDLE;
    }
    private void finishStrikeFly() {
        Rbody2D.velocity = new Vector2(0, 0);
        MoveAnim.SetBool("strikeFly", false);
        MoveAnim.SetBool("hurt", false);
        Locked = !checkAlive();
        CurrentState = StateEnum.IDLE;
    } 
    /// <summary>
    /// 更新状态
    /// </summary>
    /// <param name="state">状态枚举</param>
    /// <param name="direction">方向</param>
    /// <param name="speed">速度</param>
    /// <param name="jumpForce">跳跃力</param>
    /// <param name="up"></param>
    /// <param name="skill">攻击模式</param>
    public void updateState(StateEnum state, float direction = 0, float speed = 0, float jumpForce = 0,bool up = true, AttackEnum skill = AttackEnum.NORMAL_ATTACK) {
        switch (state)
        {
            case StateEnum.IDLE:
                CurrentState = StateEnum.IDLE;
                goToIdle();
                break;
            case StateEnum.WALK:
                if (Rbody2D.velocity.x >= CurrentSpeed)
                    CurrentState = StateEnum.WALK;
                goToWalk(direction, speed);
                break;
            case StateEnum.RUN:
                if (Rbody2D.velocity.x >= CurrentSpeed)
                    CurrentState = StateEnum.RUN;
                goToRun(direction, speed);
                break;
            case StateEnum.JUMP:
                CurrentState = StateEnum.JUMP;
                goToJump(jumpForce, up);
                break;
            case StateEnum.SPEEL:
                CurrentState = StateEnum.SPEEL;
                goToSpeel();
                break;
            case StateEnum.DEAD:
                CurrentState = StateEnum.DEAD;
                goToDead();
                break;
            case StateEnum.ALIVE:
                CurrentState = StateEnum.ALIVE;
                break;
            case StateEnum.ATTACK:
                CurrentState = StateEnum.ATTACK;
                startAttack(skill);
                break;
            case StateEnum.CROUCH:
                CurrentState = StateEnum.CROUCH;
                startCrouch();
                break;
            case StateEnum.DASH:
                CurrentState = StateEnum.DASH;
                startDash(direction,speed);
                break;
            case StateEnum.ROLL:
                CurrentState = StateEnum.ROLL;
                startRoll(direction);
                break;
            default:
                goToIdle();
                break;
        }
    }
    /// <summary>
    /// 站立
    /// </summary>
    private void goToIdle() {
    }
    /// <summary>
    /// 行走
    /// </summary>
    /// <param name="direction">方向</param>
    /// <param name="speed">速度</param>
    private void goToWalk(float direction, float speed) {
        MoveAnim.SetFloat("speed", Mathf.Abs(direction * speed));
        Rbody2D.velocity = new Vector2(direction * speed, Rbody2D.velocity.y);
    }
    /// <summary>
    /// 奔跑
    /// </summary>
    /// <param name="direction">方向</param>
    /// <param name="speed">速度</param>
    private void goToRun(float direction, float speed) {
        MoveAnim.SetFloat("speed", Mathf.Abs(direction * speed));
        Rbody2D.velocity = new Vector2(direction * speed, Rbody2D.velocity.y);
    }
    /// <summary>
    /// 跳跃
    /// </summary>
    /// <param name="jumpForce">跳跃力</param>
    /// <param name="up"></param>
    private void goToJump(float jumpForce, bool up = true) {
        //limit
        if (ExtraJumpCount >= 0) {
            Rbody2D.AddForce(new Vector2(0, jumpForce * (up ? 1 : -1)));
        }    
    }
    /// <summary>
    /// 下蹲
    /// </summary>
    private void startCrouch() {
        Rbody2D.velocity = new Vector2(0, 0);
        //Locked = true;
        MoveAnim.SetBool("crouch", true);
    }
    /// <summary>
    /// 停止下蹲
    /// </summary>
    private void finishCrouch() {
        //Locked = false;
        MoveAnim.SetBool("crouch", false);
        CurrentState = StateEnum.IDLE;
    }
    /// <summary>
    /// 开始翻滚
    /// </summary>
    private void startRoll(float direction) {
        IsRolling = true;
        LockFlip = true;
        Eternal = true;
        if (Math.Abs(Rbody2D.velocity.x) >= CurrentSpeed + CurrentSpeed * RollongMultiple){
            Rbody2D.velocity -= new Vector2(CurrentSpeed * RollongMultiple, 0) * (Rbody2D.velocity.x > 0 ? 1 : -1);
        }
        if (direction > 0){
            Rbody2D.velocity += new Vector2(CurrentSpeed * RollongMultiple, 0);
        } else {
            Rbody2D.velocity += new Vector2(-CurrentSpeed * RollongMultiple, 0);
        }
        MoveAnim.SetBool("roll", true);
    }
    /// <summary>
    /// 结束翻滚
    /// </summary>
    private void finishRoll() {
        if (checkGround()) {
            Rbody2D.velocity = new Vector2(0, Rbody2D.velocity.y);
        }
        MoveAnim.SetBool("roll", false);
        Eternal = false;
        IsRolling = false;
        LockFlip = false;
        CurrentState = StateEnum.IDLE;
    }
    private void unlock() {
        Locked = false;
    }
    /// <summary>
    /// 攀爬
    /// </summary>
    private void goToSpeel() {
        Rbody2D.velocity = new Vector2(0, 0);
        //change the gravity scale
        Rbody2D.gravityScale = 1.0f;
    }
    /// <summary>
    /// 开始冲刺
    /// </summary>
    private void startDash(float direction, float speed) {
        if(direction == 0) {
            finishDash();
        }
        MoveAnim.SetBool("dashOnGround", true);
        Rbody2D.velocity = new Vector2(direction * speed, Rbody2D.velocity.y);
    }
    /// <summary>
    /// 结束冲刺
    /// </summary>
    private void finishDash() {
        MoveAnim.SetBool("dashOnGround", false);
        IsDashing = false;
    }
    /// <summary>
    /// 开始攻击
    /// </summary>
    /// <param name="skill">攻击模式</param>
    private void startAttack(AttackEnum skill) {
        // 攻击时不可转身
        LockFlip = true;
        IsHitting = true;
        switch (skill)
        {
            case AttackEnum.NORMAL_ATTACK:
                CurrentSpeed = 0.5f;
                MoveAnim.SetBool("normalAttack", true);
                dealDamage(skill, DamageEnum.HEAVY_ATTACK, BasicDamage * 5);
                Invoke("finishNormalAttack", 0.5f);
                break;
            case AttackEnum.JUMP_ATTACK:
                CurrentSpeed = 0.5f;
                Rbody2D.velocity = new Vector2(0, 0);
                MoveAnim.SetBool("jumpAttack", true);
                dealDamage(skill, DamageEnum.LIGHT_ATTACK, BasicDamage);
                Invoke("finishJumpAttack", 0.3f);
                break;
            case AttackEnum.SPEEL_ATTACK:
                MoveAnim.SetBool("speelAttack", true);
                dealDamage(skill, DamageEnum.LIGHT_ATTACK, BasicDamage);
                Invoke("finishSpeelAttack", 0.3f);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 结束攻击
    /// </summary>
    private void finishAttack(AttackEnum skill) {
        switch (skill) {
            case AttackEnum.NORMAL_ATTACK:
                Invoke("finishNormalAttack", 0);
                break;
            case AttackEnum.JUMP_ATTACK:
                Invoke("finishJumpAttack", 0);
                break;
            case AttackEnum.SPEEL_ATTACK:
                Invoke("finishSpeelAttack", 0);
                break;
        }
    }
    private void finishNormalAttack() {
        Rbody2D.velocity = new Vector2(0, 0);
        CurrentSpeed = WalkingSpeed;
        LockFlip = false;
        MoveAnim.SetBool("normalAttack", false);
        IsHitting = false;
        CurrentState = StateEnum.IDLE;
    }
    private void finishJumpAttack() {
        CurrentSpeed = WalkingSpeed;
        LockFlip = false;
        MoveAnim.SetBool("jumpAttack", false);
        IsHitting = false;
        CurrentState = StateEnum.IDLE;
    }
    private void finishSpeelAttack() {
        LockFlip = false;
        MoveAnim.SetBool("speelAttack", false);
        IsHitting = false;
        CurrentState = StateEnum.IDLE;
    }
    /// <summary>
    /// 恢复中
    /// </summary>
    /// <param name="time"></param>
    public override void healing(float time) {
        base.healing(time);
        IsHealing = true;
        Invoke("finishHealing", time);   
    }
    private void finishHealing() {
        IsHealing = false;
    }
    /// <summary>
    /// 对话
    /// </summary>
    /// <param name="content"></param>
    /// <param name="stayTime"></param>
    public override void startChat(string content, float stayTime) {
        base.startChat(content, stayTime);
        ChatComponent.SetActive(true);
        ChatText.text = content;
        CancelInvoke("finishChat");
        Invoke("finishChat", stayTime);
    }
    /// <summary>
    /// 结束对话
    /// </summary>
    protected override void finishChat() {
        base.finishChat();
        ChatText.text = "";
        ChatComponent.SetActive(false);
    }
}