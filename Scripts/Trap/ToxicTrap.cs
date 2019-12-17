using UnityEngine;
using System.Collections.Generic;
using System;

public class ToxicTrap : Trap {
    [SerializeField]
    Buff Debuff;
    [SerializeField]
    Collider2D Area;
    [SerializeField]
    List<GameCharacter> Characters = new List<GameCharacter>();
    [SerializeField]
    LayerMask TargetLayer;


    public override bool catchTarget() {
        Bounds bounds = Area.bounds;
        Collider2D[] results = new Collider2D[20];
        Characters.Clear();
        try {
            ContactFilter2D tempFilter = new ContactFilter2D();
            tempFilter.SetLayerMask(TargetLayer);
            Physics2D.OverlapArea(bounds.min, bounds.max, tempFilter, results);
            if (Physics2D.OverlapArea(bounds.min, bounds.max, TargetLayer)) {
                for (int i = 0; i < 20; i++) {
                    if(!Characters.Contains(results[i].gameObject.GetComponent<GameCharacter>()))
                    Characters.Add(results[i].gameObject.GetComponent<GameCharacter>());
                }
            }
        } catch (Exception e) {
            Debug.Log(e.Message);
        }
        return Characters.Count > 0;
    }

    public override void onEffect() {
        base.onEffect();
        foreach (GameCharacter gameCharacter in Characters) {
            if(!gameCharacter.BM.containsBuff(Debuff.ID))
                gameCharacter.BM.addBuff(Debuff);
        }
        Effected = true;
    }
    public override void onLeave() {
        base.onLeave();
        Characters.Clear();
        Effected = false;
    }
}
