using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour {
    [SerializeField]
    public string EventName = "Unknown";
    [TextArea(5, 10)]
    public string Description = "Enter some thing...";
    List<GameCharacter> Characters = new List<GameCharacter>();
    public GameTime StartTime;
    public int Duration;
    public bool ON;

    public virtual void onStart() {
        // 事件开始时
    }

    public virtual void onEffect(GameTime time) {
        // 事件生效时
    }

    public virtual void onFinish() {
        // 事件结束时
    }
}
