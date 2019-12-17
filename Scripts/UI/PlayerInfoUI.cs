using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField]
    Player MyPlayer;
    [SerializeField]
    Image HealthLine;
    [SerializeField]
    Image EnergyLine;
    [SerializeField]
    Image HungerLine;
    [SerializeField]
    Image ShieldLine;
    [SerializeField]
    Image ShieldBack;
    [SerializeField]
    float MaxLength;
    [SerializeField]
    float StandardValue;

    Vector2 TotalHealth;
    Vector2 TotalEnergy;
    Vector2 TotalHunger;
    Vector2 TotalShield;

    // Start is called before the first frame update
    void Start()
    {
        TotalEnergy = EnergyLine.rectTransform.sizeDelta;
        TotalHealth = HealthLine.rectTransform.sizeDelta;
        TotalHunger = HungerLine.rectTransform.sizeDelta;
        TotalShield = ShieldLine.rectTransform.sizeDelta;
        MaxLength = TotalShield.x;
    }

    // Update is called once per frame
    void Update()
    {
        HealthLine.rectTransform.sizeDelta = 
            new Vector2(TotalHealth.x * MyPlayer.CurrentHealth / MyPlayer.TotalHealth, TotalHealth.y);
        EnergyLine.rectTransform.sizeDelta =
            new Vector2(TotalEnergy.x * MyPlayer.CurrentEnergy / MyPlayer.TotalEnergy, TotalEnergy.y);
        HungerLine.rectTransform.sizeDelta =
            new Vector2(TotalHunger.x * MyPlayer.CurrentHungerValue / MyPlayer.TotalHungerValue, TotalHunger.y);

        if (MyPlayer.TotalShieldValue <= 0) {
            ShieldBack.gameObject.SetActive(false);
            ShieldLine.gameObject.SetActive(false);
        } else {
            ShieldBack.rectTransform.sizeDelta = 
                new Vector2(MaxLength * MyPlayer.TotalShieldValue / StandardValue, TotalShield.y);
            ShieldBack.gameObject.SetActive(true);
            ShieldLine.rectTransform.sizeDelta = 
                new Vector2(ShieldBack.rectTransform.sizeDelta.x * MyPlayer.CurrentShieldValue / MyPlayer.TotalShieldValue, TotalShield.y);
            ShieldLine.gameObject.SetActive(true);
        }
    }
}
