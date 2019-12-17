using UnityEngine;
using System.Collections;
using TMPro;

public class PropertyDialog : Dialog {
    [SerializeField]
    GameCharacter c;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI HealthText;
    [SerializeField] TextMeshProUGUI EnergyText;
    [SerializeField] TextMeshProUGUI HungerText;
    [SerializeField] TextMeshProUGUI ShieldText;

    [SerializeField] TextMeshProUGUI DamageText;
    [SerializeField] TextMeshProUGUI DamageTypeText;
    [SerializeField] TextMeshProUGUI PhysicDefenceText;
    [SerializeField] TextMeshProUGUI MagicDefenceText;
    [SerializeField] TextMeshProUGUI StateStr;

    private void Start() {
        if (PlayerPrefs.HasKey("DialogPosX" + DialogName)) {
            transform.localPosition = new Vector3(PlayerPrefs.GetFloat("DialogPosX" + DialogName), PlayerPrefs.GetFloat("DialogPosY" + DialogName), PlayerPrefs.GetFloat("DialogPosZ" + DialogName));
        }
    }

    private void Update() {
        PlayerPrefs.SetFloat("DialogPosX" + DialogName, transform.localPosition.x);
        PlayerPrefs.SetFloat("DialogPosY" + DialogName, transform.localPosition.y);
        PlayerPrefs.SetFloat("DialogPosZ" + DialogName, transform.localPosition.z);

        NameText.text = c.CharacterName;
        HealthText.text = string.Format("{0:N0}/{1:N0}", c.CurrentHealth, c.TotalHealth);
        EnergyText.text = string.Format("{0:N0}/{1:N0}", c.CurrentEnergy, c.TotalEnergy);
        HungerText.text = string.Format("{0:N0}/{1:N0}", c.CurrentHungerValue, c.TotalHungerValue);
        ShieldText.text = string.Format("{0:N0}/{1:N0}", c.CurrentShieldValue, c.TotalShieldValue);
        PhysicDefenceText.text = c.PhysicDefence.ToString();
        MagicDefenceText.text = c.MagicDefence.ToString();
        switch (c.MyDamageType) {
            case DamageType.PHYSIC:
                DamageTypeText.text = "<color=#CD853F>PHYSIC</color>";
                DamageText.text = "<color=red>" + c.BasicDamage.ToString();
                break;
            case DamageType.MAGIC:
                DamageTypeText.text = "<color=#8B008B>MAGIC</color>";
                DamageText.text = "<color=#9400D3>" + c.BasicDamage.ToString();
                break;
        }
    }
}
