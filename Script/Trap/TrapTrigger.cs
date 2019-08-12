using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {
    [SerializeField]
    Trap trap;
    [SerializeField]
    bool IS_ON = false;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!IS_ON) {
            return;
        }
        switch (trap.Type) {
            case TrapType.Disposable:
                if (trap.catchTarget() && !trap.Effected) {
                    trap.onEffect();
                } else if(!trap.catchTarget() && trap.Effected) {
                    trap.onLeave();
                }
                break;
            case TrapType.Continuous:
                if (trap.CurEffectDuration < trap.MaxEffectDuration)
                    trap.CurEffectDuration += Time.deltaTime;
                else
                    trap.CurEffectDuration = trap.MaxEffectDuration;
                if (trap.catchTarget()) {
                    if (trap.CurEffectDuration == trap.MaxEffectDuration) {
                        trap.onEffect();
                        trap.CurEffectDuration = 0;
                    }
                }
                break;
        }
    }
}
