using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public struct GameTime {
    public int Days;
    public int Hours;
    public int Minutes;
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
    // Use this for initialization
    void Start() {
        Timer = Time.time;
    }

    // Update is called once per frame
    void Update() {
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

}
