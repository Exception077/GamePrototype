/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     Hammond.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2018.3.10f1
 *Date:         2019-05-11
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;

public class Hammond : NPC {
    [SerializeField]
    float HealthCure;
    bool CureEnable = false;

	// Use this for initialization
	void Start () {
        GameCharacterManager.Instance.addNPC(this);
	}
	
	// Update is called once per frame
	void Update () {
        CurrentShieldValue += ShieldCure * Time.deltaTime;
        if (CurrentShieldValue >= TotalShieldValue) {
            CurrentShieldValue = TotalShieldValue;
        } else if(CurrentShieldValue <= 0) {
            CurrentShieldValue = 0;
        } 
        if (CureEnable) {
            CurrentHealth += HealthCure * Time.deltaTime;
            if (CurrentHealth > TotalHealth) {
                CurrentHealth = TotalHealth;
                CureEnable = false;
            }
        }
        if (CurrentHealth <= TotalHealth * 0.1f) {
            Eternal = true;
            CureEnable = true;
        } else if(CurrentHealth >= TotalHealth * 0.9f) {
            Eternal = false;
        } else if(CurrentHealth >= TotalHealth) {
            CureEnable = false;
        }
	}

    public override void getHurt(DamageDegree damageDegree, float damage, DamageType damageType) {
        if (!Eternal) {
            foreach (Equipment equipment in EquipmentList) {
                equipment.onHurt(ref damage);
            }
            // 伤害计算公式
            CurrentShieldValue -= damage;
            if (CurrentShieldValue <= 0) {
                CurrentShieldValue = 0;
                float temp = 0f;
                switch (damageType) {
                    case DamageType.PHYSIC:
                        temp = damage - PhysicDefence - CurrentShieldValue;
                        CurrentHealth -= temp >= 0 ? temp : 0;
                        break;
                    case DamageType.MAGIC:
                        temp = damage - MagicDefence - CurrentShieldValue;
                        CurrentHealth -= temp >= 0 ? temp : 0;
                        break;
                }
            }
        }
    }
}