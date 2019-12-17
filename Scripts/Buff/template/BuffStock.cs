using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct BuffLoadIndex {
    public string ID;
    public float Duration;
    public string State;
    public BuffLoadIndex(string id, float duration,string state) {
        ID = id;
        Duration = duration;
        State = state;
    }
}

public class BuffStock : MonoBehaviour {
    [SerializeField]
    List<Buff> Buffs;

    public Buff findBuff(string id) {
        Buff buff = new Buff();
        foreach (Buff b in Buffs) {
            if (b.ID == id) {
                buff = GameObject.Instantiate(b, transform);
                return buff;
            }
        }
        return null;
    }
}
