using UnityEngine;
using System.Collections;

public enum TrapType {
    Disposable, Continuous
}
public class Trap : MonoBehaviour {
    public float MaxEffectDuration;
    public float CurEffectDuration;
    public TrapType Type;
    public bool Effected = false;

    public virtual bool catchTarget() {
        return false;
    }

    public virtual void onEffect() {

    }

    public virtual void onLeave() {

    }
}
