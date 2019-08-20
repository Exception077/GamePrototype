using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Player MyPlayer;
    public Image HealthLine;
    public Image EnergyLine;
    public Image HungerLine;
    Vector2 TotalHealth;
    Vector2 TotalEnergy;
    Vector2 TotalHunger;

    // Start is called before the first frame update
    void Start()
    {
        TotalEnergy = EnergyLine.rectTransform.sizeDelta;
        TotalHealth = HealthLine.rectTransform.sizeDelta;
        TotalHunger = HungerLine.rectTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        HealthLine.rectTransform.sizeDelta = new Vector2(TotalHealth.x * MyPlayer.CurrentHealth / MyPlayer.TotalHealth, TotalHealth.y);
        EnergyLine.rectTransform.sizeDelta = new Vector2(TotalEnergy.x * MyPlayer.CurrentEnergy / MyPlayer.TotalEnergy, TotalEnergy.y);
        HungerLine.rectTransform.sizeDelta = new Vector2(TotalHunger.x * MyPlayer.CurrentHungerValue / MyPlayer.TotalHungerValue, TotalHunger.y);
    }
}
