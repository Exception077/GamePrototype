using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
[System.Serializable]
public struct GameTime {
    public int Days;
    public int Hours;
    public int Minutes;
    public GameTime(int minutes) {
        Days = minutes / 1440;
        Hours = (minutes - Days * 1440) / 60;
        Minutes = (minutes - Days * 1440 - Hours * 60);
    }

    public bool compare(GameTime gt) {
        return gt.Days == Days && gt.Hours == Hours && gt.Minutes == Minutes;
    }

    public int converse2Minutes() {
        return Days * 24 * 60 + Hours * 60 + Minutes;
    }
}

public class TimeController : MonoBehaviour {
    public int Minutes;
    [SerializeField]
    int TimeRate;
    float Timer;
    [SerializeField]
    float MinuteDuration;
    [SerializeField]
    TextMeshProUGUI TimeText;
    [SerializeField]
    TextMeshProUGUI TimeScaleText;
    [Header("EventSystem")]
    [SerializeField]
    GameEventManager EventManager;
    [SerializeField]
    List<GameEvent> GameEvents;
    [SerializeField]
    float TimeScale = 1;

    // Use this for initialization
    void Start() {
        Timer = Time.time;
    }

    // Update is called once per frame
    void Update() {
        TimeScaleText.text = "��" + Time.timeScale.ToString();
        if (Time.timeScale == 0) {
            TimeScale = 1;
        }
        foreach (GameEvent gameEvent in GameEvents) {
            if (gameEvent.ON) {
                gameEvent.onEffect(converse2Time(Minutes));
            }
            if (convers2Minutes(gameEvent.StartTime) == Minutes) {
                gameEvent.onStart();
            } else if (convers2Minutes(gameEvent.StartTime) + gameEvent.Duration == Minutes) {
                gameEvent.onFinish();
            }
        }

        if (Time.time - Timer >= MinuteDuration) {
            Minutes++;
            Timer = Time.time;
        }
        if ((Minutes % 1440) / 60 >= 23) {
            TimeText.text = string.Format("Day {0} <color=red>{1:D2}:{2:D2}</color>", 
                converse2Time(Minutes).Days, converse2Time(Minutes).Hours, converse2Time(Minutes).Minutes);
        } else {
            TimeText.text = string.Format("Day {0} <color=white>{1:D2}:{2:D2}</color>",
                converse2Time(Minutes).Days, converse2Time(Minutes).Hours, converse2Time(Minutes).Minutes);
        }
        
    }

    public GameTime converse2Time(int minutes) {
        GameTime time;
        time.Days = minutes / 1440 + 1;
        time.Hours = (minutes % 1440) / 60;
        time.Minutes = minutes % 60;
        return time;
    }

    public int convers2Minutes(GameTime time) {
        return time.Days * 1440 + time.Hours * 60 + time.Minutes;
    }

    public void setTimeScale(float power) {
        TimeScale += power;
        if (TimeScale < 0) {
            TimeScale = 0;
        } else if (TimeScale > 10) {
            TimeScale = 10;
        }
        Time.timeScale = TimeScale;
    }
}
