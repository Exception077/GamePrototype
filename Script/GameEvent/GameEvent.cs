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
        // �¼���ʼʱ
    }

    public virtual void onEffect(GameTime time) {
        // �¼���Чʱ
    }

    public virtual void onFinish() {
        // �¼�����ʱ
    }
}
