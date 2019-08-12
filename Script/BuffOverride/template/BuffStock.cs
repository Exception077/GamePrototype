using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct BuffLoadIndex {
    public string Name;
    public float Duration;
    public string State;
    public BuffLoadIndex(string name, float duration,string state) {
        Name = name;
        Duration = duration;
        State = state;
    }
}

public class BuffStock : MonoBehaviour {
    [SerializeField]
    List<Buff> Buffs;

    public Buff findBuff(string name) {
        Buff buff = new Buff();
        foreach (Buff b in Buffs) {
            if (b.Name == name) {
                buff = GameObject.Instantiate(b, transform);
                return buff;
            }
        }
        return null;
    }
}
