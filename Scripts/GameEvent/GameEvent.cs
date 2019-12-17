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
    int TotalDuration;
    public virtual void onStart() {
        // �¼���ʼʱ
        TotalDuration = Duration;
    }

    public virtual void onEffect(GameTime time) {
        Duration = TotalDuration - (time.converse2Minutes() - StartTime.converse2Minutes());
        // �¼���Чʱ
    }

    public virtual void onFinish() {
        // �¼�����ʱ
    }
}
