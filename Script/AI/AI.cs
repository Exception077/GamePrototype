/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     AI.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-15
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System;
using System.Collections;

public enum AIMode {
    IDLE, NORMAL, CRAZY
}
public enum AIState {
    IDLE, MOVE, CHASE, ATTACK, HURT
}
public class AI : MonoBehaviour {
    [Header("Entity")]
    public NPC I;
    
    [Header("AIMode")]
    public AIMode Mode;
    [SerializeField]LayerMask ViewLayer;

    [Header("Range")]
    public Collider2D ViewRange;
    public Collider2D NearRange;
    public Collider2D AttackRange;
    public Collider2D HorizonRange;

    [Header("State")]
    [SerializeField]AIState State;

    [Header("Focus on")]
    [SerializeField]GameCharacter Target;

    [Header("AISettings")]
    // AI思考间隔
    [SerializeField]float AIThankDuration;
    // AI上一次思考时间
    [SerializeField]float AIThankTimer;
    // Signal
    [SerializeField]AISignalManager SignalManager;
    [Header("Trigger")]
    public bool AI_ON = true;
    public bool AI_PAUSE;

    bool IgnoreFlip;
    [SerializeField]float PlatformOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(AI_ON == false) {
            return;
        }
        if (I.CurrentState == StateEnum.HURT) {
            Mode = AIMode.CRAZY;
        }
        switch (Mode) {
            case AIMode.IDLE:
                updateAIMode0();
                break;
            case AIMode.NORMAL:
                updateAIMode1();
                break;
            case AIMode.CRAZY:
                updateAIMode2();
                break;
        }
	}

    /// <summary>
    /// 在该模式下，当目标进入NPC视野时，NPC朝向目标
    /// </summary>
    void updateAIMode0() {
        if (checkView(Target)) {
            if(checkRight(Target) != I.IsFacingRight) {
                // 目标与NPC不同向
                I.flip();
            }
        }
    }

    /// <summary>
    /// 在该模式下，当目标进入NPC视野时，NPC朝向目标,同时NPC有时会闲逛
    /// </summary>
    void updateAIMode1() {
        if(State == AIState.IDLE) {
            if (checkRight(Target) != I.IsFacingRight) {
                // 目标与NPC不同向
                I.flip();
            }
        }
        // 判断是否开始思考
        if (isAIThank()) {
            // 开始思考
            AIThank(0,1);
        } else {
            if (AI_PAUSE)
                return;
            // 更新状态
            UpdateState();
        }
    }

    /// <summary>
    /// 在该模式下，当目标进入NPC视野时，NPC将追击目标，直到杀死目标
    /// </summary>
    void updateAIMode2() {
        if(Target != null) {
            if (GameCharacterManager.Instance.contaiinsBody(Target)) {
                Mode = AIMode.NORMAL;
            }
        }
        if (checkRight(Target) != I.IsFacingRight && checkView(Target) && IgnoreFlip == false) {
            // 目标与NPC不同向
            I.flip();
        }
        // 判断是否开始思考
        if (isAIThank()) {
            // 开始思考
            AIThank(2, 3);
        }
        else {
            if (AI_PAUSE)
                return;
            // 更新状态
            UpdateState();
        }
    }

    protected bool isAIThank() {
        // AI在固定时长后思考一次
        if (AIThankTimer >= AIThankDuration) {
            AIThankTimer = 0;
            return true;
        } else {
            AIThankTimer += Time.deltaTime;
            return false;
        }
    }
    /// <summary>
    /// AI思考逻辑
    /// </summary>
    /// <param name="minCount"></param>
    /// <param name="maxCount"></param>
    protected void AIThank(int minCount, int maxCount) {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int d = UnityEngine.Random.Range(minCount, maxCount);
        switch (d) {
            case (int)AIState.IDLE:
                setState(AIState.IDLE);
                break;
            case (int)AIState.MOVE:
                setState(AIState.MOVE);
                break;
            case (int)AIState.CHASE:
                setState(AIState.CHASE);
                break;
            case (int)AIState.ATTACK:
                setState(AIState.ATTACK);
                break;
            case (int)AIState.HURT:
                setState(AIState.HURT);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 立即重新思考
    /// </summary>
    protected void reThink() {
        AIThankTimer = AIThankDuration;
    }
    /// <summary>
    /// 设定状态
    /// </summary>
    /// <param name="state"></param>
    public void setState(AIState state, bool direction = true) {
        if(State == state)
            return;
        State = state;
        switch (State) {
            case AIState.IDLE:
                break;
            case AIState.MOVE:
                break;
            case AIState.CHASE:
                break;
            case AIState.ATTACK:
                break;
            case AIState.HURT:
                break;
        }
    }
    /// <summary>
    /// 设置当前目标
    /// </summary>
    /// <param name="gc"></param>
    public void setTarget(GameCharacter gc) {
        Target = gc;
        setSignal(gc);
    }
    /// <summary>
    /// 更新状态
    /// </summary>
    protected void UpdateState() {
        switch (State) {
            case AIState.IDLE:
                if (I.OnGround) {
                    I.stop();
                }
                break;
            case AIState.MOVE:
                I.move(AIThankDuration, I.WalkingSpeed);
                break;
            case AIState.CHASE:
                if(Target != null) {
                    if (checkView(Target) == false) {
                        // 视野外
                        reThink();
                        break;
                    } else if(readyToAttack(Target) == true) {
                        if (I.OnGround) {
                            I.stop();
                        }
                        I.Target = Target;
                        if(I.CurrentCoolDown == I.CoolDown) {
                            setState(AIState.ATTACK);
                        } else {
                            I.move(!checkRight(Target), I.WalkingSpeed, I.CoolDown - I.CurrentCoolDown);
                            sleep(I.CoolDown - I.CurrentCoolDown);
                        }
                    } else {
                        if (needJump(Target)) {
                            // 需要跳跃
                            if(selectSignal() == null) {
                                // 没有AISignal捕捉到目标
                                IgnoreFlip = false;
                                I.move(checkRight(Target), I.RunningSpeed);
                            } else if(readyToJump(selectSignal()) == false) {
                                // AISignal捕捉到目标
                                // 不满足跳跃条件，移动至AISignal另一侧
                                IgnoreFlip = true;
                                bool direction = true;
                                if (selectSignal().RightSide) {
                                    direction = (selectSignal().Flag.position.x + PlatformOffset) > I.LoadObj.transform.position.x;
                                } else {
                                    direction = (I.LoadObj.transform.position.x + PlatformOffset) < selectSignal().Flag.position.x;
                                }
                                // print("move to another side:" + direction + selectSignal().gameObject.name);
                                I.move(direction, I.RunningSpeed);
                                
                            } else {
                                // AISignal捕捉到目标
                                // 满足跳跃条件
                                IgnoreFlip = false;
                                I.jump();
                                I.move(checkRight(Target), I.RunningSpeed);
                                pause(1.0f);
                            }
                        } else {
                            // 不需要跳跃
                            I.move(checkRight(Target), I.RunningSpeed);
                            pause(0.2f);
                        }
                    }
                }
                break;
            case AIState.ATTACK:
                if(Target != null) {
                    I.Target = Target;
                    if (I.OnGround) {
                        I.stop();
                    }
                    pause(1.0f);
                    //print("ready attack");
                    I.readyToAttack(0.5f);
                }
                break;
            case AIState.HURT:
                pause(0.75f);
                break;
        }
    }
    /// <summary>
    /// 挂起AI，继续执行最后一次决策，在StayTime内不再进行思考
    /// </summary>
    /// <param name="stayTime"></param>
    void pause(float stayTime = 1.0f) {
        AI_PAUSE = true;
        Invoke("cure", stayTime);
    }
    /// <summary>
    /// 关闭AI，不再思考
    /// </summary>
    /// <param name="stayTime"></param>
    void sleep(float stayTime = 1.0f) {
        AI_ON = false;
        Invoke("cure", stayTime);
    }
    void cure() {
        AI_PAUSE = false;
        AI_ON = true;
    }
    /// <summary>
    /// 检测目标角色是否位于当前NPC右侧
    /// </summary>
    /// <param name="gc">目标角色</param>
    /// <returns></returns>
    bool checkRight(GameCharacter gc) {
        if(gc == null) {
            return false;
        } else {
            return gc.LoadObj.transform.position.x > I.LoadObj.transform.position.x ? true : false;
        }
    }
    /// <summary>
    /// 检测目标是否在NPC视野内
    /// </summary>
    /// <param name="gc"></param>
    /// <returns></returns>
    bool checkView(GameCharacter gc) {
        if(gc == null) {
            return false;
        } else {
            Bounds bounds = ViewRange.bounds;
            Collider2D[] results = new Collider2D[20];
            try {
                ContactFilter2D tempFilter = new ContactFilter2D();
                tempFilter.SetLayerMask(ViewLayer);
                Physics2D.OverlapArea(bounds.min, bounds.max, tempFilter, results);
                if (Physics2D.OverlapArea(bounds.min, bounds.max, ViewLayer)) {
                    for (int i = 0; i < 20; i++) {
                        if (results[i].gameObject == gc.LoadObj) {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e) {
                Debug.Log(e.Message);
                return false;
            }
            return false;
        }
    }
    /// <summary>
    /// 检测目标是否与NPC近身
    /// </summary>
    /// <param name="gc"></param>
    /// <returns></returns>
    bool checkNear(GameCharacter gc) {
        if (gc == null) {
            return false;
        } else {
            Bounds bounds = NearRange.bounds;
            Collider2D[] results = new Collider2D[20];
            try {
                ContactFilter2D tempFilter = new ContactFilter2D();
                tempFilter.SetLayerMask(ViewLayer);
                Physics2D.OverlapArea(bounds.min, bounds.max, tempFilter, results);
                if (Physics2D.OverlapArea(bounds.min, bounds.max, ViewLayer)) {
                    for (int i = 0; i < 20; i++) {
                        if (results[i].gameObject == gc.LoadObj) {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e) {
                Debug.Log(e.Message);
                return false;
            }
            return false;
        }
    }
    /// <summary>
    /// 检测是否具备攻击条件
    /// </summary>
    /// <returns></returns>
    bool readyToAttack(GameCharacter gc) {
        if (gc == null) {
            return false;
        } else {
            Bounds bounds = AttackRange.bounds;
            Collider2D[] results = new Collider2D[20];
            try {
                ContactFilter2D tempFilter = new ContactFilter2D();
                tempFilter.SetLayerMask(ViewLayer);
                Physics2D.OverlapArea(bounds.min, bounds.max, tempFilter, results);
                if (Physics2D.OverlapArea(bounds.min, bounds.max, ViewLayer)) {
                    for (int i = 0; i < 20; i++) {
                        if (results[i].gameObject == gc.LoadObj) {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e) {
                Debug.Log(e.Message);
                return false;
            }
            return false;
        }
    }
    /// <summary>
    /// 设置AISignal检测对象
    /// </summary>
    /// <param name="gc"></param>
    void setSignal(GameCharacter gc) {
        foreach(AISignal signal in SignalManager.SignalList) {
            signal.setTarget(gc);
        }
    }
    /// <summary>
    /// 选择AISignal策略
    /// </summary>
    /// <returns></returns>
    AISignal selectSignal() {
        foreach(AISignal signal in SignalManager.SignalList) {
            if(signal.Caught == true) {
                return signal;
            }
        }
        return null;
    }
    /// <summary>
    /// 检测是否可以跳到某个AISignal所在位置
    /// </summary>
    /// <param name="signal"></param>
    /// <returns></returns>
    bool readyToJump(AISignal signal) {
        if(signal == null) {
            // 无AISignal捕捉到目标
            return false;
        }
        if(I.LoadObj.transform.position.x < signal.Flag.position.x && signal.RightSide) {
            return false;
        }
        if(I.LoadObj.transform.position.x > signal.Flag.position.x && !signal.RightSide) {
            return false;
        }
        float x = Mathf.Abs(I.LoadObj.transform.position.x - signal.Flag.position.x) - PlatformOffset;
        if(x < 0) {
            // 水平偏移量过大
            return false;
        }
        float h = Mathf.Abs(I.LoadObj.transform.position.y - signal.Flag.position.y); 
        float v = I.JumpForce / I.Rbody2D.mass;
        float ymax = v * v / (2 * Physics2D.gravity.y);
        float t = x / I.CurrentSpeed;
        float y = v * t - 0.5f * Physics2D.gravity.y * t * t;
        return y > h;
    }
    /// <summary>
    /// 判断是否有必要跳跃
    /// </summary>
    /// <param name="gc"></param>
    /// <returns></returns>
    bool needJump(GameCharacter gc) {
        if (gc == null) {
            // 目标为空
            return false;
        } else if(gc.OnGround == false) {
            // 目标未落地
            return false;
        } else {
            Bounds bounds = HorizonRange.bounds;
            Collider2D[] results = new Collider2D[20];
            try {
                ContactFilter2D tempFilter = new ContactFilter2D();
                tempFilter.SetLayerMask(ViewLayer);
                Physics2D.OverlapArea(bounds.min, bounds.max, tempFilter, results);
                if (Physics2D.OverlapArea(bounds.min, bounds.max, ViewLayer)) {
                    for (int i = 0; i < 20; i++) {
                        if (results[i].gameObject == gc.LoadObj) {
                            return false;
                        }
                    }
                }
            }
            catch (Exception e) {
                Debug.Log(e.Message);
                return false;
            }
            return true;
        }
    }
}