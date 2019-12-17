using UnityEngine;
using System.Collections;

public class Bleed : Buff {
    [SerializeField]
    float Damage;

    public override void onKeep() {
        base.onKeep();
        Target.CurrentHealth -= Damage * Time.deltaTime;
    }
}
