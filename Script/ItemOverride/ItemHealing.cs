// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealing : Item
{
    public float HealingCount;
    public GameObject HealingLight;
    GameObject Temp;
    string str;

    public override void onObtain() {
        base.onObtain();
        str = Description;
        CurrentCoolDown = CoolDown;
    }

    public override bool onUse() {
        base.onUse();
        if (Owner.OnGround == false) {
            return false;
        } else if(CurrentCoolDown < CoolDown) {
            return false;
        }
        CurrentCoolDown = 0;
        Owner.healing(CureTime);
        Owner.Rbody2D.velocity = new Vector2(0, 0);
        Temp = GameObject.Instantiate(HealingLight, Owner.LoadObj.transform.position, new Quaternion());
        Temp.SetActive(true);
        Temp.GetComponent<FollowTarget>().Target = Owner.LoadObj;
        Invoke("finishUse", CureTime);
        return true;
    }

    private void finishUse() {
        Owner.CurrentHealth += HealingCount;
        if (Owner.CurrentHealth > Owner.TotalHealth) {
            Owner.CurrentHealth = Owner.TotalHealth;
        }
        Destroy(Temp);
    }

    public override void onHold() {
        base.onHold();
        Description = str + "\n(剩余使用次数:" + UseCount + ")";
        if(CurrentCoolDown < CoolDown) {
            CurrentCoolDown += DeltaCoolDown * Time.deltaTime;
        } else {
            CurrentCoolDown = CoolDown;
        }
    }

    public override void onAbout() {
        base.onAbout();
        Owner.startChat(AboutInfo, 1 + AboutInfo.Length / 10f);
    }

    public override string getStatus() {
        return base.getStatus();
    }
}
