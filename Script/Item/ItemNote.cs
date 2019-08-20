using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNote : Item
{
    public override void onHold() {
        base.onHold();
    }

    public override void onObtain() {
        base.onObtain();
    }

    public override void onRemove() {
        base.onRemove();
    }

    public override bool onUse() {
        base.onUse();
        return true;
    }

    public override void onAbout() {
        base.onAbout();
        Owner.startChat(AboutInfo, 1 + AboutInfo.Length / 10f);
    }
}
