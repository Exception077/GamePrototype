/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     NpcAI.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-16
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
public enum AIModEnum
{
    NORMAL, ACTIVE, GUARD, CRAZY
}
public enum AIStateEnum
{
    NPC_NORMAL, NPC_MOVE, MPC_CHASE, NPC_ATTACK
}
public class NpcAI : MonoBehaviour {
    bool ReadyToAttack;
    bool CatchTarget;
    
    [Header("Mode/State")]
    // AI模式
    public AIModEnum AIType;
    // AI状态
    public AIStateEnum State;
    [Header("Brain info")]
    // AI思考间隔
    public float AIThankDuration;
    // AI上一次思考时间
    public float AIThankLastTime;
    // AI开关
    public bool AI_ON;
    [Header("Watching settings")]
    // “眼睛”
    public Transform Eye;
    // 监视层
    public LayerMask WatchingLayer;
    // 监视范围
    public Collider2D WatchingRange;
    // 警戒范围
    public Collider2D GuardingRange;
    [Header("Entity")]
    // NPC实体    
    public NPC MyNPC;
    // 目标
    public GameCharacter Target;
    public float StayTime;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(GameCharacterManager.Instance.PlayerList.Count > 0) {
            ReadyToAttack = MyNPC.checkHit(getNearCharacter());
            CatchTarget = catchTarget(getNearCharacter());
        }
        if (!MyNPC.Alive) {
            AI_ON = false;
        }
        selectMode();
    }
    /// <summary>
    /// 切换模式的策略
    /// </summary>
    protected void selectMode() {
        if (AI_ON) {
            if(MyNPC.CurrentState == StateEnum.HURT) {
                AIType = AIModEnum.GUARD;
            }
            switch (AIType) {
                case AIModEnum.NORMAL:
                    updateNormalMod();
                    break;
                case AIModEnum.GUARD:
                    updateGuardMod();
                    break;
                case AIModEnum.CRAZY:
                    updateGuardMod();
                    break;
            }
        }
    }
    /// <summary>
    /// 普通模式，NPC将偶尔进行水平移动
    /// </summary>
    protected void updateNormalMod() {
        if (isAIThank()) {
            // 开始思考
            // 只触发NPC_NORMAL, NPC_MOVE两种状态
            AIThank(2);
        } else {
            // 更新AI状态
            updateAIState();
        }        
    }
    /// <summary>
    /// 戒备模式，当玩家进入监视范围，NPC将追击玩家
    /// </summary>
    protected void updateGuardMod() {
        Target = getNearCharacter();
        MyNPC.Target = Target;
        if (Target.Alive) {
            goToChase(Target);
        } else {
            updateNormalMod();
        }
    }

    protected void updateCrazyMod() {

    }

    protected bool isAIThank() {
        // AI在固定时长后思考一次
        if(Time.time - AIThankLastTime >= AIThankDuration) {
            AIThankLastTime = Time.time;
            return true;
        } else {
            return false;
        }
    }
    // AI思考逻辑
    protected void AIThank(int count) {
        Random.InitState((int)System.DateTime.Now.Ticks);
        int d = Random.Range(0, count);
        switch (d) {
            case (int)AIStateEnum.NPC_NORMAL:
                setAIState(AIStateEnum.NPC_NORMAL);
                break;
            case (int)AIStateEnum.NPC_MOVE:
                setAIState(AIStateEnum.NPC_MOVE);
                break;
            case (int)AIStateEnum.MPC_CHASE:
                setAIState(AIStateEnum.MPC_CHASE);
                break;
            case (int)AIStateEnum.NPC_ATTACK:
                setAIState(AIStateEnum.NPC_ATTACK);
                break;
            default:
                break;
        }
    }
    protected void AIThank(int minCount, int maxCount) {
        Random.InitState((int)System.DateTime.Now.Ticks);
        int d = Random.Range(minCount, maxCount);
        switch (d) {
            case (int)AIStateEnum.NPC_NORMAL:
                setAIState(AIStateEnum.NPC_NORMAL);
                break;
            case (int)AIStateEnum.NPC_MOVE:
                setAIState(AIStateEnum.NPC_MOVE);
                break;
            case (int)AIStateEnum.MPC_CHASE:
                setAIState(AIStateEnum.MPC_CHASE);
                break;
            case (int)AIStateEnum.NPC_ATTACK:
                setAIState(AIStateEnum.NPC_ATTACK);
                break;
            default:
                break;
        }
    }
    protected void setAIState(AIStateEnum newState) {
        if (State == newState)
            return;
        State = newState;
        switch (State) {
            case AIStateEnum.NPC_NORMAL:
                break;
            case AIStateEnum.NPC_MOVE:
                break;
            case AIStateEnum.MPC_CHASE:
                break;
            case AIStateEnum.NPC_ATTACK:
                break;
            default:
                break;
        }
    }
    // 获取最近的角色
    protected GameCharacter getNearCharacter() {
        float min = (GameCharacterManager.Instance.PlayerList[0].LoadObj.transform.position - MyNPC.LoadObj.transform.position).magnitude;
        GameCharacter nearOne = new GameCharacter();
        for (int i = 0; i < GameCharacterManager.Instance.PlayerList.Count; i++) {
            if ((GameCharacterManager.Instance.PlayerList[i].LoadObj.transform.position - MyNPC.LoadObj.transform.position).magnitude <= min) {
                nearOne = GameCharacterManager.Instance.PlayerList[i];
            }
        }
        return nearOne;
    }
    // 判断朝向，当人物面向指定角色时，返回true，否则返回false
    protected bool checkFacing(GameCharacter character) {
        if (character.LoadObj.transform.position.x < MyNPC.LoadObj.transform.position.x && MyNPC.IsFacingRight == true) {
            // 目标在NPC左侧，NPC面朝右侧
            return false;
        } else if (character.LoadObj.transform.position.x > MyNPC.LoadObj.transform.position.x && MyNPC.IsFacingRight == false) {
            // 目标在NPC右侧，NPC面朝左侧
            return false;
        } else {
            return true;
        }
    }
    // 获取目标方向，右为true，左为false
    protected bool getCharacterDeriction(GameCharacter character) {
        return character.LoadObj.transform.position.x > MyNPC.LoadObj.transform.position.x;        
    }
    // 追击目标
    protected void goToChase(GameCharacter target) {
        if (CatchTarget == true) {
            //监视范围内存在目标
            {
                // 向目标方向移动
                if (ReadyToAttack == true) {
                    // 目标进入攻击范围
                    //MyNPC.stop();
                    if (MyNPC.CurrentCoolDown == MyNPC.CoolDown) {
                        // 冷却完毕，进行攻击
                        goToAttack(target);
                    } else {
                        // 正在冷却，暂时后退
                        MyNPC.move(!getCharacterDeriction(target), MyNPC.WalkingSpeed, (MyNPC.CoolDown - MyNPC.CurrentCoolDown) / 2.0f);
                        AISleep((MyNPC.CoolDown - MyNPC.CurrentCoolDown) / 2.0f);
                    }                       
                } else {
                    // 目标不在攻击范围，向目标方向移动
                    MyNPC.move(getCharacterDeriction(target), MyNPC.RunningSpeed);
                }
            }
        } else {
            updateNormalMod();
        }
    }
    // 攻击目标
    protected void goToAttack(GameCharacter target) {
        MyNPC.stop();
        MyNPC.readyToAttack(StayTime);
        setAIState(AIStateEnum.MPC_CHASE);
        AISleep(1.75f);
    }
    // 检测目标是否在监视范围内
    protected bool catchTarget(GameCharacter character) {
        Bounds bounds = GuardingRange.bounds;
        Collider2D[] results = new Collider2D[20];
        ContactFilter2D tempFilter = new ContactFilter2D();
        tempFilter.SetLayerMask(WatchingLayer);
        Physics2D.OverlapArea(bounds.min, bounds.max, tempFilter, results);
        if (Physics2D.OverlapArea(bounds.min, bounds.max, WatchingLayer)) {
            return true;
            for (int i = 0; i < 20; i++) {
                if (results[i].gameObject == character.LoadObj) {
                    return true;
                }
            }
        }
        return false;
    }
    // 更新AI状态
    protected void updateAIState() {
        switch (State) {
            case AIStateEnum.NPC_NORMAL:
                //MyNPC.WalkingDuration = 0;
                break;
            case AIStateEnum.NPC_MOVE:
                MyNPC.move(2f, MyNPC.WalkingSpeed);
                break;
            case AIStateEnum.MPC_CHASE:
                goToChase(getNearCharacter());
                break;
            case AIStateEnum.NPC_ATTACK:
                goToAttack(getNearCharacter());
                break;
            default:
                break;
        }
    }
    // 挂起AI
    protected void AISleep() {
        AI_ON = false;
    }
    protected void AISleep(float time) {
        AI_ON = false;
        Invoke("AIAwake", time);
    }
    // 唤醒AI
    protected void AIAwake() {
        AI_ON = true;
    } 
}